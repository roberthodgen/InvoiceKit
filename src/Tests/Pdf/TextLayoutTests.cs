namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using SkiaSharp;
using Xunit.Abstractions;

public class TextLayoutTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Text_Single_Test()
    {
        const string fileName = "text-single-page-test.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            .DisplayLayoutGuidelines()
            .WithVStack(vStack => vStack
                .AddText(
                    "Test Document",
                    style => style with { FontPath = "Open Sans/Bold", FontSize = 24f, })
                .AddText(
                    "Sample document for text block layout and rendering.",
                    style => style with { ForegroundColor = SKColors.DimGray, })
                .AddText("Default: The quick brown fox jumps over the lazy dog.")
                .AddText(
                    "Spacing after: The quick brown fox jumps over the lazy dog.",
                    style => style with { Margin = new Margin(0, 0, 0, 2f) })
                .AddText("None: The quick brown fox jumps over the lazy dog.")
                .AddText(
                    "Spacing before: The quick brown fox jumps over the lazy dog.",
                    style => style with { Margin = new Margin(0, 2f, 0, 0) })
                .AddText(
                    "Double line height: The quick brown fox jumps over the lazy dog.",
                    style => style with { LineHeight = 2f, })
                .AddText(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer pharetra neque nec sapien pharetra gravida. Aenean quis sapien enim. In semper quis nisi laoreet sollicitudin. Morbi vehicula massa sed erat egestas tempus. Duis tincidunt odio elit, a efficitur est dictum quis. Cras egestas ante et mi vulputate, in dapibus nisi suscipit. Sed sodales nibh leo, eu hendrerit nibh semper non. Praesent id nunc sed eros aliquam tristique eget ut erat. Pellentesque dignissim mattis justo sed viverra.")
                .AddText(
                    "Aenean dui mauris, sagittis ac dapibus et, commodo sit amet tortor. Vestibulum porttitor feugiat sem, at fermentum ex laoreet at. Curabitur lobortis finibus tincidunt. Morbi mattis quam nec nulla dapibus luctus. Quisque mattis nunc risus, quis pellentesque felis molestie ut. Donec rhoncus accumsan aliquam. Nulla vestibulum dolor eget sapien aliquet, a volutpat urna efficitur. Proin erat lorem, auctor placerat tincidunt eu, porta nec dui. Ut dapibus tortor sit amet tortor tristique, tempus pharetra nunc sagittis. Aenean pretium vulputate quam, at rutrum nisi commodo eu. Curabitur vitae erat nec lacus placerat suscipit iaculis et nunc."))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void Text_ForegroundAndBackground_Test()
    {
        const string fileName = "Text_ForegroundAndBackground_Test.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(vStack => vStack
                .AddText("Text_ForegroundAndBackground_Test.pdf")
                .AddHorizontalRule()
                .AddHStack(hStack => hStack
                    .AddText("Black text w/ white background.")
                    .AddText("White text w/ black background.",
                        style => style with
                        {
                            ForegroundColor = SKColors.White, BackgroundColor = SKColors.Black,
                            Padding = new Padding(5f)
                        })
                    .AddText("Cyan text w/ pink background.",
                        style => style with
                        {
                            ForegroundColor = SKColors.Cyan, BackgroundColor = SKColors.Pink,
                            Padding = new Padding(5f)
                        }))
                .AddText("Black text w/ white background.")
                .AddText("White text w/ black background.",
                    style => style with
                    {
                        ForegroundColor = SKColors.White, BackgroundColor = SKColors.Black,
                        Padding = new Padding(5f)
                    })
                .AddText("Cyan text w/ pink background.",
                    style => style with
                    {
                        ForegroundColor = SKColors.Cyan, BackgroundColor = SKColors.Pink, Padding = new Padding(5f)
                    })
                .AddSpacing()
                .AddHorizontalRule(style => style with { ForegroundColor = SKColors.Red })
            ).Build();
        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void Text_WithBorders_Test()
    {
        const string fileName = "Text_WithBorders_Test.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithVStack(vStack => vStack
                .AddText(
                    "Text_WithBorders_Text.pdf",
                    style => style with { FontPath = "Open Sans/SemiBold", FontSize = 24f, })
                .AddText(
                    """
                    Solid black border on top only.
                    """,
                    style => style with
                    {
                        Border = new BoxBorder
                        {
                            Top = new BorderStyle
                            {
                                Width = 1f,
                                Color = SKColors.Black,
                            },
                        },
                    })
                .AddText(
                    """
                    Solid black border on left only.
                    """,
                    style => style with
                    {
                        Border = new BoxBorder
                        {
                            Left = new BorderStyle
                            {
                                Width = 1f,
                                Color = SKColors.Black,
                            },
                        },
                    })
                .AddText(
                    """
                    Solid black border on right only.
                    """,
                    style => style with
                    {
                        Border = new BoxBorder
                        {
                            Right = new BorderStyle
                            {
                                Width = 1f,
                                Color = SKColors.Black,
                            },
                        },
                    })
                .AddText(
                    """
                    Solid black border on bottom only.
                    """,
                    style => style with
                    {
                        Border = new BoxBorder
                        {
                            Bottom = new BorderStyle
                            {
                                Width = 1f,
                                Color = SKColors.Black,
                            },
                        },
                    })
                .AddSpacing(10f)
                .AddText(
                    """
                    Solid black border on all sides.
                    """,
                    style => style with
                    {
                        Border = BoxBorder.Create(
                            new BorderStyle
                            {
                                Width = 1f,
                                Color = SKColors.Black,
                            }),
                    }))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }

    [Fact]
    public void Text_MultiPage_Test()
    {
        const string fileName = "text-multi-page-test.pdf";
        File.Delete(fileName);

        using var stream = File.OpenWrite(fileName);
        using var builder = PdfDocument.UsLetter;
        var pdfBytes = builder
            // .DisplayLayoutGuidelines()
            .WithDocumentStyle(style => style with { LineHeight = 1.5f, })
            .WithVStack(vStack => vStack
                .AddText(
                    "Test Document",
                    style => style with { FontPath = "Open Sans/Bold ", FontSize = 24f, })
                .AddText(
                    "Sample document for text block layout and rendering.",
                    style => style with { ForegroundColor = SKColors.DimGray, })
                .AddText(
                    """
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut euismod gravida ligula, ac interdum sapien viverra eget. Fusce pellentesque enim tristique interdum aliquet. Nulla quam ex, elementum at lorem ut, pellentesque luctus purus. Curabitur feugiat id tortor ut rutrum. Integer id velit suscipit, maximus nisi ac, sollicitudin odio. Maecenas imperdiet lacus velit, id aliquet sapien consectetur faucibus. Nunc lobortis gravida dui, cursus condimentum ex gravida id. Cras at erat quis mi tempor tempus. Nullam consequat velit non interdum vestibulum. Nulla quis magna ac augue molestie luctus sit amet at dolor. Integer aliquam quam quis lacinia scelerisque. Nunc ante velit, tempor quis luctus id, volutpat non enim. Suspendisse rhoncus imperdiet diam, at semper tellus congue at. In sit amet gravida est, nec viverra erat. Phasellus volutpat blandit ipsum, in condimentum nunc congue ut. Sed lacinia finibus elit eget molestie.
                    """)
                .AddText(
                    """
                    Phasellus sed venenatis elit, sed dictum augue. Suspendisse lorem lacus, aliquam eu nisl sed, tincidunt scelerisque est. Etiam a nunc quis enim vestibulum malesuada eu quis leo. In condimentum, nisl in scelerisque vehicula, mi quam posuere erat, sed fermentum ligula dolor molestie urna. Nunc vehicula nisl eu mauris mattis, eget dictum metus lacinia. Donec in tincidunt arcu. Suspendisse at posuere quam, a sollicitudin mi.
                    """)
                .AddText(
                    """
                    Praesent facilisis eros odio. In hac habitasse platea dictumst. Nulla facilisi. Aliquam orci enim, dictum eget elementum ut, posuere vel erat. Sed eget dictum diam. Nulla mollis, ante non tincidunt facilisis, massa nunc porta elit, ut accumsan mauris enim a enim. Sed id semper nulla. Vivamus id imperdiet velit. Donec volutpat sollicitudin magna, in sagittis nulla lacinia id. Cras ut urna iaculis, vulputate ipsum sit amet, porttitor leo. Nulla aliquam turpis sed urna laoreet, vel posuere dolor porta. Curabitur feugiat ultrices nisl et venenatis.
                    """)
                .AddText(
                    """
                    Nullam posuere sed ligula laoreet viverra. Suspendisse potenti. Sed vitae augue id libero tincidunt scelerisque. Curabitur vulputate metus sit amet lacus scelerisque, vel maximus sapien malesuada. Vestibulum sit amet turpis sem. Vivamus aliquam id metus ut condimentum. Donec eget faucibus lectus. Nam in sem sit amet lectus malesuada efficitur. Nunc pulvinar nec dolor non rhoncus. Aliquam odio sapien, pulvinar eu semper ut, imperdiet aliquam massa. Aliquam tincidunt non lorem et gravida. Vestibulum in orci libero. Phasellus finibus tempor pulvinar.
                    """)
                .AddText(
                    """
                    Mauris lacinia libero id libero sodales porttitor. Donec sagittis enim et felis scelerisque euismod. Praesent tortor quam, malesuada sed viverra nec, fringilla vel ipsum. Nunc sit amet luctus lectus. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cras interdum lacus et ligula tempor, volutpat gravida massa fermentum. Suspendisse semper purus ac sem accumsan, et molestie magna elementum. Vivamus bibendum eros vel rutrum luctus. Integer quis est vel tellus accumsan venenatis nec vitae tellus. Nam elementum magna sapien, et pretium tellus posuere in. Sed tincidunt quam quis fermentum faucibus. Aenean mattis ullamcorper diam, id feugiat sapien auctor id. Etiam non massa sapien.
                    """)
                .AddText(
                    """
                    Suspendisse in tempus eros. Nam malesuada nunc id libero finibus semper. Nunc libero neque, facilisis id tincidunt in, tincidunt a purus. Donec eu commodo dui, vestibulum euismod elit. Sed ut ligula id dui mollis rhoncus nec ut dolor. Etiam euismod non nunc vitae consectetur. Nunc libero mauris, tincidunt ac tempus id, aliquet vel enim. Duis at leo aliquet, laoreet ex ac, aliquam dui. Nullam lectus nulla, scelerisque non erat non, bibendum accumsan metus. Ut placerat ex risus, id mattis purus ullamcorper nec. Nullam augue nibh, finibus vel suscipit ac, ultricies ut enim. Interdum et malesuada fames ac ante ipsum primis in faucibus. Aenean eleifend posuere hendrerit.
                    """)
                .AddText(
                    """
                    Vivamus pharetra elit leo, vitae bibendum quam tristique vel. Integer vestibulum dolor quis finibus iaculis. Integer velit dolor, feugiat sit amet erat vulputate, sollicitudin elementum neque. Curabitur mattis elementum interdum. Ut sit amet sem ut tellus fermentum suscipit eu nec nisl. Cras ultrices dictum lacinia. Nullam imperdiet leo imperdiet dui egestas fringilla. Curabitur nec odio risus. Quisque aliquam dictum pellentesque. Nullam tempor placerat ligula, et tincidunt dolor eleifend id. Curabitur mattis, tortor vel lacinia consectetur, libero diam suscipit nisi, vel luctus lacus erat quis nulla. Nam dignissim dolor quis odio molestie, a lacinia orci malesuada. Quisque dui tortor, placerat et tincidunt at, placerat eu justo. Ut posuere aliquet nibh nec ultrices. Aliquam consequat imperdiet rutrum. Proin tristique metus sit amet bibendum ornare.
                    """)
                .AddText(
                    """
                    Aliquam erat volutpat. Donec vel metus ex. Nam ac pretium odio. Donec aliquet tellus felis, non varius nisi tempor ut. Cras dictum sit amet nisl ac sodales. Vestibulum laoreet felis at velit vulputate, commodo cursus diam sagittis. Donec ut est vehicula nulla venenatis consequat. Sed eu ante mi. Praesent imperdiet purus et egestas placerat. Mauris vestibulum egestas dictum. Donec iaculis, neque non pretium blandit, velit lorem malesuada turpis, eu consequat orci turpis a urna. Maecenas in vulputate urna, vitae mattis lacus. Curabitur arcu quam, bibendum at metus sed, finibus facilisis nibh. Vestibulum eu augue congue, dictum risus sit amet, sollicitudin arcu.
                    """)
                .AddHorizontalRule()
                .AddSpacing(50f)
                .AddHorizontalRule()
                .AddHStack(stack => stack
                    .WithDefaultStyle(style => style with { LineHeight = 1f, })
                    .AddText(
                        """
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut euismod gravida ligula, ac interdum sapien viverra eget. Fusce pellentesque enim tristique interdum aliquet. Nulla quam ex, elementum at lorem ut, pellentesque luctus purus. Curabitur feugiat id tortor ut rutrum. Integer id velit suscipit, maximus nisi ac, sollicitudin odio. Maecenas imperdiet lacus velit, id aliquet sapien consectetur faucibus. Nunc lobortis gravida dui, cursus condimentum ex gravida id. Cras at erat quis mi tempor tempus. Nullam consequat velit non interdum vestibulum. Nulla quis magna ac augue molestie luctus sit amet at dolor. Integer aliquam quam quis lacinia scelerisque. Nunc ante velit, tempor quis luctus id, volutpat non enim. Suspendisse rhoncus imperdiet diam, at semper tellus congue at. In sit amet gravida est, nec viverra erat. Phasellus volutpat blandit ipsum, in condimentum nunc congue ut. Sed lacinia finibus elit eget molestie.
                        """)
                    .AddText(
                        """
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut euismod gravida ligula, ac interdum sapien viverra eget. Fusce pellentesque enim tristique interdum aliquet. Nulla quam ex, elementum at lorem ut, pellentesque luctus purus. Curabitur feugiat id tortor ut rutrum. Integer id velit suscipit, maximus nisi ac, sollicitudin odio. Maecenas imperdiet lacus velit, id aliquet sapien consectetur faucibus. Nunc lobortis gravida dui, cursus condimentum ex gravida id. Cras at erat quis mi tempor tempus. Nullam consequat velit non interdum vestibulum. Nulla quis magna ac augue molestie luctus sit amet at dolor. Integer aliquam quam quis lacinia scelerisque. Nunc ante velit, tempor quis luctus id, volutpat non enim. Suspendisse rhoncus imperdiet diam, at semper tellus congue at. In sit amet gravida est, nec viverra erat. Phasellus volutpat blandit ipsum, in condimentum nunc congue ut. Sed lacinia finibus elit eget molestie.
                        """)))
            .Build();

        stream.Write(pdfBytes);
        testOutputHelper.WriteLine($"PDF created: file://{Path.GetFullPath(fileName)}");
        File.Exists(fileName).ShouldBeTrue();
    }
}
