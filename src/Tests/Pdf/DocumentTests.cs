namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using InvoiceKit.Pdf.Layouts.Text;
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
        var defaultTextStyle = builder.DefaultTextStyle;
        var pdfBytes = builder
            .DisplayLayoutGuidelines()
            .DefaultFont("Open Sans/Regular")
            .AddColumnStack(stack => stack
                .AddColumn(
                    new TextBlock(defaultTextStyle)
                        .AddLine("Test Document", text => text.Font("Open Sans/Bold").FontSize(24))
                        .AddLine("123 Main Street")
                        .AddLine("Anytown, XX 12345"))
                .AddColumn(
                    new TextBlock(defaultTextStyle)
                        .AddLine("Customer Co.", text => text.Font("Open Sans/Bold"))
                        .AddLine("Invoice No.: 123")
                        .AddLine("Due: July 1, 2025")))
            .AddHorizontalRule()
            .AddImageBlock(Path.Combine(Directory.GetCurrentDirectory(), "Images/circle.svg"))
            .AddTextBlock(text => text
                .AddLine(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer pharetra neque nec sapien pharetra gravida. Aenean quis sapien enim. In semper quis nisi laoreet sollicitudin. Morbi vehicula massa sed erat egestas tempus. Duis tincidunt odio elit, a efficitur est dictum quis. Cras egestas ante et mi vulputate, in dapibus nisi suscipit. Sed sodales nibh leo, eu hendrerit nibh semper non. Praesent id nunc sed eros aliquam tristique eget ut erat. Pellentesque dignissim mattis justo sed viverra. Aenean dui mauris, sagittis ac dapibus et, commodo sit amet tortor.")
                .AddLine(
                    "Nulla id aliquam orci. Etiam id magna eget metus bibendum sodales. Suspendisse maximus ullamcorper ipsum. Quisque vel sagittis sapien. Integer finibus tellus eu rutrum pretium. Sed congue auctor posuere. Pellentesque tortor orci, molestie in turpis non, mattis pharetra eros. Aliquam vitae lacus sit amet augue varius auctor. Aenean ipsum diam, sodales sit amet nibh scelerisque, pellentesque sagittis dui.")
                .AddLine(
                    "Integer ac placerat neque. Vivamus tempus felis sodales lacus pretium, ut finibus nibh porttitor. Nullam eleifend risus sit amet porttitor congue. Nullam a sem lacinia est tincidunt feugiat. Vestibulum sed mi pulvinar lectus maximus congue at nec justo. Quisque enim tortor, ultrices ut suscipit sed, pulvinar vitae turpis. Vestibulum a quam ligula."))
            .AddTextBlock(text => text.AddLine("Before default page spacing of 5f."))
            .AddSpacingBlock()
            .AddTextBlock(text => text.AddLine("After default page spacing of 5f."))
            .AddTextBlock(text => text.AddLine("Before custom page spacing of 3f."))
            .AddSpacingBlock(3f)
            .AddTextBlock(text => text.AddLine("After custom page spacing of 3f."))
            .AddPageBreak()
            .AddTextBlock(text => text
                .AddLine("New page?"))
            .AddTextBlock(text => text
                .AddLine(
                    "Suspendisse dictum faucibus justo, sit amet sollicitudin orci fermentum ac. Etiam placerat velit lacus, eget gravida magna luctus vitae. Donec facilisis nibh nulla, at mattis mauris interdum eleifend. Donec euismod enim commodo, porta enim eget, sollicitudin est. Morbi imperdiet tortor eget ex semper, sed viverra augue tincidunt. Pellentesque non scelerisque nisi, sit amet lacinia leo. Nam quis purus vitae eros tempor tristique eu vel libero. Nam et quam feugiat, placerat nunc sit amet, egestas velit. In pellentesque commodo enim, a ultrices odio tincidunt in. Nullam vel quam justo."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
