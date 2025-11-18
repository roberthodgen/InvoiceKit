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
                .AddText(
                    "Simple Table",
                    style => style with { Text = style.Text with { FontPath = "Open Sans/Bold", FontSize = 24f, }, })
                .AddText("The table below contains two rows and 3 equally spaced columns with row separators.")
                .AddTable(table => table
                    .UseEquallySpaceColumns()
                    .AddRowSeparators()
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
                .AddText(
                    "Fixed Width Columns",
                    style => style with { Text = style.Text with { FontPath = "Open Sans/Bold", FontSize = 24f, }, })
                .AddText("The table below contains 3 fixed-percentage width columns.")
                .AddTable(table => table
                    .UseFixedColumnWidths(
                    [
                        ColumnWidthPercent.FromPercent(75),
                        ColumnWidthPercent.FromPercent(10),
                        ColumnWidthPercent.FromPercent(15),
                    ])
                    .AddHeader(header => header
                        .AddCell(cell => cell.AddText(("Description")))
                        .AddCell(cell => cell.AddText("Qty"))
                        .AddCell(cell => cell.AddText("Price")))
                    .AddRow(row => row
                        .AddCell(cell => cell.AddText("Product One - The quick brown fox jumps over the lazy dog."))
                        .AddCell(cell => cell.AddText("1"))
                        .AddCell(cell => cell.AddText("$ 10.00")))
                    .AddRow(row => row
                        .AddCell(cell => cell.AddText("Product Two - The quick brown fox jumps over the lazy dog."))
                        .AddCell(cell => cell.AddText("1"))
                        .AddCell(cell => cell.AddText("$ 20.00"))))
                .AddText(
                    "Consistent Row Heights",
                    style => style with { Text = style.Text with { FontPath = "Open Sans/Bold", FontSize = 24f, }, })
                .AddText("All rows are the same height with row separators.")
                .AddTable(table => table
                    .UseFixedColumnWidths(
                    [
                        ColumnWidthPercent.FromPercent(50),
                        ColumnWidthPercent.FromPercent(25),
                        ColumnWidthPercent.FromPercent(25),
                    ])
                    .AddRowSeparators()
                    .AddHeader(header => header
                        .AddCell(cell => cell.AddText(("Description")))
                        .AddCell(cell => cell.AddText("Qty"))
                        .AddCell(cell => cell.AddText("Price")))
                    .AddRow(row => row
                        .AddCell(cell => cell.AddText("Product One - The quick brown fox jumps over the lazy dog."))
                        .AddCell(cell => cell.AddText("1"))
                        .AddCell(cell => cell.AddText("$ 10.00")))
                    .AddRow(row => row
                        .AddCell(cell => cell.AddText("Product Two - The quick brown fox jumps over the lazy dog."))
                        .AddCell(cell => cell.AddText("1"))
                        .AddCell(cell => cell.AddText("$ 20.00")))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
