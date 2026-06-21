public interface IDateConvertService
{
    int AgeFromDateOfBirth(string dateOfBirth);
}

public class DateConvertService : IDateConvertService
{
    public int AgeFromDateOfBirth(string dateOfBirth)
    {
        if (DateTime.TryParse(dateOfBirth, out var dob))
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }

        return 0; // or throw an exception if the date is invalid
    }
}