namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using InvoiceKit.Pdf.Layouts.Tables;
using InvoiceKit.Pdf.Layouts.Text;
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
            .AddBlock(doc => new TextBlock(doc.DefaultTextStyle)
                .AddLine("Simple Table", text => text.Font("Open Sans/Bold").FontSize(24f)))
            .AddBlock(doc => new TextBlock(doc.DefaultTextStyle)
                .AddLine("The table below contains two rows and 3 equally spaced columns."))
            .AddBlock(doc => new TableLayoutBuilder(doc.DefaultTextStyle)
                .UseEquallySpaceColumns()
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
            // .AddPageBreak()
            .AddBlock(doc => new TextBlock(doc.DefaultTextStyle)
                .AddLine("Fixed Width Columns", text => text.Font("Open Sans/Bold").FontSize(24f)))
            .AddBlock(doc => new TextBlock(doc.DefaultTextStyle)
                .AddLine("The table below contains 3 fixed-percentage width columns."))
            .AddBlock(doc => new TableLayoutBuilder(doc.DefaultTextStyle)
                .UseFixedColumnWidths(
                [
                    ColumnWidthPercent.FromPercent(75),
                    ColumnWidthPercent.FromPercent(10),
                    ColumnWidthPercent.FromPercent(15),
                ])
                .AddHeader(header => header
                    .AddCell(cell => cell.AddText("Description"))
                    .AddCell(cell => cell.AddText("Qty"))
                    .AddCell(cell => cell.AddText("Price")))
                .AddRow(row =>
                    row.AddCell(cell => cell.AddText("Product One - The quick brown fox jumps over the lazy dog."))
                        .AddCell(cell => cell.AddText("1"))
                        .AddCell(cell => cell.AddText("$ 10.00")))
                .AddRow(row =>
                    row.AddCell(cell => cell.AddText("Product Two - The quick brown fox jumps over the lazy dog."))
                        .AddCell(cell => cell.AddText("1"))
                        .AddCell(cell => cell.AddText("$ 20.00"))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {fileName}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
