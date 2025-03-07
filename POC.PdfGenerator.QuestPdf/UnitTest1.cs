using QuestPDF;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace POC.PdfGenerator.QuestPdf;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Settings.License = LicenseType.Community;

        FontManager.RegisterFont(File.OpenRead("GaMaamli-Regular.ttf")); // use file name

        
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
                                .Text("List of supported languages")
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

                            "Chinese: 浅色模式; Ukrainian: Укрзалізниця; Russian: Героев упа 73В; English: Anytown, WA 99999; Korean: 어두운 모드; Japanese: ダークモード; Arabic: وضع الظلام; Hebrew: מצב כהה; Spanish: Modo oscuro; French: Mode sombre; German: Dunkles Design; Italian: Modalità scura; Portuguese: Modo escuro; turkish: Koyu mod; Swedish: Mörkt läge; Netherlands: Donkere modus;"
                                .Split(";")
                                .ToList()
                                .ForEach(a => x.Item().Text(a.Trim()).FontFamily("Ga Maamli")
                                );
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
            .ShowInPreviewer();
    }
}