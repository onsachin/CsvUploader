// namespace CsvUploader.Web.Utilities;
//
// public sealed record ImportResult(
//     string SourceFile,
//     int TotalRows,
//     int Succeeded,
//     int Failed,
//     IReadOnlyList<string> Errors,
//     TimeSpan Duration)
// {
//     public bool HasError =>Errors.Count > 0;
//
//     public override string ToString() =>
//         $"[{SourceFile}] {Succeeded}/{TotalRows} succeeded in {Duration.TotalSeconds:F2}s. " +
//         (HasError?$"{Failed} errors.":"No errors");
// }