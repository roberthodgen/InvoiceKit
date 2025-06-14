namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using InvoiceKit.Pdf.Layouts.Tables;
using Xunit.Abstractions;

public class TableTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Table_EndToEnd_Test()
    {
        const string fileName = "table.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            .DefaultFont("Open Sans/Regular")
            .AddBlock(doc => new TableLayoutBuilder(doc.DefaultTextStyle)
                .AddHeader(header => header
                    .AddCell(cell => cell.AddText("Description"))
                    .AddCell(cell => cell.AddText("Qty"))
                    .AddCell(cell => cell.AddText("Price")))
                .AddRow(row => row.AddCell(cell => cell.AddText("Product One"))
                    .AddCell(cell => cell.AddText("1"))
                    .AddCell(cell => cell.AddText("$ 10.00")))
                .AddRow(row => row.AddCell(cell => cell.AddText("Product Two"))
                    .AddCell(cell => cell.AddText("1"))
                    .AddCell(cell => cell.AddText("$ 20.00"))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {fileName}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
