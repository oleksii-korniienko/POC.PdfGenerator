using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;

namespace POC.PdfGenerator.PdfPig;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        PdfDocumentBuilder builder = new PdfDocumentBuilder();

        PdfDocumentBuilder.AddedFont helvetica = builder.AddStandard14Font(Standard14Font.Helvetica);
        PdfDocumentBuilder.AddedFont helveticaBold = builder.AddStandard14Font(Standard14Font.HelveticaBold);

        PdfPageBuilder page = builder.AddPage(PageSize.A4);

        PdfPoint closeToTop = new PdfPoint(15, page.PageSize.Top - 50);

        page.AddText("Comform letter heading - bold", 14, closeToTop, helveticaBold);
        page.AddText("Project: Apple", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 80), helvetica);
        page.AddText($"Order placed on: {DateTime.Now.ToShortDateString()}", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 100), helvetica);
        page.AddText($"Archive name: Iphone 13 pro max", 12, new PdfPoint(closeToTop.X, closeToTop.Y - 120), helvetica);

        var logoImageStream = File.OpenRead("logo.png"); // this can be done once on startup
        page.AddPng(logoImageStream, new PdfRectangle(page.PageSize.Right - 125,page.PageSize.Top - 50, page.PageSize.Right - 25, page.PageSize.Top - 30));

        page.AddText(DateTime.UtcNow.ToString("yyyy MMMM dd"), 12, new PdfPoint(page.PageSize.Right - 100, 50), helvetica);

        File.WriteAllBytes("output.pdf", builder.Build());
    }
}