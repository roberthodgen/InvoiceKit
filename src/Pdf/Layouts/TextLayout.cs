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
        var stylingSize = Style.GetStyleSize();
        var sizeAfterStyle = Style.GetSizeAfterStyle(available);
        if (_wrappedLines.Count == 0)
        {
            _wrappedLines = _lines.SelectMany(line => WrapText(line, Style, sizeAfterStyle.Width)).ToList();
        }

        var height = _wrappedLines.Select(_ => MeasureFullLineSize(sizeAfterStyle).Height).Sum() + stylingSize.Height;
        return new SKSize(available.Width, height);
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
    /// Separates a single string into multiple lines based on the width of the available space.
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

    public LayoutResult Layout(LayoutContext context)
    {
        var childContext = context.GetChildContext(Style.GetContentRect(context.Available));
        if (_wrappedLines.Count == 0)
        {
            _wrappedLines = _lines.SelectMany(line => WrapText(line, Style, childContext.Available.Width)).ToList();
        }

        var drawables = new List<IDrawable>();

        // Allocate the style size
        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
        }

        while (_currentIndex < _wrappedLines.Count)
        {
            if (childContext.TryAllocate(MeasureFullLineSize(childContext.Available.Size), out var rect))
            {
                drawables.Add(new DebugDrawable(rect, DebugDrawable.ContentColor));
                drawables.Add(new TextDrawable(_wrappedLines[_currentIndex], rect, Style));
                _currentIndex++;
                continue;   // Skip to the next line.
            }

            // Add background and border drawables.
            // Background needs to come before text.
            drawables.Insert(0, new BackgroundDrawable(Style.GetBackgroundRect(childContext.Allocated), Style.BackgroundToPaint()));
            drawables.Add(new BorderDrawable(Style.GetBorderRect(childContext.Allocated), Style.Border));

            // Add margin and padding debug drawables.
            drawables.Add(new DebugDrawable(Style.GetMarginDebugRect(childContext.Allocated), DebugDrawable.MarginColor));
            drawables.Add(new DebugDrawable(Style.GetBackgroundRect(childContext.Allocated), DebugDrawable.PaddingColor));

            context.CommitChildContext(childContext);
            return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
        }

        // Reset index for repeating layouts.
        _currentIndex = 0;

        // Add and border drawables.
        // Background needs to come before text.
        drawables.Insert(0, new BackgroundDrawable(Style.GetBackgroundRect(childContext.Allocated), Style.BackgroundToPaint()));
        drawables.Add(new BorderDrawable(Style.GetBorderRect(childContext.Allocated), Style.Border));

        // Add margin and padding debug drawables.
        drawables.Add(new DebugDrawable(Style.GetMarginDebugRect(childContext.Allocated), DebugDrawable.MarginColor));
        drawables.Add(new DebugDrawable(Style.GetBackgroundRect(childContext.Allocated), DebugDrawable.PaddingColor));

        context.CommitChildContext(childContext);
        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }
}
