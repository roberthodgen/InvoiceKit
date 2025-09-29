namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using InvoiceKit.Pdf.Containers.Tables;
using Xunit.Abstractions;

public class TableTests(ITestOutputHelper testOutputHelper)
{
    [Fact(Skip = "WIP")]
    public void Table_EndToEnd_Test()
    {
        const string fileName = "table-test.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(vStack => vStack
            .AddText(table => table
                .Font("Open Sans/Bold")
                .FontSize(24f)
                .WithText("Simple Table"))
            .AddText(table => table
                .WithText("The table below contains two rows and 3 equally spaced columns with row separators."))
            .AddTable(table => table
                .UseEquallySpaceColumns()
                .AddRowSeparators()
                .AddHeader(header => header
                    .AddCell(cell => cell.WithText(text => text.WithText("Description")))
                    .AddCell(cell => cell.WithText(text => text.WithText("Qty")))
                    .AddCell(cell => cell.WithText(text => text.WithText("Price"))))
                .AddRow(row => row.AddCell(cell => cell.WithText(text => text.WithText("Product One")))
                    .AddCell(cell => cell.WithText(text => text.WithText("1")))
                    .AddCell(cell => cell.WithText(text => text.WithText("$ 10.00"))))
                .AddRow(row => row.AddCell(cell => cell.WithText(text => text.WithText("Product Two")))
                    .AddCell(cell => cell.WithText(text => text.WithText("1")))
                    .AddCell(cell => cell.WithText(text => text.WithText("$ 20.00")))))
            .AddText(table => table
                .Font("Open Sans/Bold")
                .FontSize(24f)
                .WithText("Fixed Width Columns"))
            .AddText(table => table
                .WithText("The table below contains 3 fixed-percentage width columns."))
            .AddTable(table => table
                .UseFixedColumnWidths(
                [
                    ColumnWidthPercent.FromPercent(75),
                    ColumnWidthPercent.FromPercent(10),
                    ColumnWidthPercent.FromPercent(15),
                ])
                .AddHeader(header => header
                    .AddCell(cell => cell.WithText(text => text.WithText("Description")))
                    .AddCell(cell => cell.WithText(text => text.WithText("Qty")))
                    .AddCell(cell => cell.WithText(text => text.WithText("Price"))))
                .AddRow(row => row
                    .AddCell(cell => cell.WithText(text => text.WithText("Product One - The quick brown fox jumps over the lazy dog.")))
                    .AddCell(cell => cell.WithText(text => text.WithText("1")))
                    .AddCell(cell => cell.WithText(text => text.WithText("$ 10.00"))))
                .AddRow(row => row
                    .AddCell(cell => cell.WithText(text => text.WithText("Product Two - The quick brown fox jumps over the lazy dog.")))
                    .AddCell(cell => cell.WithText(text => text.WithText("1")))
                    .AddCell(cell => cell.WithText(text => text.WithText("$ 20.00")))))
            .AddText(table => table
                .Font("Open Sans/Bold")
                .FontSize(24f)
                .WithText("Consistent Row Heights"))
            .AddText(table => table
                .WithText("All rows are the same height with row separators."))
            .AddTable(table => table
                .UseFixedColumnWidths(
                [
                    ColumnWidthPercent.FromPercent(50),
                    ColumnWidthPercent.FromPercent(25),
                    ColumnWidthPercent.FromPercent(25),
                ])
                .AddRowSeparators()
                .AddHeader(header => header
                    .AddCell(cell => cell.WithText(text => text.WithText("Description")))
                    .AddCell(cell => cell.WithText(text => text.WithText("Qty")))
                    .AddCell(cell => cell.WithText(text => text.WithText("Price"))))
                .AddRow(row => row
                    .AddCell(cell => cell.WithText(text => text.WithText("Product One - The quick brown fox jumps over the lazy dog.")))
                    .AddCell(cell => cell.WithText(text => text.WithText("1")))
                    .AddCell(cell => cell.WithText(text => text.WithText("$ 10.00"))))
                .AddRow(row => row
                    .AddCell(cell => cell.WithText(text => text.WithText("Product Two - The quick brown fox jumps over the lazy dog.")))
                    .AddCell(cell => cell.WithText(text => text.WithText("1")))
                    .AddCell(cell => cell.WithText(text => text.WithText("$ 20.00"))))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
