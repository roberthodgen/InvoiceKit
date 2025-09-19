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

    /// <summary>
    ///
    /// </summary>
    private List<SKSize> MeasureLines(SKSize available)
    {
        // Todo: might not need this measure lines method. However, could separates measuring logic out for both the text and rect.
        var lines = new List<SKSize>();
        var font = Style.ToFont();
        var renderedHeight = font.Metrics.Descent - font.Metrics.Ascent;
        var lineHeight = renderedHeight + ((Style.LineHeight * Style.FontSize) - Style.FontSize);
        var wrappedLines = _lines
            .SelectMany(line => WrapText(line, Style, available.Width))
            .Select((_, index) => index)
            .ToList();
        wrappedLines.ForEach(line => lines.Add(new SKSize(available.Width, lineHeight)));;
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

    /// <summary>
    /// Separates a single string into multiple lines based on the width of the available space.
    /// </summary>
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

    /// <summary>
    ///
    /// </summary>
    public void LayoutPages(MultiPageContext context, bool debug)
    {
        var page = context.GetCurrentPage();
        var wrappedLines = _lines.SelectMany(line => WrapText(line, Style, page.Available.Width)).ToList();
        var halfLineHeight = (Style.LineHeight * Style.FontSize - Style.FontSize) / 2;
        var top = page.Available.Top;

        for(var i = 0; i < wrappedLines.Count; i++)
        {
            while (true)
            {
                var textLineLocation = top + halfLineHeight - Style.ToFont().Metrics.Ascent;

                if (i == 0)
                {
                    textLineLocation += Style.ParagraphSpacingBefore;
                }

                var textRect = new SKRect(page.Available.Left, textLineLocation, page.Available.Right, textLineLocation);
                var allocationRectBottom = textLineLocation + halfLineHeight + Style.ToFont().Metrics.Descent;

                if (i == wrappedLines.Count - 1)
                {
                    allocationRectBottom += Style.ParagraphSpacingAfter;
                }

                var rect = new SKRect(page.Available.Left, top, page.Available.Right, allocationRectBottom);

                // Tries to allocate the rect to the current page. If it fails, the page is marked full and a new page is created.
                if (page.TryAllocateRect(rect))
                {
                    page.AddDrawable(new TextDrawable(wrappedLines[i], textRect, Style, debug));
                    top = allocationRectBottom;
                    break;
                }

                // Will only be hit if the page is full.
                page.MarkFullyDrawn(page);
                page = context.GetCurrentPage();
                top = page.Available.Top;
            }
        }
    }
}
