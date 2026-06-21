// using CsvUploader.Web.Utilities;

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
    
public sealed record PersontandardRow(
    int Index,
    string UserId,
    string FirstName,
    string LastName,
    string Sex,
    string Email,
    string Phone,
    string DateOfBirth,
    string JobTitle,
    int Salary) ;