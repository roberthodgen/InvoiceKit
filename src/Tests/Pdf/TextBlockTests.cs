namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using SkiaSharp;
using Xunit.Abstractions;

public class TextBlockTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TextBlock_EndToEnd_Test()
    {
        const string fileName = "text-block-test.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            .DisplayLayoutGuidelines()
            .WithVStack(vStack => vStack
                .AddText(text => text
                    .Font("Open Sans/Bold")
                    .FontSize(24f)
                    .WithText("Test Document"))
                .AddText(text => text
                    .Color(SKColors.DimGray)
                    .WithText("Sample document for text block layout and rendering."))
                .AddText(text => text.WithText("Default: The quick brown fox jumps over the lazy dog."))
                .AddText(text => text
                    .ParagraphSpacing(after: 2f)
                    .WithText("Spacing after: The quick brown fox jumps over the lazy dog."))
                .AddText(text => text
                    .ParagraphSpacing(before: 1f, after: 1f)
                    .LineHeight(1f)
                    .WithText("None: The quick brown fox jumps over the lazy dog."))
                .AddText(text => text
                    .ParagraphSpacing(before: 2f, after: 1f)
                    .WithText("Spacing before: The quick brown fox jumps over the lazy dog."))
                .AddText(text => text
                    .LineHeight(2f)
                    .WithText("Double line: The quick brown fox jumps over the lazy dog."))
                .AddText(text => text
                    .WithText("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer pharetra neque nec sapien pharetra gravida. Aenean quis sapien enim. In semper quis nisi laoreet sollicitudin. Morbi vehicula massa sed erat egestas tempus. Duis tincidunt odio elit, a efficitur est dictum quis. Cras egestas ante et mi vulputate, in dapibus nisi suscipit. Sed sodales nibh leo, eu hendrerit nibh semper non. Praesent id nunc sed eros aliquam tristique eget ut erat. Pellentesque dignissim mattis justo sed viverra."))
                .AddText(text => text
                    .WithText("Aenean dui mauris, sagittis ac dapibus et, commodo sit amet tortor. Vestibulum porttitor feugiat sem, at fermentum ex laoreet at. Curabitur lobortis finibus tincidunt. Morbi mattis quam nec nulla dapibus luctus. Quisque mattis nunc risus, quis pellentesque felis molestie ut. Donec rhoncus accumsan aliquam. Nulla vestibulum dolor eget sapien aliquet, a volutpat urna efficitur. Proin erat lorem, auctor placerat tincidunt eu, porta nec dui. Ut dapibus tortor sit amet tortor tristique, tempus pharetra nunc sagittis. Aenean pretium vulputate quam, at rutrum nisi commodo eu. Curabitur vitae erat nec lacus placerat suscipit iaculis et nunc.")))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
