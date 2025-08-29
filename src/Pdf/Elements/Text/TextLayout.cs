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
        var wrappedLines = _lines.SelectMany(line => WrapText(line, Style, page.Available.Width)).ToList();
        var halfLineHeightAddition = ((Style.LineHeight * Style.ToFont().Size) - Style.ToFont().Size) / 2;
        var lineMeasurements = MeasureLines(page.Available.Size);
        var top = page.Available.Top + Style.ParagraphSpacingBefore;

        for(var i = 0; i < wrappedLines.Count; i++)
        {
            var lineHeight = lineMeasurements[i].Height;
            while (true)
            {
                top += halfLineHeightAddition - Style.ToFont().Metrics.Ascent;
                var rectBottom = top + lineHeight + (halfLineHeightAddition + Style.ToFont().Metrics.Descent);

                if (i == wrappedLines.Count - 1)
                {
                    rectBottom += Style.ParagraphSpacingAfter;
                }

                var rect = new SKRect(
                    page.Available.Left,
                    top,
                    page.Available.Right,
                    rectBottom);

                if (page.TryAllocateRect(rect))
                {
                    page.AddDrawable(new TextDrawable(wrappedLines[i], rect, Style));
                    break;
                }

                // Will only be hit if the page is full.
                page.MarkFullyDrawn();
                page = context.GetCurrentPage();
                top = page.Available.Top;
            }
        }
    }
}
