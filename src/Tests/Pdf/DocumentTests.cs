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
            .DisplayLayoutGuidelines()
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
                        .AddVStack(_ => { })
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

    [Fact]
    public void HeaderAndFooter_SinglePageTest()
    {
        const string fileName = "HeaderAndFooterSinglePage.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            .DisplayLayoutGuidelines()
            .DefaultFont("Open Sans/Regular")
            .WithVStack(vStack => vStack
                .WithHeader(header => header.AddText(text => text.WithText("This is the header.")))
                .WithFooter(footer => footer.AddText(text => text.WithText("This is the footer.")))
                .AddText(text => text.WithText("This is inside the first page's body."))
                .AddText(text => text.WithText("This is inside the first page's body."))
                .AddText(text => text.WithText("This is inside the first page's body.")))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
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
            .DefaultFont("Open Sans/Regular")
            .WithVStack(vStack => vStack
                .WithHeader(header => header.AddText(text => text.WithText("This is the header.")))
                .WithFooter(footer => footer.AddText(text => text.WithText("This is the footer.")))
                .AddText(text => text.WithText("This is inside the first page's body."))
                .AddText(text => text.WithText("This is inside the first page's body."))
                .AddText(text => text.WithText("This is inside the first page's body."))
                .AddPageBreak()
                .AddText(text => text.WithText("This is inside the second page's body."))
                .AddText(text => text.WithText("This is inside the second page's body."))
                .AddText(text => text.WithText("This is inside the second page's body."))
                .AddText(text => text.WithText("This is inside the second page's body."))
                .AddText(text => text.WithText("This is inside the second page's body."))
                .AddText(text => text.WithText("This is inside the second page's body."))
                .AddSpacing(475)
                .AddText(text => text.WithText("This is inside the second page's body."))
                .AddText(text => text.WithText("This is inside the second page's body."))
                .AddText(text => text.WithText("This is inside the second page's body."))
                .AddText(text => text.WithText("This is inside the second page's body."))
                .AddText(text => text.WithText("This is inside the second page's body."))
                .AddText(text => text.WithText("This is inside the second page's body.")))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
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
            .DefaultFont("Open Sans/Regular")
            .WithVStack(vStack => vStack
                .AddHStack(hStack => hStack
                    .AddText(text => text.FontSize(24f).WithText("Invoice #123"))
                    .AddSpacing()
                    .AddText(text => text.FontSize(11f).ParagraphSpacing(3f).WithText("September 29, 2025")))
                .AddHorizontalRule()
                .AddSpacing(20f)
                .AddHStack(column => column
                    .AddVStack(stack => stack
                        .AddText(text => text.FontSize(11f).LineHeight(.5f).WithText("Customer LLC"))
                        .AddText(text => text.FontSize(11f).LineHeight(.5f).WithText("321 Curvy Rd."))
                        .AddText(text => text.FontSize(11f).LineHeight(.5f).WithText("customer@mail.com"))
                        .AddText(text => text.FontSize(11f).LineHeight(.5f).WithText("(800) 555 - 4444")))
                    .AddVStack(stack => stack
                        .AddText(text => text.FontSize(11f).LineHeight(.5f).WithText("Company LLC"))
                        .AddText(text => text.FontSize(11f).LineHeight(.5f).WithText("121 Bumpy Street"))
                        .AddText(text => text.FontSize(11f).LineHeight(.5f).WithText("company@mail.com"))
                        .AddText(text => text.FontSize(11f).LineHeight(.5f).WithText("(800) 444 - 5555"))))
                .AddSpacing(20f)
                .AddText(text => text.FontSize(18f).WithText("IT Services"))
                .AddText(text => text.FontSize(11f)
                    .WithText("\t    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut euismod gravida ligula, ac interdum sapien viverra eget. Fusce pellentesque enim tristique interdum aliquet. Nulla quam ex, elementum at lorem ut, pellentesque luctus purus. Curabitur feugiat id tortor ut rutrum. Integer id velit suscipit, maximus nisi ac, sollicitudin odio. Maecenas imperdiet lacus velit, id aliquet sapien consectetur faucibus. Nunc lobortis gravida dui, cursus condimentum ex gravida id. Cras at erat quis mi tempor tempus. Nullam consequat velit non interdum vestibulum. Nulla quis magna ac augue molestie luctus sit amet at dolor. Integer aliquam quam quis lacinia scelerisque. Nunc ante velit, tempor quis luctus id, volutpat non enim. Suspendisse rhoncus imperdiet diam, at semper tellus congue at. In sit amet gravida est, nec viverra erat. Phasellus volutpat blandit ipsum, in condimentum nunc congue ut. Sed lacinia finibus elit eget molestie."))
                .AddSpacing(20f)
                .AddText(text => text.FontSize(18f).WithText("Add a table here")))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: {Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
