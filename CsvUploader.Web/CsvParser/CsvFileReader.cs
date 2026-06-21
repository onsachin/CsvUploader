using System.Runtime.CompilerServices;
using System.Text;

namespace CsvUploader.Web.CsvParser;

public static class CsvFileReader
{


    /// <summary>
    /// ReadHeader
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public static async Task<IReadOnlyList<string>> ReadHeaderAsync(Stream stream,
        CancellationToken ct = default)
    {
        await foreach (var row in ParseAsync(stream, ct))
            return row.Select(field => field.Trim()).ToList();

        return Array.Empty<string>();
    }

    /// <summary>
    /// ParseWithHeaderAsync
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<(IReadOnlyDictionary<string,string> row,int index)> ParseWithHeaderAsync(Stream stream,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        string[]? headers = null;
        int rowIndex = 0;

        await foreach (var fields in ParseAsync(stream, ct))
        {
            if (headers == null)
            {
                headers = fields.Select(field => field.Trim()).ToArray();
                continue;
            }

            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < headers.Length; i++)
            {
                dict[headers[i]] = i<fields.Count() ? fields[i] : string.Empty;
            }
            yield return (dict, ++rowIndex);
        }
    }

    /// <summary>
    /// Parse async
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<IReadOnlyList<string>> ParseAsync(
        Stream stream,
        [EnumeratorCancellation] CancellationToken ct = default
    )
    {
        using var reader = new StreamReader(stream, leaveOpen: true);
        string? line;
        bool isfirstLine = true;

        while ((line = await reader.ReadLineAsync(ct)) is not null)
        {
            ct.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(line)) continue;
            if (isfirstLine)
            {
                line = line.TrimStart('\uFEFF');
                isfirstLine = false;
            }

            var fields = ParseLine(line);

            yield return fields;
        }
    }

    /// <summary>
    /// Parse line
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    private static IReadOnlyList<string> ParseLine(ReadOnlySpan<char> line)
    {
        var fields = new List<string>();
        var currentField = new StringBuilder();
        var inQuotes = false;
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Escaped quote inside quoted field
                    currentField.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(currentField.ToString());
                currentField.Clear();
            }
            else
            {
                currentField.Append(c);
            }
        }

        fields.Add(currentField.ToString());
        return fields.ToArray();
    }
}