using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;

namespace POC.PdfGenerator.PdfPig;

public class UnitTest1
{
    [Theory]
    [InlineData("Chinese", "浅色模式")]
    [InlineData("English", "Light Mode")]
    [InlineData("Russian", "Светлый режим")]
    [InlineData("Japanese", "ライトモード")]
    [InlineData("Korean", "라이트 모드")]
    [InlineData("Poland", "Tryb jasny")]
    [InlineData("Ukrainian", "Світлий режим")]
    public void Test1(string language, string word)
    {
        PdfDocumentBuilder builder = new PdfDocumentBuilder();


        var font = GetFont(builder, language);
        
        PdfDocumentBuilder.AddedFont helvetica = builder.AddStandard14Font(Standard14Font.Helvetica);
        PdfDocumentBuilder.AddedFont helveticaBold = builder.AddStandard14Font(Standard14Font.TimesRoman);

        PdfPageBuilder page = builder.AddPage(PageSize.A4);

        PdfPoint closeToTop = new PdfPoint(15, page.PageSize.Top - 50);

        page.AddText("Comform letter heading - bold", 14, closeToTop, helveticaBold);
        page.AddText("Project: Apple", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 80), helvetica);
        page.AddText($"Order placed on: {DateTime.Now.ToShortDateString()}", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 100), helvetica);
        page.AddText($"language: {word}", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 120), font);

        var logoImageStream = File.OpenRead("logo.png"); // this can be done once on startup
        page.AddPng(logoImageStream, new PdfRectangle(page.PageSize.Right - 125,page.PageSize.Top - 50, page.PageSize.Right - 25, page.PageSize.Top - 30));

        page.AddText(DateTime.UtcNow.ToString("yyyy MMMM dd"), 12, new PdfPoint(page.PageSize.Right - 100, 50), helvetica);

        File.WriteAllBytes($"output.{language}.pdf", builder.Build());
    }

    private PdfDocumentBuilder.AddedFont GetFont(PdfDocumentBuilder builder, string language)
    {
        return language switch
        {
            "Chinese" => GetFontBytes("NotoSansSC-Regular.ttf"),
            "Japanese" => GetFontBytes("NotoSansJP-Regular.ttf"),
            "Korean" => GetFontBytes("NotoSansKR-Regular.ttf"),
            _ => GetFontBytes("NotoSans-Regular.ttf")
        };

        PdfDocumentBuilder.AddedFont GetFontBytes(string fileName)
        {
            var font = File.ReadAllBytes(fileName);

            return builder.AddTrueTypeFont(font);
        }
    }
}