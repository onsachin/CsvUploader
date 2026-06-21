// namespace CsvUploader.Web.Utilities;
//
// public interface ICsvRow
// {
// }
//
// public interface ICsvRowMapper<T> where T : ICsvRow
// {
//     IReadOnlyList<string> ExpectedHeaders { get; }
//     T MapRow(IReadOnlyDictionary<string,string> row, int rowIndex);
//     bool CanHandle(IReadOnlyList< string> headers);
// }
//
// public interface IDomainEntityFactory<in TRow, TEntity> where TRow : ICsvRow
// {
//     Task<Result<TEntity>> CreateAsync(TRow data, string sourceFileName, CancellationToken cancellationToken = default);
// }
//
// public interface ICsvImportPipeline<TRow, TEntity> where TRow : ICsvRow
// {
//     Task<ImportResult> ExecuteAsync(Stream stream, string sourceFileName, CancellationToken ct = default);
//
// }