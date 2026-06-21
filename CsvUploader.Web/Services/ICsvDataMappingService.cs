public interface ICsvDataMappingService
{
    Task<dynamic> MapRow(IReadOnlyDictionary<string, string> row, int rowIndex);
    
}

public class CsvDataMappingService(IDateConvertService dateConvertService) : ICsvDataMappingService
{
    public async Task<dynamic> MapRow(IReadOnlyDictionary<string, string> row, int rowIndex)
    {
        int age = dateConvertService.AgeFromDateOfBirth(row.GetValueOrDefault("Date Of Birth", string.Empty));
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
            Age = age
        };
    }
}