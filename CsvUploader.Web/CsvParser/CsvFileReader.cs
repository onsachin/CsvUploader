using System.Runtime.CompilerServices;
using System.Text;

namespace CsvUploader.Web.CsvParser;

public static class CsvFileReader
{


    public static async Task<IReadOnlyList<string>> ReadHeaderAsync(Stream stream,
        CancellationToken ct = default)
    {
        await foreach (var row in ParseAsync(stream, ct))
            return row.Select(field => field.Trim()).ToList();

        return Array.Empty<string>();
    }

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

            var dict = new Dictionary<string, string>();//StringComparison.OrdinalIgnoreCase);

            for (int i = 0; i < headers.Length; i++)
            {
                dict[headers[i]] = i<fields.Count() ? fields[i] : string.Empty;
            }
            yield return (dict, ++rowIndex);
        }
    }

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

    private static IReadOnlyList<string> ParseLine(ReadOnlySpan<char> line)
    {
        var fields = new List<string>();
        var currentField = new StringBuilder();
        foreach (var c in line)
        {
            if (c == ',' && c != '"')
            {
                fields.Add(currentField.ToString());
                currentField.Clear();
            }

            currentField.Append(c);
        }

        fields.Add(currentField.ToString());
        return fields.ToArray();
    }
}