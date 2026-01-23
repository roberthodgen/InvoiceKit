namespace InvoiceKit.Tests.Pdf.Layouts;

using InvoiceKit.Pdf;
using Xunit.Abstractions;

public class HStackTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void EqualWidth_Test()
    {
        const string fileName = "hstack-equal-width.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .AddText(
                    fileName,
                    style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddHorizontalRule()
                .AddVStack(table =>
                    table.AddHStack(headerRow =>
                            headerRow
                                .WithDefaultStyle(style => style with { FontPath = "Open Sans/SemiBold", })
                                .AddText("Column 1")
                                .AddText("Column 2")
                                .AddText("Column 3"))
                        .AddHStack(row1 =>
                            row1.AddText("Row 1 Col 2").AddText("Row 1 Col 2").AddText("Row 1 Col 3"))
                        .AddHStack(row2 =>
                            row2.AddText("Row 2 Col 2").AddText("Row 2 Col 2").AddText("Row 2 Col 3"))
                        .AddHStack(row3 =>
                            row3.AddText("Row 3 Col 2").AddText("Row 3 Col 2").AddText("Row 3 Col 3"))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
