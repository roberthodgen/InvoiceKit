namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using SkiaSharp;
using Xunit.Abstractions;

public class TextBlockTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TextBlock_NoMarginNoParagraphSpacing_Test()
    {
        const string fileName = "text-block.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            .DisplayLayoutGuidelines()
            .AddTextBlock(text => text
                .AddLine("Test Document", line => line.Font("Open Sans/Bold").FontSize(24f))
                .AddLine(
                    "Sample document for text block layout and rendering.",
                    line => line.Color(SKColors.DimGray)))
            .AddTextBlock(text => text
                .AddLine("Default: The quick brown fox jumps over the lazy dog."))
            .AddTextBlock(text => text
                .ParagraphSpacing(after: 2f)
                .AddLine("Spacing after: The quick brown fox jumps over the lazy dog."))
            .AddTextBlock(text => text
                .ParagraphSpacing(before: 1f, after: 1f)
                .LineHeight(1f)
                .AddLine("None: The quick brown fox jumps over the lazy dog."))
            .AddTextBlock(text => text
                .ParagraphSpacing(before: 2f, after: 1f)
                .AddLine("Spacing before: The quick brown fox jumps over the lazy dog."))
            .AddTextBlock(text => text
                .LineHeight(2f)
                .AddLine("Double line: The quick brown fox jumps over the lazy dog."))
            .AddTextBlock(text => text
                .AddLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer pharetra neque nec sapien pharetra gravida. Aenean quis sapien enim. In semper quis nisi laoreet sollicitudin. Morbi vehicula massa sed erat egestas tempus. Duis tincidunt odio elit, a efficitur est dictum quis. Cras egestas ante et mi vulputate, in dapibus nisi suscipit. Sed sodales nibh leo, eu hendrerit nibh semper non. Praesent id nunc sed eros aliquam tristique eget ut erat. Pellentesque dignissim mattis justo sed viverra."))
            .AddTextBlock(text => text
                .AddLine("Aenean dui mauris, sagittis ac dapibus et, commodo sit amet tortor. Vestibulum porttitor feugiat sem, at fermentum ex laoreet at. Curabitur lobortis finibus tincidunt. Morbi mattis quam nec nulla dapibus luctus. Quisque mattis nunc risus, quis pellentesque felis molestie ut. Donec rhoncus accumsan aliquam. Nulla vestibulum dolor eget sapien aliquet, a volutpat urna efficitur. Proin erat lorem, auctor placerat tincidunt eu, porta nec dui. Ut dapibus tortor sit amet tortor tristique, tempus pharetra nunc sagittis. Aenean pretium vulputate quam, at rutrum nisi commodo eu. Curabitur vitae erat nec lacus placerat suscipit iaculis et nunc."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
