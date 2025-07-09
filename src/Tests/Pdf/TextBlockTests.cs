namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using InvoiceKit.Pdf.Layouts.Text;
using InvoiceKit.Pdf.Styles.Text;
using SkiaSharp;
using Xunit.Abstractions;

public class TextBlockTests(ITestOutputHelper testOutputHelper)
{
    private const string fileName = "text-block.pdf";

    [Fact]
    public async Task TextBlock_NoMarginNoParagraphSpacing_Test()
    {
        File.Delete(fileName);

        using var builder = PdfDocument.UsLetter;
        await builder
            // .DisplayLayoutGuidelines()
            .AddBlock(doc => new TextBlock(doc.DefaultTextStyle)
                .AddLine("Test Document", text => text.Font("Open Sans/Bold").FontSize(24f))
                .AddLine(
                    "Sample document for text block layout and rendering.",
                    text => text.Color(SKColors.DimGray)))
            .AddBlock(doc => new TextBlock(doc.DefaultTextStyle)
                .AddLine("Default: The quick brown fox jumps over the lazy dog."))
            .AddBlock(_ => new TextBlock(
                new TextStyle
                {
                    ParagraphSpacing = new ParagraphSpacing
                    {
                        After = 2f,
                    },
                }).AddLine("Spacing after: The quick brown fox jumps over the lazy dog."))
            .AddBlock(_ => new TextBlock(
                    new TextStyle
                    {
                        LineHeight = 1f,
                        ParagraphSpacing = new ParagraphSpacing
                        {
                            Before = 1f,
                            After = 1f,
                        },
                    })
                .AddLine("None: The quick brown fox jumps over the lazy dog."))
            .AddBlock(_ => new TextBlock(
                new TextStyle
                {
                    ParagraphSpacing = new ParagraphSpacing
                    {
                        Before = 2f,
                        After = 1f,
                    },
                }).AddLine("Spacing before: The quick brown fox jumps over the lazy dog."))
            .AddBlock(_ => new TextBlock(
                new TextStyle
                {
                    LineHeight = 2f,
                }).AddLine("Double line: The quick brown fox jumps over the lazy dog."))
            .AddBlock(doc => new TextBlock(doc.DefaultTextStyle)
                .AddLine(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer pharetra neque nec sapien pharetra gravida. Aenean quis sapien enim. In semper quis nisi laoreet sollicitudin. Morbi vehicula massa sed erat egestas tempus. Duis tincidunt odio elit, a efficitur est dictum quis. Cras egestas ante et mi vulputate, in dapibus nisi suscipit. Sed sodales nibh leo, eu hendrerit nibh semper non. Praesent id nunc sed eros aliquam tristique eget ut erat. Pellentesque dignissim mattis justo sed viverra."))
            .AddBlock(doc => new TextBlock(doc.DefaultTextStyle)
                .AddLine(
                    "Aenean dui mauris, sagittis ac dapibus et, commodo sit amet tortor. Vestibulum porttitor feugiat sem, at fermentum ex laoreet at. Curabitur lobortis finibus tincidunt. Morbi mattis quam nec nulla dapibus luctus. Quisque mattis nunc risus, quis pellentesque felis molestie ut. Donec rhoncus accumsan aliquam. Nulla vestibulum dolor eget sapien aliquet, a volutpat urna efficitur. Proin erat lorem, auctor placerat tincidunt eu, porta nec dui. Ut dapibus tortor sit amet tortor tristique, tempus pharetra nunc sagittis. Aenean pretium vulputate quam, at rutrum nisi commodo eu. Curabitur vitae erat nec lacus placerat suscipit iaculis et nunc."))
            .SaveAsPdfAsync(fileName);

        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
