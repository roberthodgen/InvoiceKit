namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using Xunit.Abstractions;

public class DocumentTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void HStack_EndToEnd_Test()
    {
        const string fileName = "hstack-test.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .DefaultFont("Open Sans/Regular")
            .WithVStack(main => main
                .AddText(text => text
                    .Font("Open Sans/Bold")
                    .FontSize(24)
                    .WithText("Invoice #123"))
                .AddText(text => text.WithText("Invoice No.: 123\nDue: July 1, 2025"))
                .AddHStack(column => column
                    .AddHStack(hStack => hStack
                        .AddHStack(cols => cols
                            .AddImage(image =>
                                image.WithSvgImage(Path.Combine(Directory.GetCurrentDirectory(), "Images/circle.svg")))
                            .AddVStack(lines => lines
                                .AddText(text => text
                                    .Font("Open Sans/Bold")
                                    .WithText("My Co."))
                                .AddText(text => text.WithText("123 Main Street\nAnytown, XX 12345"))))
                        .AddVStack(lines => lines.AddText(text => text
                                .Font("Open Sans/Bold")
                                .WithText("Customer Co."))
                            .AddText(text => text.WithText("999 Billto Lane\nSometime, YY 98765"))))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void Image_EndToEnd_Test()
    {
        const string fileName = "image-test.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .DefaultFont("Open Sans/Regular")
            .WithVStack(vStack => vStack
                .AddHStack(column => column
                    .AddImage(image =>
                        image.WithSvgImage(Path.Combine(Directory.GetCurrentDirectory(), "Images/circle.svg")))
                    .AddVStack(stack => stack
                        .AddText(text => text
                            .Font("Open Sans/Bold")
                            .WithText("Customer Co."))
                        .AddText(text => text
                            .WithText("Invoice No.: 123\nDue: July 1, 2025")))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
