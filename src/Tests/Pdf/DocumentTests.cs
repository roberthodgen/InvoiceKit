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
                .AddHStack(stack => stack
                    .AddHStack(column1 => column1
                        .AddImage(image => image
                            .WithSvgImage(Path.Combine(Directory.GetCurrentDirectory(), "Images/circle.svg")))
                        .AddVStack(cols => cols
                            .AddText(text => text
                                .Font("Open Sans/Bold")
                                .WithText("My Co."))
                            .AddText(text => text
                                .WithText("123 Main Street\nAnytown, XX 12345"))))
                    .AddHStack(column2 => column2
                        .AddVStack(spacing => spacing.AddSpacing())
                        .AddVStack(lines => lines
                            .AddText(text => text
                                .Font("Open Sans/Bold")
                                .WithText("Customer Co."))
                            .AddText(text => text
                                .WithText("999 Billto Lane\nSometime, YY 98765")))))
                .AddVStack(stack => stack
                    .AddText(text => text
                        .WithText("""
                                  Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut euismod gravida ligula, ac interdum sapien viverra eget. Fusce pellentesque enim tristique interdum aliquet. Nulla quam ex, elementum at lorem ut, pellentesque luctus purus. Curabitur feugiat id tortor ut rutrum. Integer id velit suscipit, maximus nisi ac, sollicitudin odio. Maecenas imperdiet lacus velit, id aliquet sapien consectetur faucibus. Nunc lobortis gravida dui, cursus condimentum ex gravida id. Cras at erat quis mi tempor tempus. Nullam consequat velit non interdum vestibulum. Nulla quis magna ac augue molestie luctus sit amet at dolor. Integer aliquam quam quis lacinia scelerisque. Nunc ante velit, tempor quis luctus id, volutpat non enim. Suspendisse rhoncus imperdiet diam, at semper tellus congue at. In sit amet gravida est, nec viverra erat. Phasellus volutpat blandit ipsum, in condimentum nunc congue ut. Sed lacinia finibus elit eget molestie.
                                  """))))
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
