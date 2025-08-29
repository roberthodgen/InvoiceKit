namespace InvoiceKit.Pdf.Elements.Text;

using Containers.Stacks;
using SkiaSharp;
using Styles.Text;

/// <summary>
/// A text block represents a single paragraph's text. Line breaks may be added to prevent paragraph spacing. Automatic
/// new lines are added as needed.
/// </summary>
/// <remarks>
/// If multiple paragraphs are required, add them to <see cref="VStack"/>.
/// </remarks>
public sealed class TextLayout : ILayout
{
    private TextStyle Style { get; }

    private readonly List<string> _lines = [];

    internal TextLayout(TextStyle style, string text)
    {
        Style = style;
        using var reader = new StringReader(text);
        while (true)
        {
            var line = reader.ReadLine();
            if (line is null)
            {
                break;
            }

            _lines.Add(line);
        }
    }

    public SKSize Measure(SKSize available)
    {
        var lines = MeasureLines(available);
        return new SKSize(available.Width, lines.Sum(line => line.Height));
    }

    private List<SKSize> MeasureLines(SKSize available)
    {
        var lines = new List<SKSize>();
        var font = Style.ToFont();
        var renderedHeight = font.Metrics.Descent - font.Metrics.Ascent;
        var lineHeight = renderedHeight + ((Style.LineHeight * Style.FontSize) - Style.FontSize);
        var wrappedLines = _lines
            .SelectMany(line => WrapText(line, Style, available.Width))
            .Select((_, index) => index)
            .ToList();
        foreach (var index in wrappedLines)
        {
            var height = lineHeight;
            if (index == 0) // first line
            {
                height += Style.ParagraphSpacingBefore;
            }
            else if (index == wrappedLines.Count - 1) // last line
            {
                height += Style.ParagraphSpacingAfter;
            }

            lines.Add(new SKSize(available.Width, height));
        }

        return lines;
    }

    private static List<string> WrapText(string text, TextStyle style, float maxWidth)
    {
        var font = style.ToFont();
        var paint = style.ToPaint();
        var words = text.Split(' ');
        var lines = new List<string>();
        var currentLine = "";

        foreach (var word in words)
        {
            var testLine = string.IsNullOrEmpty(currentLine) ? word : $"{currentLine} {word}";
            var width = font.MeasureText(testLine, paint);

            if (width <= maxWidth)
            {
                currentLine = testLine;
            }
            else
            {
                if (!string.IsNullOrEmpty(currentLine))
                {
                    lines.Add(currentLine);
                }
                currentLine = word;
            }
        }

        if (!string.IsNullOrEmpty(currentLine))
        {
            lines.Add(currentLine);
        }

        return lines;
    }

    public void LayoutPages(MultiPageContext context)
    {
        var page = context.GetCurrentPage();
        var lineMeasurements = MeasureLines(page.Available.Size);

        for(int i = 0; i < _lines.Count; i++)
        {
            var left = lineMeasurements[i].Width;
            var top = lineMeasurements[i].Height;
            var rect = new SKRect(page.Available.Left, page.Available.Top, page.Available.Left + left, page.Available.Top + top);

            // This can probably be optimized.
            if (page.TryAllocateRect(rect))
            {
                page.Drawables.Add(new TextDrawable(_lines[i], rect, Style));
            }
            else
            {
                page.MarkFullyDrawn();
                page = context.GetCurrentPage();
                page.TryAllocateRect(rect);
                page.Drawables.Add(new TextDrawable(_lines[i], rect, Style));
            }
        }

        // Old Code for drawing lines below

        // foreach (var line in _lines)
        // {
        //     var wrappedLines = WrapText(line, Style, SizeAndLocation.Width);
        //     foreach (var wrapped in wrappedLines)
        //     {
        //         top += halfLineHeightAddition - font.Metrics.Ascent; // baseline adjustment
        //
        //         page.GetCurrentPage().Canvas.DrawText(wrapped, SizeAndLocation.Left, top, SKTextAlign.Left, font, paint);
        //         top += halfLineHeightAddition + font.Metrics.Descent; // for the next line: corSizeAndLocations baseline adjustment
        //     }
        // }
    }
}
