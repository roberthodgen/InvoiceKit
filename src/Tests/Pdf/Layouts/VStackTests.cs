namespace InvoiceKit.Tests.Pdf.Layouts;

using InvoiceKit.Pdf;
using SkiaSharp;
using Xunit.Abstractions;

public class VStackTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Simple_Test()
    {
        const string fileName = "vstack-simple.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .AddText(
                    fileName,
                    style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddHorizontalRule()
                .AddText("Lorem ipsum."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void PageBreak_Test()
    {
        const string fileName = "vstack-page-break.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .AddText(
                    fileName,
                    style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddHorizontalRule()
                .AddText("See next page...")
                .AddPageBreak()
                .AddText("Lorem ipsum."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void MultiPage_Test()
    {
        const string fileName = "vstack-multi-page.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .AddText(
                    fileName,
                    style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddHorizontalRule()
                .AddText(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec placerat, ipsum quis aliquam venenatis, purus odio gravida neque, sed viverra tellus ligula ac est. Vivamus pharetra egestas vehicula. Nulla laoreet lorem non turpis fermentum, at molestie turpis lobortis. Mauris et pretium felis, sit amet dictum elit. Proin dictum velit ut ipsum porttitor euismod. Praesent sed leo eu augue sodales malesuada. Morbi feugiat diam a velit volutpat sodales. Aliquam mollis, lacus sed egestas aliquam, diam augue auctor nisi, vel rutrum lorem dolor non odio. Morbi aliquet eget ipsum nec porta. Aenean tincidunt sapien ac lacus scelerisque laoreet. Aenean leo mi, efficitur at ullamcorper elementum, suscipit eget urna. Proin malesuada in metus id tempus. Phasellus pulvinar mi nec elit luctus pellentesque. In sit amet metus est. Nam a eleifend lorem.")
                .AddText(
                    "Ut in metus urna. Etiam nec velit commodo, bibendum turpis sed, vulputate enim. Sed rutrum aliquam libero. Maecenas eu libero ut risus aliquam cursus in eu risus. Pellentesque ac elementum dolor, sit amet mollis mi. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Integer augue augue, sodales ut lectus id, auctor consectetur lorem. Donec vitae mollis quam.")
                .AddText(
                    "Maecenas vestibulum tempus sapien, sit amet molestie metus volutpat et. Donec imperdiet diam eget urna placerat, sed pulvinar sem volutpat. Phasellus pretium, mi et malesuada sagittis, ante lorem placerat turpis, sed rutrum elit neque et felis. Suspendisse rhoncus fermentum sem, at iaculis leo. Aliquam varius quam nec hendrerit rhoncus. Mauris fermentum rhoncus congue. Sed consectetur lectus id urna iaculis, non dictum augue maximus. Aenean efficitur est nisl. Aenean dignissim dolor nec pulvinar vestibulum. Donec posuere orci tincidunt dui pretium accumsan. Quisque nec sapien eget elit maximus dapibus vitae vitae diam. Maecenas faucibus, urna et vehicula egestas, lorem velit commodo tortor, quis pharetra mi leo vitae lorem. Quisque pellentesque nunc dolor, ut sagittis metus lobortis in. Nunc lacinia iaculis odio mattis ultrices.")
                .AddText(
                    "Sed iaculis nibh massa, non vestibulum ante viverra vel. Suspendisse vehicula, augue non congue efficitur, metus risus porta ante, sollicitudin vestibulum urna enim in leo. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Ut consectetur, ante tincidunt eleifend vulputate, dui arcu cursus tellus, quis vulputate est ligula in elit. Sed maximus nunc magna, eu sodales dui dapibus a. Mauris tincidunt arcu non nisl iaculis lacinia. Pellentesque mattis laoreet mauris, at interdum ex fringilla non. Mauris enim risus, semper ut pulvinar vel, porta nec quam. Sed tincidunt bibendum scelerisque. Ut eu malesuada quam. Ut consequat euismod nisi, id facilisis nulla vulputate in. In sem velit, mattis at lorem sed, rhoncus dapibus enim. Vivamus ultrices, lorem placerat porttitor ornare, est risus dignissim lorem, eu porttitor ex mi at lorem. Cras viverra ipsum at nisi cursus, et sagittis neque porttitor. Sed fermentum id arcu sed aliquet. Suspendisse non maximus erat.")
                .AddText(
                    "Quisque euismod neque dui, in tincidunt odio lobortis sed. Quisque eget volutpat enim. Nullam commodo varius aliquam. Proin eget urna egestas dolor commodo convallis a eget dolor. Nam ultricies vitae enim sit amet pulvinar. Cras rhoncus erat sit amet consequat finibus. Curabitur mattis felis lectus, eget vulputate ipsum pharetra ac. Donec dapibus orci ipsum, sed facilisis mi rutrum nec. Quisque dignissim sit amet purus fermentum sollicitudin."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void SimpleHeader_Test()
    {
        const string fileName = "vstack-simple-header.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .WithHeader(header => header.AddText(
                    "HEADER",
                    style => style with { FontPath = "Open Sans/Bold", ForegroundColor = SKColors.Gray, }))
                .AddText(
                    fileName,
                    style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddHorizontalRule()
                .AddText("Lorem ipsum."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void SimpleFooter_Test()
    {
        const string fileName = "vstack-simple-footer.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .WithFooter(footer => footer.AddText(
                    "FOOTER",
                    style => style with { FontPath = "Open Sans/Bold", ForegroundColor = SKColors.Gray, }))
                .AddText(
                    fileName,
                    style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddHorizontalRule()
                .AddText("Lorem ipsum."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void HeaderAndFooter_Test()
    {
        const string fileName = "vstack-header-and-footer.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .WithHeader(header => header.AddText(
                    "HEADER",
                    style => style with { FontPath = "Open Sans/Bold", ForegroundColor = SKColors.Gray, }))
                .WithFooter(footer => footer.AddText(
                    "FOOTER",
                    style => style with { FontPath = "Open Sans/Bold", ForegroundColor = SKColors.Gray, }))
                .AddText(
                    fileName,
                    style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddHorizontalRule()
                .AddText("Lorem ipsum."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void MultiPageHeader_Test()
    {
        const string fileName = "vstack-multi-page-header.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(main => main
                .WithHeader(header => header.AddText(
                        fileName,
                        style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                    .AddHorizontalRule())
                .AddText(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec placerat, ipsum quis aliquam venenatis, purus odio gravida neque, sed viverra tellus ligula ac est. Vivamus pharetra egestas vehicula. Nulla laoreet lorem non turpis fermentum, at molestie turpis lobortis. Mauris et pretium felis, sit amet dictum elit. Proin dictum velit ut ipsum porttitor euismod. Praesent sed leo eu augue sodales malesuada. Morbi feugiat diam a velit volutpat sodales. Aliquam mollis, lacus sed egestas aliquam, diam augue auctor nisi, vel rutrum lorem dolor non odio. Morbi aliquet eget ipsum nec porta. Aenean tincidunt sapien ac lacus scelerisque laoreet. Aenean leo mi, efficitur at ullamcorper elementum, suscipit eget urna. Proin malesuada in metus id tempus. Phasellus pulvinar mi nec elit luctus pellentesque. In sit amet metus est. Nam a eleifend lorem.")
                .AddText(
                    "Ut in metus urna. Etiam nec velit commodo, bibendum turpis sed, vulputate enim. Sed rutrum aliquam libero. Maecenas eu libero ut risus aliquam cursus in eu risus. Pellentesque ac elementum dolor, sit amet mollis mi. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Integer augue augue, sodales ut lectus id, auctor consectetur lorem. Donec vitae mollis quam.")
                .AddText(
                    "Maecenas vestibulum tempus sapien, sit amet molestie metus volutpat et. Donec imperdiet diam eget urna placerat, sed pulvinar sem volutpat. Phasellus pretium, mi et malesuada sagittis, ante lorem placerat turpis, sed rutrum elit neque et felis. Suspendisse rhoncus fermentum sem, at iaculis leo. Aliquam varius quam nec hendrerit rhoncus. Mauris fermentum rhoncus congue. Sed consectetur lectus id urna iaculis, non dictum augue maximus. Aenean efficitur est nisl. Aenean dignissim dolor nec pulvinar vestibulum. Donec posuere orci tincidunt dui pretium accumsan. Quisque nec sapien eget elit maximus dapibus vitae vitae diam. Maecenas faucibus, urna et vehicula egestas, lorem velit commodo tortor, quis pharetra mi leo vitae lorem. Quisque pellentesque nunc dolor, ut sagittis metus lobortis in. Nunc lacinia iaculis odio mattis ultrices.")
                .AddText(
                    "Sed iaculis nibh massa, non vestibulum ante viverra vel. Suspendisse vehicula, augue non congue efficitur, metus risus porta ante, sollicitudin vestibulum urna enim in leo. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Ut consectetur, ante tincidunt eleifend vulputate, dui arcu cursus tellus, quis vulputate est ligula in elit. Sed maximus nunc magna, eu sodales dui dapibus a. Mauris tincidunt arcu non nisl iaculis lacinia. Pellentesque mattis laoreet mauris, at interdum ex fringilla non. Mauris enim risus, semper ut pulvinar vel, porta nec quam. Sed tincidunt bibendum scelerisque. Ut eu malesuada quam. Ut consequat euismod nisi, id facilisis nulla vulputate in. In sem velit, mattis at lorem sed, rhoncus dapibus enim. Vivamus ultrices, lorem placerat porttitor ornare, est risus dignissim lorem, eu porttitor ex mi at lorem. Cras viverra ipsum at nisi cursus, et sagittis neque porttitor. Sed fermentum id arcu sed aliquet. Suspendisse non maximus erat.")
                .AddText(
                    "Quisque euismod neque dui, in tincidunt odio lobortis sed. Quisque eget volutpat enim. Nullam commodo varius aliquam. Proin eget urna egestas dolor commodo convallis a eget dolor. Nam ultricies vitae enim sit amet pulvinar. Cras rhoncus erat sit amet consequat finibus. Curabitur mattis felis lectus, eget vulputate ipsum pharetra ac. Donec dapibus orci ipsum, sed facilisis mi rutrum nec. Quisque dignissim sit amet purus fermentum sollicitudin."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
