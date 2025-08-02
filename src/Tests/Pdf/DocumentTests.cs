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
                .AddVStack(stack => stack.AddTextBlock(text => text
                    .AddLine(
                        "Invoice #123",
                        style => style.Font("Open Sans/Bold").FontSize(24))
                    .AddLine("Invoice No.: 123")
                    .AddLine("Due: July 1, 2025")))
                .AddHStack(column => column
                    .AddHStack(hStack => hStack
                        .AddImage(image => image.WithSvgImage(Path.Combine(Directory.GetCurrentDirectory(), "Images/circle.svg")))
                        .AddTextBlock(text => text
                            .AddLine("My Co.", style => style.Font("Open Sans/Bold"))
                            .AddLine("123 Main Street")
                            .AddLine("Anytown, XX 12345")))
                    .AddTextBlock(text => text
                        .AddLine("Customer Co.", style => style.Font("Open Sans/Bold"))
                        .AddLine("999 Billto Lane")
                        .AddLine("Sometime, YY 98765"))))
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
                    .AddTextBlock(text => text
                        .AddLine("Customer Co.", style => style.Font("Open Sans/Bold"))
                        .AddLine("Invoice No.: 123")
                        .AddLine("Due: July 1, 2025"))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
