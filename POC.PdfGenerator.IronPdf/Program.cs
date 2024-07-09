using Microsoft.AspNetCore.Http.HttpResults;
using POC.PdfGenerator.IronPdf.Views;
using Razor.Templating.Core;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

License.LicenseKey = "IRONSUITE.OLEKSII.KORNIIENKO.IDEALSCORP.COM.16355-943D0D42BA-AUIRG3WK6PRHLJ-GVUCUL6GJXIQ-RF5DBNEZRRWF-CJGOORXRRIM5-NALGCAIDAEBG-GMB6A7KYTLJW-W4PBTD-TKMEKUFHVIWNEA-DEPLOYMENT.TRIAL-AMT6IW.TRIAL.EXPIRES.08.AUG.2024";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/test-report", async () =>
    {
       var html =  await RazorTemplateEngine.RenderAsync("views/test.cshtml", new TestModelForCsHtml());

       var renderer = new ChromePdfRenderer();

       using var pdfDoc = renderer.RenderHtmlAsPdf(html);
       pdfDoc.SaveAs("test-report.pdf");

    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}