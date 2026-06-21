// using CsvUploader.Web.DTOs;
// using CsvUploader.Web.Utilities.Entities;
//
// namespace CsvUploader.Web.Utilities.Mappers;
//
// public sealed class PersonStandardRowMapper : ICsvRowMapper<PersontandardRow>
// {
//     public IReadOnlyList<string> ExpectedHeaders { get; }=new[]
//     {
//         "Index", "User Id", "First Name", "Last Name",
//         "Sex", "Email", "Phone Number", "Date of birth", "Job Role", "Salary"
//     };
//
//     public PersontandardRow MapRow(IReadOnlyDictionary<string, string> row, int rowIndex)
//     {
//         return new PersontandardRow(
//             Index:0,
//             Salary:0,
//             UserId:row.GetValueOrDefault("User Id", string.Empty),
//             FirstName:row.GetValueOrDefault("First Name", string.Empty),
//             LastName:row.GetValueOrDefault("Last Name", string.Empty),
//             Email:row.GetValueOrDefault("Email", string.Empty),
//             Phone:row.GetValueOrDefault("Phone Number", string.Empty),
//             DateOfBirth:row.GetValueOrDefault("Date Of Birth", string.Empty),
//             JobTitle:row.GetValueOrDefault("Job Title", string.Empty),
//             Sex: row.GetValueOrDefault("Sex", string.Empty)
//            
//             );
//     }
//
//     public bool CanHandle(IReadOnlyList<string> headers)
//     {
//         var set = new HashSet<string>(headers, StringComparer.OrdinalIgnoreCase);
//         return set.Contains("User Id", StringComparer.OrdinalIgnoreCase) && 
//                set.Contains("First Name", StringComparer.OrdinalIgnoreCase) &&
//                set.Contains("Last Name", StringComparer.OrdinalIgnoreCase) &&
//                set.Contains("Salary", StringComparer.OrdinalIgnoreCase);
//     }
// }