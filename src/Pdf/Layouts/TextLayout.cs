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

    private readonly string _text;

    private List<string> _wrappedLines = [];

    private int _currentIndex;

    private float HalfLineHeight => (Style.LineHeight * Style.FontSize - Style.FontSize) / 2;

    internal TextLayout(BlockStyle style, string text)
    {
        Style = style;
        _text = text;
    }

    /// <summary>
    /// Measures how much space a line of text takes.
    /// </summary>
    private SKSize MeasureFullLineSize(SKSize available)
    {
        var height = 0f;
        height += HalfLineHeight - Style.ToFont().Metrics.Ascent;
        height += HalfLineHeight + Style.ToFont().Metrics.Descent;

        return new SKSize(available.Width, height);
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
        var childContext = GetContext(context);

        if (_wrappedLines.Count == 0)
        {
            _wrappedLines = WrapText(_text, Style, childContext.Available.Size.Width).ToList();
        }

        if (_currentIndex >= _wrappedLines.Count)
        {
            return LayoutResult.FullyDrawn([]);
        }

        // Allocate the style size
        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            return LayoutResult.NeedsNewPage([]);
        }

        var drawables = new List<IDrawable>();

        while (_currentIndex < _wrappedLines.Count)
        {
            if (context.TryAllocate(MeasureFullLineSize(context.Available.Size), out var rect))
            {
                drawables.Add(new DebugDrawable(rect, DebugDrawable.ContentColor));
                drawables.Add(new TextDrawable(_wrappedLines[_currentIndex], rect, Style));
                _currentIndex++;
                continue; // Skip to the next line.
            }

            drawables.InsertRange(0, Style.GetStyleDrawables(childContext.Allocated));

            childContext.CommitChildContext();
            return LayoutResult.NeedsNewPage(drawables);
        }

        // Reset index for repeating layouts.
        _currentIndex = 0;

        drawables.InsertRange(0, Style.GetStyleDrawables(childContext.Allocated));

        childContext.CommitChildContext();
        return LayoutResult.FullyDrawn(drawables);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext();
    }

    public ILayoutContext GetContext(ILayoutContext parentContext, SKRect intersectingRect)
    {
        return parentContext.GetVerticalChildContext(intersectingRect);
    }
}
