// using CsvUploader.Web.DTOs;
// using CsvUploader.Web.Utilities.Entities;
//
// namespace CsvUploader.Web.Utilities.Mappers;
//
//
// public class PersonEntityFactory : IDomainEntityFactory<PersontandardRow, Person>
// {
//     public async Task<Result<Person>> CreateAsync(PersontandardRow row, string sourceFileName,
//         CancellationToken cancellationToken = default)
//     {
//         var sal = 0; //call conversion api
//         //Check the sal for failures
//
//         return Person.Create(
//             userId: row.UserId,
//             firstName: row.FirstName,
//             lastName: row.LastName,
//             documentType: "",
//             dateOfBirth: new DateOnly(),
//             jobTitle: row.JobTitle
//         );
//     }
// }
