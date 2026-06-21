 using System.Text.Json.Serialization;
 using CsvUploader.Web.Utilities;

// namespace CsvUploader.Web.DTOs;
//
// public sealed record PersontandardRow(
//     int Index,
//     string UserId,
//     string FirstName,
//     string LastName,
//     string Sex,
//     string Email,
//     string Phone,
//     string DateOfBirth,
//     string JobTitle,
//     int Salary) : ICsvRow;
    
// public sealed record PersontandardRow(
//     int Index,
//     string UserId,
//     string FirstName,
//     string LastName,
//     string Sex,
//     string Email,
//     string Phone,
//     string DateOfBirth,
//     string JobTitle,
//     int Salary) ;
 
 public class PersontandardRow
 {
     public int? Index { get; init; }
     public string? UserId { get; init; }
     public string? FirstName { get; init; }
     public string? LastName { get; init; }
     public string? Sex { get; set; }
     public string? Email { get; init; }
     public string? Phone { get; init; }
     public string? DateOfBirth { get; init; }
     public string? JobTitle { get; init; }
     public int? Salary { get; init; }

 }
 
 
 [JsonConverter(typeof(SourceMapConverter<PersontandardRow>))]
 public class Persontandard
 {
     public int? Index { get; init; }

     [SourceMap("id", "customerId","UserId")] 
     public string? id { get; init; }
    
     public string? FirstName { get; init; }
     public string? LastName { get; init; }
     public string? Sex { get; set; }
     public string? Email { get; init; }
     public string? Phone { get; init; }
     public string? DateOfBirth { get; init; }
     public string? JobTitle { get; init; }
     public int? Salary { get; init; }

 }