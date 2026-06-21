using System.Text.Json;
using CsvUploader.Web.CsvParser;
using CsvUploader.Web.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<ICsvDataMappingService, CsvDataMappingService>();
builder.Services.AddScoped<IDateConvertService, DateConvertService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/getfiles", async (ICsvDataMappingService csvDataMappingService, CancellationToken ct) =>
{
    var data = await ReadJsonData(csvDataMappingService, ct);
    var jsonData = JsonSerializer.Serialize(data);

    var csObjects = JsonSerializer.Deserialize<PersontEntity[]>(jsonData);
    return Results.Ok(csObjects);
});

app.Run();




async Task<IReadOnlyList<object>> ReadJsonData(ICsvDataMappingService csvDataMappingService, CancellationToken ct = default)
{
    var fileStream = (Stream)File.OpenRead("Data/people-100-extra-1k.csv");
    var buffer = new MemoryStream();
    await fileStream.CopyToAsync(buffer, ct);
    buffer.Position = 0;
    string line = string.Empty;
    using var reader = new StreamReader(new MemoryStream(buffer.ToArray()), leaveOpen: true);

    var data = await CsvFileReader.ReadHeaderAsync(new MemoryStream(buffer.ToArray()), ct);

    //var data1 = await CsvFileReader.ParseAsync(new MemoryStream(buffer.ToArray()), ct);
    var list = new List<dynamic>();
    await foreach (var (row, rowIndex)
                   in CsvFileReader.ParseWithHeaderAsync(new MemoryStream(buffer.ToArray()), ct))
    {
        var typed = await csvDataMappingService.MapRow(row, rowIndex);
        list.Add(typed);
    }

    return list;
}

