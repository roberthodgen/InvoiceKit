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

    private List<string> _wrappedLines = [];

    private int _currentIndex = 0;

    public bool IsFullyDrawn { get; set; }

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
        //Todo: measure a single line for use inside of the layout methods.
        return new SKSize();
    }

    /// <summary>
    /// Separates a single string into multiple lines based on the width of the available space.
    /// Todo: This needs to be merged into the Layout method.
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
    public LayoutResult Layout(LayoutContext context)
    {
        if (IsFullyDrawn || _lines.Count == 0) return new LayoutResult([], LayoutStatus.IsFullyDrawn);

        if (_wrappedLines.Count == 0)
        {
            _wrappedLines = _lines.SelectMany(line => WrapText(line, Style, context.Available.Width)).ToList();
        }

        var listDrawables = new List<IDrawable>();
        var halfLineHeight = (Style.LineHeight * Style.FontSize - Style.FontSize) / 2;
        var top = context.Available.Top;

        while (_currentIndex < _wrappedLines.Count)
        {
            var textLineLocation = top + halfLineHeight - Style.ToFont().Metrics.Ascent;

            if (_currentIndex == 0)
            {
                textLineLocation += Style.ParagraphSpacingBefore;
            }

            var textRect = new SKRect(context.Available.Left, textLineLocation, context.Available.Right,
                textLineLocation);
            var allocationRectBottom = textLineLocation + halfLineHeight + Style.ToFont().Metrics.Descent;

            if (_currentIndex == _wrappedLines.Count - 1)
            {
                allocationRectBottom += Style.ParagraphSpacingAfter;
            }

            var rect = new SKRect(context.Available.Left, top, context.Available.Right, allocationRectBottom);

            // Tries to allocate the rect to the current page. If it fails, the page is marked full and a new page is created.
            if (context.TryAllocateRect(rect))
            {
                listDrawables.Add(new TextDrawable(_wrappedLines[_currentIndex], textRect, Style));
                _currentIndex++;
                top = allocationRectBottom;
            }
            else
            {
                // Will only be hit if the page is full.
                return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
            }
        }
        IsFullyDrawn = true;
        return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
    }
}
