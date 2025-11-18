namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

/// <summary>
/// A text block represents a single paragraph's text. Line breaks may be added to prevent paragraph spacing. Automatic
/// new lines are added as needed.
/// </summary>
internal class TextLayout : ILayout
{
    private BlockStyle Style { get; }

    private readonly List<string> _lines = [];

    private List<string> _wrappedLines = [];

    private int _currentIndex;

    private float HalfLineHeight => (Style.LineHeight * Style.FontSize - Style.FontSize) / 2;

    internal TextLayout(BlockStyle style, string text)
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
        if (_wrappedLines.Count == 0)
        {
            _wrappedLines = _lines.SelectMany(line => WrapText(line, Style, available.Width)).ToList();
        }

        var height = _wrappedLines.Select((_, index) => MeasureFullLineSize(available, index).Height).Sum();
        return new SKSize(available.Width, height);
    }

    /// <summary>
    /// Measures how much space a line of text takes.
    /// </summary>
    private SKSize MeasureFullLineSize(SKSize available, int index)
    {
        var height = 0f;

        if (index == 0)
        {
            height += Style.ParagraphSpacingBefore;
        }

        height += HalfLineHeight - Style.ToFont().Metrics.Ascent;
        height += HalfLineHeight + Style.ToFont().Metrics.Descent;

        if (index == _wrappedLines.Count - 1)
        {
            height += Style.ParagraphSpacingAfter;
        }

        return new SKSize(available.Width, height);
    }

    /// <summary>
    /// Creates a rect for the location of a text drawable.
    /// </summary>
    private SKRect MeasureTextLineRect(SKRect available, int index)
    {
        var textLineLocation = available.Top + HalfLineHeight - Style.ToFont().Metrics.Ascent;

        if (index == 0)
        {
            textLineLocation += Style.ParagraphSpacingBefore;
        }

        return new SKRect(available.Left, textLineLocation, available.Right, textLineLocation);
    }

    /// <summary>
    /// Separates a single string into multiple lines based on the width of the available space.
    /// </summary>
    private static List<string> WrapText(string text, BlockStyle style, float maxWidth)
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

    public LayoutResult Layout(LayoutContext context)
    {
        if (_wrappedLines.Count == 0)
        {
            _wrappedLines = _lines.SelectMany(line => WrapText(line, Style, context.Available.Width)).ToList();
        }

        var drawables = new List<IDrawable>();
        while (_currentIndex < _wrappedLines.Count)
        {
            // Tries to allocate the size within the current page or calls for a new page.
            if (context.TryAllocate(MeasureFullLineSize(context.Available.Size, _currentIndex), out var rect))
            {
                var textRect = MeasureTextLineRect(rect, _currentIndex);
                drawables.Add(new TextDrawable(_wrappedLines[_currentIndex], textRect, Style));
                _currentIndex++;
            }
            else
            {
                drawables.Add(new BorderDrawable(context.Allocated, Style));
                return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
            }
        }

        _currentIndex = 0;
        drawables.Add(new BorderDrawable(context.Allocated, Style));
        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }
}
