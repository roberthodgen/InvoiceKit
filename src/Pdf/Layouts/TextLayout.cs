namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;
using Styles.Text;

/// <summary>
/// A text block represents a single paragraph's text. Line breaks may be added to prevent paragraph spacing. Automatic
/// new lines are added as needed.
/// </summary>
internal class TextLayout : ILayout
{
    private bool _drawn;

    private TextStyle Style { get; }

    private readonly List<string> _lines = [];

    private List<string> _wrappedLines = [];

    private int _currentIndex = 0;

    private float HalfLineHeight => (Style.LineHeight * Style.FontSize - Style.FontSize) / 2;

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

    public SKSize Measure(SKRect available)
    {
        var wrappedLines = _lines.SelectMany(line => WrapText(line, Style, available.Width)).ToList();
        var height = available.Top;

        for (int index = 0; index < wrappedLines.Count; index++)
        {
            height += MeasureFullLineRect(available, height, index).Height;
        }

        return new SKSize(available.Width, height);
    }

    private SKRect MeasureFullLineRect(SKRect available, float top, int index)
    {
        var bottom = top;

        if (index == 0)
        {
            bottom += Style.ParagraphSpacingBefore;
        }

        bottom += HalfLineHeight - Style.ToFont().Metrics.Ascent;
        bottom += HalfLineHeight + Style.ToFont().Metrics.Descent;

        if (index == _wrappedLines.Count - 1)
        {
            bottom += Style.ParagraphSpacingAfter;
        }
        return new SKRect(available.Left, top, available.Right, bottom);
    }

    private SKRect MeasureTextLineRect(SKRect available, float top, int index)
    {
        var textLineLocation = top + HalfLineHeight - Style.ToFont().Metrics.Ascent;

        if (index == 0)
        {
            textLineLocation += Style.ParagraphSpacingBefore;
        }

        return new SKRect(
            available.Left,
            textLineLocation,
            available.Right,
            textLineLocation);
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

    public LayoutResult Layout(LayoutContext context, LayoutType layoutType)
    {
        if (_drawn || _lines.Count == 0)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        if (_wrappedLines.Count == 0)
        {
            _wrappedLines = _lines.SelectMany(line => WrapText(line, Style, context.Available.Width)).ToList();
        }

        var listDrawables = new List<IDrawable>();
        var top = context.Available.Top;

        while (_currentIndex < _wrappedLines.Count)
        {
            var textRect = MeasureTextLineRect(context.Available, top, _currentIndex);

            var allocationRect = MeasureFullLineRect(context.Available, top, _currentIndex);

            // Tries to allocate the rect to the current page. If it fails, the page is marked full and a new page is created.
            if (context.TryAllocateRect(allocationRect))
            {
                listDrawables.Add(new TextDrawable(_wrappedLines[_currentIndex], textRect, Style));
                _currentIndex++;
                top = allocationRect.Bottom;
            }
            else
            {
                // Will only be hit if the page is full.
                return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
            }
        }

        if (layoutType == LayoutType.RepeatingElement)
        {
            _currentIndex = 0;
        }
        else
        {
            _drawn = true;
        }
        return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
    }
}
