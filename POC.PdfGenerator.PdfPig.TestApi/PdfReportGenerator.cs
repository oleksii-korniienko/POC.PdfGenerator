using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;

namespace POC.PdfGenerator.PdfPig.TestApi;

public static class PdfReportGenerator
{
    private static Dictionary<string, (string, string)> dictionary;
    private static HashSet<string> fontPaths;

    static PdfReportGenerator()
    {
        dictionary = new Dictionary<string, (string, string)>()
        {
            { "en", ("English", "Light Mode") },
            { "ru", ("Russian", "Светлый режим") },
            { "ja", ("Japanese", "ライトモード") },
            { "ko", ("Korean", "라이트 모드") },
            { "zh", ("Chinese", "浅色模式") },
            { "uk", ("Ukrainian", "Світлий режим") },
            { "pl", ("Polish", "Tryb jasny") },
        };

        fontPaths =
        [
            "NotoSansMonoCJKsc-Regular-03.ttf",
            /*"NotoSansKR-Regular.ttf",
            "NotoSansSC-Regular.ttf"*/
        ];

    }

    public static MemoryStream Generate()
    {
        var memoryStream = new MemoryStream();

        var font = File.ReadAllBytes("Arial Unicode MS Regular.ttf");

        PdfDocumentBuilder builder = new PdfDocumentBuilder();

        var customFont = builder.AddTrueTypeFont(font);

        PdfDocumentBuilder.AddedFont helvetica = builder.AddStandard14Font(Standard14Font.Helvetica);
        PdfDocumentBuilder.AddedFont helveticaBold = builder.AddStandard14Font(Standard14Font.TimesRoman);

        PdfPageBuilder page = builder.AddPage(PageSize.A4);

        PdfPoint closeToTop = new PdfPoint(15, page.PageSize.Top - 50);

        page.AddText("Comform letter heading - bold", 14, closeToTop, helveticaBold);
        page.AddText("Project: Apple", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 80), helvetica);
        page.AddText($"Order placed on: {DateTime.Now.ToShortDateString()}", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 100), helvetica);
        page.AddText($"language: Korean. Symbol: 라이트 모드", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 120), customFont);
        page.AddText($"language: Japanese. Symbol: ライトモード", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 140), customFont);
        page.AddText($"language: Chinese. Symbol: 浅色模式", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 160), customFont);
        page.AddText($"language: Russian. Symbol: Светлый режим", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 180), customFont);
        page.AddText($"language: English. Symbol: Light Mode", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 200), customFont);
        page.AddText($"language: Ukrainian. Symbol: Світлий режим", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 220), customFont);

        var logoImageStream = File.OpenRead("logo.png"); // this can be done once on startup
        page.AddPng(logoImageStream, new PdfRectangle(page.PageSize.Right - 125, page.PageSize.Top - 50, page.PageSize.Right - 25, page.PageSize.Top - 30));

        page.AddText(DateTime.UtcNow.ToString("yyyy MMMM dd"), 12, new PdfPoint(page.PageSize.Right - 100, 50), helvetica);

        var bytes = builder.Build();

        memoryStream.Write(bytes, 0, bytes.Length);

        return memoryStream;
        //.GeneratePdf("output.pdf");
    }

    public static MemoryStream GenerateSingleLanguage(string text)
    {
        foreach (var font in fontPaths)
        {
            try
            {
                return TryToGenerate(text, font);
            }
            catch (Exception ex) when(ex.Message.StartsWith("The font does not contain a character"))
            {
            }
        }
        
        throw new Exception("No font found for text: " + text);
    }

    private static MemoryStream TryToGenerate(string language, string fontPath)
    {
        var font = File.ReadAllBytes(fontPath);

        PdfDocumentBuilder builder = new PdfDocumentBuilder();

        var customFont = builder.AddTrueTypeFont(font);

        PdfDocumentBuilder.AddedFont helvetica = builder.AddStandard14Font(Standard14Font.Helvetica);
        PdfDocumentBuilder.AddedFont helveticaBold = builder.AddStandard14Font(Standard14Font.TimesRoman);

        PdfPageBuilder page = builder.AddPage(PageSize.A4);

        PdfPoint closeToTop = new PdfPoint(15, page.PageSize.Top - 50);

        page.AddText("Comform letter heading - bold", 14, closeToTop, helveticaBold);
        page.AddText("Project: Apple", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 80), helvetica);
        page.AddText($"Order placed on: {DateTime.Now.ToShortDateString()}", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 100), helvetica);
        var (languageName, word) = dictionary[language];
        
        page.AddText($"language: {languageName}. Symbol: {word}", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 120), customFont);

        var logoImageStream = File.OpenRead("logo.png"); // this can be done once on startup
        page.AddPng(logoImageStream, new PdfRectangle(page.PageSize.Right - 125, page.PageSize.Top - 50, page.PageSize.Right - 25, page.PageSize.Top - 30));

        page.AddText(DateTime.UtcNow.ToString("yyyy MMMM dd"), 12, new PdfPoint(page.PageSize.Right - 100, 50), helvetica);

        var bytes = builder.Build();

        var newMemoryStream = new MemoryStream();
        newMemoryStream.Write(bytes, 0, bytes.Length);

        return newMemoryStream;
    }
    
    
}