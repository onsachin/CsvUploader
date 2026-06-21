// using CsvUploader.Web.CsvParser;
// using CsvUploader.Web.Utilities;
// using CsvUploader.Web.Utilities.Mappers;
//
// namespace CsvUploader.Web.Services;
//
// public class CsvImportDispatcher
// {
//     public CsvImportDispatcher()
//     {
//
//     }
//
//     public async Task<ImportResult> ImportAsync(Stream stream, string sourceFileName, CancellationToken ct = default)
//     {
//         var buffer = new MemoryStream();
//         await stream.CopyToAsync(buffer, ct);
//         buffer.Position = 0;
//
//         var headers =await CsvFileReader.ReadHeaderAsync(new MemoryStream(buffer.ToArray()), ct);
//
//         var standard = new PersonStandardRowMapper();
//         if (standard.CanHandle(headers))
//         {
//             
//         }
//         
//         string error = string.Empty;
//
//
//         return new ImportResult(sourceFileName, 0, 0, 1, new[] { error }, TimeSpan.Zero);
//     }
// }