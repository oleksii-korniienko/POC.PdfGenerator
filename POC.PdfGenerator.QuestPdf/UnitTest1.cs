using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace POC.PdfGenerator.QuestPdf;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Settings.License = LicenseType.Community;

        Document.Create(d =>
            {
                d.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);

                    page.Header()
                        .Padding(20)
                        .Row(row =>
                        {
                            row.ConstantItem(450)
                                .Text("Some text")
                                .SemiBold().FontSize(24).FontColor(Colors.Blue.Medium);

                            row.ConstantItem(50)
                                .Image(File.ReadAllBytes("logo.png"));
                        });


                    page.Content()
                        .PaddingHorizontal(20)
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            x.Item().Text(Placeholders.LoremIpsum());
                        });
                    
                    page.Footer()
                        .PaddingRight(20)
                        .PaddingLeft(20)
                        .PaddingBottom(35)
                        .Row(row =>
                        {
                            row.ConstantItem(400)
                                .Text("Thank you for choosing iDeals!");

                            row.ConstantItem(125)
                                .Text(DateTime.Now.ToString("D"));
                        });
                });
            })
            .GeneratePdf("output.pdf");
    }
}