using System.Text;
using Aspose.Pdf;

namespace POC.PdfGenerator.Aspose;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var original = File.ReadAllText("src.html");

        // generate a sample words of different languages to test encoding. Each words should contain characters that are specific to the language (so that to test every possible unsupported symbol)

        var languagesWordsSample = """
                            <li>Chinese: 浅色模式</li>
                            <li>Ukrainian: Укрзалізниця</li>
                            <li>Russian: Героев упа 73В</li>
                            <li>English: Anytown, WA 99999</li>
                            <li>Korean: 어두운 모드</li>
                            <li>Japanese: ダークモード</li>
                            <li>Arabic: وضع الظلام</li>
                            <li>Hebrew: מצב כהה</li>
                            <li>Spanish: Modo oscuro</li>
                            <li>French: Mode sombre</li>
                            <li>German: Dunkles Design</li>
                            <li>Italian: Modalità scura</li>
                            <li>Portuguese: Modo escuro</li>
                            <li>turkish: Koyu mod</li>
                            <li>Swedish: Mörkt läge</li>
                            <li>Netherlands: Donkere modus</li>
                            
                            
                            """;
        var modifiedTemplate = Substitute(original, new Dictionary<string, string>
        {
            {"languages", languagesWordsSample},
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