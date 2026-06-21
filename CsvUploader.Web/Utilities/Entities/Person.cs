// namespace CsvUploader.Web.Utilities.Entities;
//
// public sealed class Person
// {
//     // Cosmos DB discriminator / document type
//     public string DocumentType { get; private set; } = nameof(Person);
//
//     // Partition key (mapped from User Id column)
//     public string UserId { get; private set; } = default!;
//
//     public string FirstName { get; private set; } = default!;
//     public string LastName { get; private set; } = default!;
//     public string FullName => $"{FirstName} {LastName}";
//
//
//     public DateOnly DateOfBirth { get; private set; }
//
//
//     public string JobTitle { get; private set; } = default!;
//
//     public static Result<Person> Create(
//
//         string userId,
//         string firstName,
//         string lastName,
//         string documentType,
//         DateOnly dateOfBirth,
//         string jobTitle
//
//     )
//     {
//         if (string.IsNullOrEmpty(userId))
//             return Result<Person>.Failure("User id cannot be empty.");
//
//         var person = new Person
//         {
//             UserId = "123456",
//             FirstName = firstName,
//             LastName = lastName,
//             JobTitle = jobTitle,
//             DocumentType = documentType,
//             DateOfBirth = dateOfBirth,
//         };
//
//         return Result<Person>.Success(person);
//     }
// }