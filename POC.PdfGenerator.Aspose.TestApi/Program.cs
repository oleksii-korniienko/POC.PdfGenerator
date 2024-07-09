using POC.PdfGenerator.Aspose.TestApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.MapGet("/generate-from-html", () =>
    {
        var pdfStream = PdfReportGenerator.Generate();
        
        return Results.File(pdfStream, "application/pdf");
    })
    .WithName("Generate pdf from html template")
    .WithOpenApi();

app.MapGet("/generate-manually", () =>
    {
        var pdfStream = PdfReportGenerator.GenerateManual();
        
        return Results.File(pdfStream, "application/pdf");
    })
    .WithName("Generate pdf manually")
    .WithOpenApi();

app.Run();

namespace POC.PdfGenerator.Aspose.TestApi
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}