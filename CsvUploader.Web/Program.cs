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




async Task<IReadOnlyList<string>> ReadJsonData(CancellationToken ct = default)
{
    var fileStream=(Stream)File.OpenRead("Data/people-100.csv");
    var buffer = new MemoryStream();
    await fileStream.CopyToAsync(buffer, ct);
    buffer.Position = 0;
    string line=string.Empty;
    using var reader = new StreamReader(new MemoryStream(buffer.ToArray()), leaveOpen: true);
    
    var data = await CsvFileReader.ReadHeaderAsync(new MemoryStream(buffer.ToArray()), ct);
    return data;
}