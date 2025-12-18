namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using SkiaSharp;
using Xunit.Abstractions;

public class DocumentTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void HStack_Test()
    {
        const string fileName = "hstack-test.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .AddText(
                    "Invoice #123",
                    style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddText(
                    """
                    Invoice No.: 123
                    Due: July 1, 2025
                    """
                )
                .AddHStack(stack => stack
                    .AddImage(image => image
                        .WithSvgImage(Path.Combine(Directory.GetCurrentDirectory(), "Images/circle.svg")))
                    .AddVStack(cols => cols
                        .AddText(
                            "My Co.",
                            style => style with { FontPath = "Open Sans/Bold", })
                        .AddText(
                            """
                            123 Main Street
                            Anytown, XX 12345
                            """
                        ))
                    .AddVStack(lines => lines
                        .AddText(
                            "Customer Co.",
                            style => style with { FontPath = "Open Sans/Bold", })
                        .AddText(
                            """
                            999 BillTo Lane
                            Sometime, YY 98765
                            """))
                    )
                .AddText(
                    """
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut euismod gravida ligula, ac interdum sapien viverra eget. Fusce pellentesque enim tristique interdum aliquet. Nulla quam ex, elementum at lorem ut, pellentesque luctus purus. Curabitur feugiat id tortor ut rutrum. Integer id velit suscipit, maximus nisi ac, sollicitudin odio. Maecenas imperdiet lacus velit, id aliquet sapien consectetur faucibus. Nunc lobortis gravida dui, cursus condimentum ex gravida id. Cras at erat quis mi tempor tempus. Nullam consequat velit non interdum vestibulum. Nulla quis magna ac augue molestie luctus sit amet at dolor. Integer aliquam quam quis lacinia scelerisque. Nunc ante velit, tempor quis luctus id, volutpat non enim. Suspendisse rhoncus imperdiet diam, at semper tellus congue at. In sit amet gravida est, nec viverra erat. Phasellus volutpat blandit ipsum, in condimentum nunc congue ut. Sed lacinia finibus elit eget molestie.
                    """)
            )
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
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
            .DisplayLayoutGuidelines()
            .WithVStack(vStack => vStack
                .AddHStack(column => column
                    .AddImage(image =>
                        image.WithSvgImage(Path.Combine(Directory.GetCurrentDirectory(), "Images/circle.svg")))
                    .AddVStack(stack => stack
                        .AddText("Customer Co.", style => style with { FontPath = "Open Sans/Bold", })
                        .AddText("Invoice No.: 123\nDue: July 1, 2025"))))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void HeaderAndFooter_SinglePageTest()
    {
        const string fileName = "HeaderAndFooterSinglePage.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            .DisplayLayoutGuidelines()
            .WithVStack(vStack => vStack
                .WithHeader(header => header.AddText("This is the header."))
                .WithFooter(footer => footer.AddText("This is the footer."))
                .AddText("This is inside the first page's body.")
                .AddText("This is inside the first page's body.")
                .AddText("This is inside the first page's body."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void HeaderAndFooter_MultiPageTest()
    {
        const string fileName = "HeaderAndFooterMultiPage.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            .DisplayLayoutGuidelines()
            .WithVStack(vStack => vStack
                .WithHeader(header => header.AddText("This is the header."),
                    style => style with { Margin = new Margin(5f) })
                .WithFooter(footer => footer.AddText("This is the footer."),
                    style => style with { Margin = new Margin(5f) })
                .AddText("This is inside the first page's body.")
                .AddText("This is inside the first page's body.")
                .AddText("This is inside the first page's body.")
                .AddPageBreak()
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body.")
                .AddText("This is inside the second page's body."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void Example_Document_Test()
    {
        const string fileName = "Example-Document.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            .DisplayLayoutGuidelines()
            .WithVStack(vStack => vStack
                .AddHStack(hStack => hStack
                    .AddText("Invoice #123", style => style with { FontSize = 24f, })
                    .AddSpacing()
                    .AddText("September 29, 2025"))
                .AddHorizontalRule()
                .AddSpacing(20f)
                .AddHStack(column => column
                    .AddText("""
                             Customer LLC
                             321 Curvy Rd.
                             customer@mail.com
                             (800) 555 - 4444
                             """)
                    .AddVStack(stack => stack
                        .AddText("Company LLC")
                        .AddText("121 Bumpy Street")
                        .AddText("company@mail.com")
                        .AddText("(800) 444 - 5555")))
                .AddSpacing(20f)
                .AddText("IT Services")
                .AddText(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut euismod gravida ligula, ac interdum sapien viverra eget. Fusce pellentesque enim tristique interdum aliquet. Nulla quam ex, elementum at lorem ut, pellentesque luctus purus. Curabitur feugiat id tortor ut rutrum. Integer id velit suscipit, maximus nisi ac, sollicitudin odio. Maecenas imperdiet lacus velit, id aliquet sapien consectetur faucibus. Nunc lobortis gravida dui, cursus condimentum ex gravida id. Cras at erat quis mi tempor tempus. Nullam consequat velit non interdum vestibulum. Nulla quis magna ac augue molestie luctus sit amet at dolor. Integer aliquam quam quis lacinia scelerisque. Nunc ante velit, tempor quis luctus id, volutpat non enim. Suspendisse rhoncus imperdiet diam, at semper tellus congue at. In sit amet gravida est, nec viverra erat. Phasellus volutpat blandit ipsum, in condimentum nunc congue ut. Sed lacinia finibus elit eget molestie.")
                .AddSpacing(20f)
                .AddText("Add a table here", style => style with { FontSize = 18f, }))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void MarginAndPadding_Test()
    {
        const string fileName = "MarginAndPaddingTest.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            .DisplayLayoutGuidelines()
            .WithVStack(vStack => vStack
                .AddText("Margin and Padding Test",
                    style => style with { Margin = new Margin(5f), Padding = new Padding(5f) })
                .AddHStack(hStack => hStack
                    .AddText("Left Column", style => style with { Margin = new Margin(5f), Padding = new Padding(5f) })
                    .AddImage(image =>
                            image.WithSvgImage(Path.Combine(Directory.GetCurrentDirectory(), "Images/circle.svg")),
                        style => style with
                        {
                            BackgroundColor = SKColors.BlanchedAlmond,
                            Margin = new Margin(5f),
                            Padding = new Padding(5f)
                        })
                    .AddText("Right Column",
                        style => style with { Margin = new Margin(5f), Padding = new Padding(5f) }))
                .AddText("Outside HStack", style => style with { Margin = new Margin(5f), Padding = new Padding(5f) })
            ).Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
