namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using Geometry;

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

    /// <summary>
    /// Measures how much space a line of text takes.
    /// </summary>
    private OuterSize MeasureFullLineSize(OuterSize available)
    {
        var height = 0f;
        var font = Style.ToFont();
        height += HalfLineHeight - font.Metrics.Ascent;
        height += HalfLineHeight + font.Metrics.Descent;

        if (_currentIndex == 0)
        {
            // first
            height += Style.Padding.Top;
            height += Style.Border.Top.Width;
            height += Style.Margin.Top;
        }

        if (_currentIndex == _wrappedLines.Count - 1)
        {
            // last
            height += Style.Padding.Bottom;
            height += Style.Border.Bottom.Width;
            height += Style.Margin.Bottom;
        }

        return new OuterSize(available.Width, height);
    }

    /// <summary>
    /// Separates a string into multiple lines based on the width of the available space.
    /// </summary>
    private static List<string> WrapText(string text, BlockStyle style, float maxWidth)
    {
        var font = style.ToFont();
        var paint = style.ForegroundToPaint();
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

    public LayoutResult Layout(ILayoutContext context)
    {
        if (_wrappedLines.Count == 0)
        {
            _wrappedLines = _lines.SelectMany(line => WrapText(line, Style, context.Available.Width)).ToList();
        }

        if (_currentIndex >= _wrappedLines.Count)
        {
            return LayoutResult.FullyDrawn([]);
        }

        var drawables = new List<IDrawable>();

        while (_currentIndex < _wrappedLines.Count)
        {
            if (context.TryAllocate(MeasureFullLineSize(context.Available.ToSize()), out var rect))
            {
                drawables.Add(new TextDrawable(_wrappedLines[_currentIndex], rect, Style));
                _currentIndex++;
                continue; // Skip to the next line.
            }

            drawables.Insert(0, new DebugDrawable(context.Allocated, Style));
            drawables.Insert(0, new BackgroundDrawable(context.Allocated, Style.BackgroundToPaint()));
            // drawables.Add(new BorderDrawable(new OuterRect(context.Allocated), Style.Border));

            return LayoutResult.NeedsNewPage(drawables);
        }

        // Reset index for repeating layouts.
        _currentIndex = 0;

        drawables.Insert(0, new DebugDrawable(context.Allocated, Style));
        drawables.Insert(0, new BackgroundDrawable(context.Allocated, Style.BackgroundToPaint()));
        // drawables.Add(new BorderDrawable(context.Allocated, Style.Border));

        return LayoutResult.FullyDrawn(drawables);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext();
    }

    public ILayoutContext GetContext(ILayoutContext parentContext, OuterRect intersectingRect)
    {
        return parentContext.GetVerticalChildContext(intersectingRect);
    }
}
