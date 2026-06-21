namespace CsvUploader.Web.Services;

public static class CsvDataMapper
{
    public static async Task<dynamic> MapRow(IReadOnlyDictionary<string, string> row, int rowIndex)
    {
        return new
        {
            Index = 0,
            Salary = 0,
            UserId = row.GetValueOrDefault("User Id", string.Empty),
            FirstName = row.GetValueOrDefault("First Name", string.Empty),
            LastName = row.GetValueOrDefault("Last Name", string.Empty),
            Email = row.GetValueOrDefault("Email", string.Empty),
            Phone = row.GetValueOrDefault("Phone Number", string.Empty),
            DateOfBirth = row.GetValueOrDefault("Date Of Birth", string.Empty),
            JobTitle = row.GetValueOrDefault("Job Title", string.Empty),
            Sex = row.GetValueOrDefault("Sex", string.Empty)

        };

    }
}