namespace InvoiceKit.Tests.Pdf.Layouts;

using InvoiceKit.Pdf;
using Xunit.Abstractions;

public class HStackTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void EqualWidth_Test()
    {
        const string fileName = "hStack-equal-width.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .AddText(fileName, style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddHorizontalRule()
                .AddVStack(table => table
                    .WithHeader(header => header
                        .AddHStack(headerRow => headerRow
                            .WithDefaultStyle(style => style with { FontPath = "Open Sans/SemiBold", })
                            .AddText("Column 1")
                            .AddText("Column 2")
                            .AddText("Column 3")))
                    .AddHStack(row1 => row1.AddText("Row 1 Col 1").AddText("Row 1 Col 2").AddText("Row 1 Col 3"))
                    .AddHStack(row2 => row2.AddText("Row 2 Col 1").AddText("Row 2 Col 2").AddText("Row 2 Col 3"))
                    .AddHStack(row3 => row3.AddText("Row 3 Col 1").AddText("Row 3 Col 2").AddText("Row 3 Col 3"))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void Percentage_Test()
    {
        const string fileName = "hStack-column-percentages.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .AddText(fileName, style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddHorizontalRule()
                .AddVStack(table => table
                    .WithColumnWidths(widths => widths
                        .AddColumnPercent(20)
                        .AddColumnPercent(60)
                        .AddColumnPercent(20))
                    .WithHeader(header => header
                        .AddHStack(headerRow => headerRow
                            .WithDefaultStyle(style => style with { FontPath = "Open Sans/SemiBold", })
                            .AddText("Column 1")
                            .AddText("Column 2")
                            .AddText("Column 3")))
                    .AddHStack(row1 => row1.AddText("Row 1 Col 1").AddText("Row 1 Col 2").AddText("Row 1 Col 3"))
                    .AddHStack(row2 => row2.AddText("Row 2 Col 1").AddText("Row 2 Col 2").AddText("Row 2 Col 3"))
                    .AddHStack(row3 => row3.AddText("Row 3 Col 1").AddText("Row 3 Col 2").AddText("Row 3 Col 3"))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void Point_Test()
    {
        const string fileName = "hStack-column-points.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .AddText(fileName, style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddHorizontalRule()
                .AddVStack(table => table
                    .WithColumnWidths(widths => widths
                        .AddColumnPoints(100)
                        .AddColumnPoints(250)
                        .AddColumnPoints(100))
                    .WithHeader(header => header
                        .AddHStack(headerRow => headerRow
                            .WithDefaultStyle(style => style with { FontPath = "Open Sans/SemiBold", })
                            .AddText("Column 1")
                            .AddText("Column 2")
                            .AddText("Column 3")))
                    .AddHStack(row1 => row1.AddText("Row 1 Col 1").AddText("Row 1 Col 2").AddText("Row 1 Col 3"))
                    .AddHStack(row2 => row2.AddText("Row 2 Col 1").AddText("Row 2 Col 2").AddText("Row 2 Col 3"))
                    .AddHStack(row3 => row3.AddText("Row 3 Col 1").AddText("Row 3 Col 2").AddText("Row 3 Col 3"))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void HStack_ToLayout_ThrowsWhenUnequalColumnsAndWidths()
    {
        using var builder = PdfDocument.UsLetter;
        var exception = Should.Throw<ApplicationException>(() =>
            builder.WithVStack(root => root
                .WithColumnWidths(widths => widths
                    .AddColumnPercent(50)
                    .AddColumnPercent(50))
                .AddHStack(table => table
                    .AddText("Column1")
                    .AddText("Column2")
                    .AddText("Column3")))
            .Build());
        exception.Message.ShouldContain("Column");
    }

    [Fact]
    public void HStack_ToLayout_ThrowsWhenPointsOversize()
    {
        using var builder = PdfDocument.UsLetter;
        var exception = Should.Throw<ApplicationException>(() =>
            builder.WithVStack(root => root
                    .WithColumnWidths(widths => widths
                        .AddColumnPoints(100)
                        .AddColumnPoints(300)
                        .AddColumnPoints(100))
                    .AddHStack(table => table
                        .AddText("Column1")
                        .AddText("Column2")
                        .AddText("Column3")))
                .Build());
        exception.Message.ShouldContain("Column");
    }
}
