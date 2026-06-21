using CsvUploader.Web.CsvParser;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/getfiles", async (CancellationToken ct) =>
{
    var data =await ReadJsonData(ct);
    return Results.Ok(data);
});

app.Run();




async Task<IReadOnlyList<PersontandardRow>> ReadJsonData(CancellationToken ct = default)
{
    var fileStream=(Stream)File.OpenRead("Data/people-100.csv");
    var buffer = new MemoryStream();
    await fileStream.CopyToAsync(buffer, ct);
    buffer.Position = 0;
    string line=string.Empty;
    using var reader = new StreamReader(new MemoryStream(buffer.ToArray()), leaveOpen: true);
    
    var data = await CsvFileReader.ReadHeaderAsync(new MemoryStream(buffer.ToArray()), ct);
   
    //var data1 = await CsvFileReader.ParseAsync(new MemoryStream(buffer.ToArray()), ct);
    var list = new List<PersontandardRow>();
    await foreach (var (row, rowIndex)
                   in CsvFileReader.ParseWithHeaderAsync(new MemoryStream(buffer.ToArray()), ct))
    {
        var typed = MapRow(row, rowIndex);
        list.Add(typed);
    }
    
    return list;
}

PersontandardRow MapRow(IReadOnlyDictionary<string, string> row, int rowIndex)
{
    return new PersontandardRow(
        Index:0,
        Salary:0,
        UserId:row.GetValueOrDefault("User Id", string.Empty),
        FirstName:row.GetValueOrDefault("First Name", string.Empty),
        LastName:row.GetValueOrDefault("Last Name", string.Empty),
        Email:row.GetValueOrDefault("Email", string.Empty),
        Phone:row.GetValueOrDefault("Phone Number", string.Empty),
        DateOfBirth:row.GetValueOrDefault("Date Of Birth", string.Empty),
        JobTitle:row.GetValueOrDefault("Job Title", string.Empty),
        Sex: row.GetValueOrDefault("Sex", string.Empty)
           
    );
}