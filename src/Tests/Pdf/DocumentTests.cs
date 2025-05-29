namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using SkiaSharp;
using Xunit.Abstractions;

public class DocumentTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Sample()
    {
        const string fileName = "test.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocumentBuilder.UsLetter;
        var pdfBytes = builder
            .UseColumns(columns =>
            {
                columns
                    .AddColumn(col => col.AddTextLine(
                        "Test Co.",
                        text => text.FontSize(24)
                            .Color(SKColors.Navy)
                            // .FontWeight(SKFontStyleWeight.SemiBold)
                        ))
                    .AddColumn(col => col
                        .AddTextLine("Customer Co.")
                        .AddTextLine("Invoice No.: 123")
                        .AddTextLine("Due: July 1, 2025"));
            })
            .UseTable(table =>
            {
                table.AddHeader(header =>
                {
                    header.AddCell(cell => cell.Text("Description"));
                    header.AddCell(cell => cell.Text("Qty"));
                    header.AddCell(cell => cell.Text("Price"));
                });
            })
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {fileName}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
