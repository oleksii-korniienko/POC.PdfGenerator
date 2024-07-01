using System.Text;
using Aspose.Pdf;

namespace POC.PdfGenerator.Aspose;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var original = File.ReadAllText("src.html");
        var modifiedTemplate = Substitute(original, new Dictionary<string, string>
        {
            {"name", "John Doe"},
            {"age", "30"},
            {"address", "123 Main St"},
            {"city", "Anytown"},
            {"state", "WA"},
            {"zip", "99999"},
            {"date", DateTime.Now.ToShortDateString()}
        });

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(modifiedTemplate));
        
        Document pdfDocument = new Document(stream, new HtmlLoadOptions());
        
        pdfDocument.Save("output.pdf");
        
    }
    
    private static string Substitute(string template, Dictionary<string, string> values)
    {
        foreach (var key in values.Keys)
        {
            template = template.Replace("{{" + key + "}}", values[key]);
        }
        return template;
    }
}