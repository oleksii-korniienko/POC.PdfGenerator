using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace POC.PdfGenerator.Aspose.TestApi;

public static class PdfReportGenerator
{
    private static readonly string OriginalFile;
    
    static (string Language, string Text)[] texts;
    
    static PdfReportGenerator()
    {
        License license = new License();
        // Set license
        license.SetLicense("Aspose.Total.NET.lic");
        
        OriginalFile = File.ReadAllText("src.html");
        
        texts =
        [
            ("English", "Light Mode"),
            /*("Chinese", "浅色模式"),
            ("Russian", "Светлый режим"),
            ("Japanese", "墨消し未実行のファイルのダウンロード"),
            ("Korean", "라이트 모드"),
            ("Polish", "Tryb jasny"),
            ("Ukrainian", "Світлий режим"),
            ("Arabic", "وضع النهار"),
            ("Hebrew", "מצב יום"),
            ("Spanish", "Modo claro"),
            ("French", "Mode clair"),
            ("German", "Heller Modus"),
            ("Italian", "Modalità chiara"),
            ("Portuguese", "Modo claro"),
            ("Turkish", "Açık Mod"),
            ("Swedish", "Ljust läge"),
            ("Netherlands", "Lichte modus")*/
        ];
        
    }
    
    public static Stream Generate()
    {
        var languagesWordsSample = string.Join('\n', texts.Select(x => $"<li>{x.Language}: {x.Text}</li>"));
        var modifiedTemplate = Substitute(OriginalFile, new Dictionary<string, string>
        {
            {"languages", languagesWordsSample},
            {"date", DateTime.Now.ToShortDateString()}
        });
        

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(modifiedTemplate));
        
        Document pdfDocument = new Document(stream, new HtmlLoadOptions());
        
        var memoryStream = new MemoryStream();
        pdfDocument.Save(memoryStream);

        return memoryStream;
    }

    public static Stream GenerateManual()
    {

        // Initialize the document
        Document pdfDocument = new Document();

        // Add a page to the document
        Page page = pdfDocument.Pages.Add();

        // Create a table for the header
        Table headerTable = new Table
        {
            ColumnWidths = "300 200", // Define column widths
            DefaultCellBorder = new BorderInfo(BorderSide.None),
            Margin = new MarginInfo(0, 0, 0, 10) // Add some margin at the bottom
        };

        // Create a row for the header
        Row headerRow = headerTable.Rows.Add();

        // Create a cell for the text
        Cell textCell = headerRow.Cells.Add();
        TextFragment headerText = new TextFragment("Document Header")
        {
            TextState =
            {
                FontSize = 16,
                //Font = FontRepository.FindFont("Arial"),
                FontStyle = FontStyles.Bold,
                HorizontalAlignment = HorizontalAlignment.Left
            }
        };

        textCell.Paragraphs.Add(headerText);

        // Create a cell for the image
        Cell imageCell = headerRow.Cells.Add();
        Image headerImage = new Image
        {
            File = "logo.png", // Path to your image file
            FixWidth = 100,
            FixHeight = 30
        };

        imageCell.Paragraphs.Add(headerImage);

        // Add the header table to the page
        page.Paragraphs.Add(headerTable);

        // Specify the path to the custom font
        //string fontPath = @"D:\Repository\Temp\POC.PdfGenerator\POC.PdfGenerator.Aspose\NotoSansKR-Regular.ttf";

        // Load the custom font
        //Font customFont = FontRepository.OpenFont(fontPath);

        // Array of languages and their respective texts

        // Set the initial vertical position for the text
        double positionY = page.PageInfo.Height - 150; // Adjusted to fit below the header

        // Loop through each language and text
        foreach (var (language, text) in texts)
        {
            // Create a text fragment and set the custom font
            TextFragment textFragment = new TextFragment($"{language} - {text}")
            {
                TextState =
                {
                    //Font = customFont,
                    FontSize = 14,
                    ForegroundColor = Color.FromRgb(System.Drawing.Color.Black)
                },
                Position = new Position(100, positionY)
            };

            // Add the text fragment to the page
            page.Paragraphs.Add(textFragment);

            // Decrease the vertical position for the next text to avoid overlap
            positionY -= 30;
        }


        // Create a row for the footer

        // Create a cell for the footer text
        TextFragment footerText = new TextFragment("Page 1 of 1")
        {
            TextState =
            {
                FontSize = 12,
                //Font = FontRepository.FindFont("Arial"),
                HorizontalAlignment = HorizontalAlignment.Center
            }
        };

        // Add the footer table to the page
        //footerTable.VerticalAlignment = VerticalAlignment.Bottom;
        page.Paragraphs.Add(footerText);

        // Ensure the footer is the last element added
        /*page.Paragraphs.Add(new FloatingBox
        {
            Margin = new MarginInfo(0, 10, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom,
            Paragraphs = { footerTable }
        });*/

        // Save the document

        var stream = new MemoryStream();
        pdfDocument.Save(stream);

        return stream;

        Console.WriteLine("PDF with custom font, header, and footer created successfully!");
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