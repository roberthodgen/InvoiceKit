namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using InvoiceKit.Pdf.Layouts.Images;
using InvoiceKit.Pdf.Layouts.Stacks;
using InvoiceKit.Pdf.Layouts.Text;
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
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            .DefaultFont("Open Sans/Regular")
            .AddBlock(doc => new HStack()
                .Add(new TextBlock(doc.DefaultTextStyle)
                    .AddLine("Test Co.", text => text.Color(SKColors.Navy).Font("Open Sans/Bold"))
                    .AddLine("123 Main Street")
                    .AddLine("Anytown, XX 12345"))
                .Add(new TextBlock(doc.DefaultTextStyle)
                    .AddLine("Customer Co.", text => text.Font("Open Sans/Bold"))
                    .AddLine("Invoice No.: 123")
                    .AddLine("Due: July 1, 2025")))
            .AddBlock(doc => new HStack()
                .Add(new TextBlock(doc.DefaultTextStyle)
                    .AddLine("Footer")))
            .AddBlock(doc => ImageBlock.CreateSvg(Path.Combine(Directory.GetCurrentDirectory(), "Images/circle.svg")))
            .Build();
            // .UseTable(table =>
            // {
            //     table.AddHeader(header =>
            //     {
            //         header.UseText(text => text.Font("Open Sans/Bold"))
            //             .AddCell(cell => cell.AddText("Description"))
            //             .AddCell(cell => cell.AddText("Qty"))
            //             .AddCell(cell => cell.AddText("Price"));
            //     });
            //
            //     foreach (var nthProduct in Enumerable.Range(1, 100))
            //     {
            //         table.AddRow(row =>
            //         {
            //             row.AddCell(cell => cell.AddText(
            //                     $"Product {nthProduct}",
            //                     text => text.Font("Open Sans/SemiBold")))
            //                 .AddCell(cell => cell.AddText("1"))
            //                 .AddCell(cell => cell.AddText("$ 10.00"));
            //         });
            //     }
            // })
            // .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {fileName}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
