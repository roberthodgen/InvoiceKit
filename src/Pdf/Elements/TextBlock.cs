namespace InvoiceKit.Pdf.Elements;

using Layouts;
using Layouts.Stacks;
using SkiaSharp;
using Styles.Text;

/// <summary>
/// A text block represents a single paragraph's text. Line breaks may be added to prevent paragraph spacing. Automatic
/// new lines are added as needed.
/// </summary>
/// <remarks>
/// If multiple paragraphs are required, add them to <see cref="VStack"/>.
/// </remarks>
public sealed class TextBlock : IDrawable
{
    public TextStyle Style { get; private set; }

    private readonly List<string> _lines = new();

    internal TextBlock(TextStyle style, string text)
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
        var totalHeight = Style.ParagraphSpacingBefore;
        var font = Style.ToFont();
        var renderedHeight = font.Metrics.Descent - font.Metrics.Ascent;
        var lineHeight = renderedHeight + ((Style.LineHeight * Style.FontSize) - Style.FontSize);
        foreach (var line in _lines)
        {
            var wrapped = WrapText(line, Style, available.Width);
            totalHeight += lineHeight * wrapped.Count;
        }

        totalHeight += Style.ParagraphSpacingAfter;
        return new SKSize(available.Width, totalHeight);
    }

    public void Draw(PageLayout page, SKRect rect, Func<PageLayout> getNextPage)
    {
        if (page.Debug)
        {
            page.Canvas.DrawRect(
                rect,
                new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Cyan,
                    StrokeWidth = .5f,
                });
        }

        var top = rect.Top + Style.ParagraphSpacingBefore;
        var paint = Style.ToPaint();
        var font = Style.ToFont();
        var halfLineHeightAddition = ((Style.LineHeight * Style.FontSize) - Style.FontSize) / 2;
        foreach (var line in _lines)
        {
            var wrappedLines = WrapText(line, Style, rect.Width);
            foreach (var wrapped in wrappedLines)
            {
                top += halfLineHeightAddition - font.Metrics.Ascent; // baseline adjustment

                if (page.Debug)
                {
                    page.Canvas.DrawLine(
                        new SKPoint(rect.Left, top),
                        new SKPoint(rect.Right, top),
                        new SKPaint
                        {
                            Style = SKPaintStyle.Stroke,
                            Color = SKColors.LightGray,
                            StrokeWidth = .5f,
                        });
                }

                page.Canvas.DrawText(wrapped, rect.Left, top, SKTextAlign.Left, font, paint);
                top += halfLineHeightAddition + font.Metrics.Descent; // for the next line: corrects baseline adjustment
            }
        }
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

    public void Dispose()
    {
    }
}
