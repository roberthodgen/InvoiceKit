namespace InvoiceKit.Pdf.Layouts.Text;

using SkiaSharp;
using Styles.Text;

/// <summary>
/// A text block represents a single paragraph's text. Line breaks may be added to prevent paragraph spacing. Automatic
/// new lines are added as needed.
/// </summary>
/// <remarks>
/// If multiple paragraphs are required, add them to <see cref="Stacks.VStack"/>.
/// </remarks>
public class TextBlock : IDrawable
{
    public TextStyle Style { get; private set; }

    private readonly List<TextLine> _lines = new();

    public TextBlock(TextStyle style)
    {
        Style = style;
    }

    public TextBlock ParagraphSpacing(float before = 1.25f, float after = 1.25f)
    {
        Style = Style with { ParagraphSpacing = new() { Before = before, After = after } };
        return this;
    }

    public TextBlock LineHeight(float height = 1.1f)
    {
        Style = Style with { LineHeight = height };
        return this;
    }

    public TextBlock AddLine(string text)
    {
        return AddLine(text, _ => { });
    }

    public TextBlock AddLine(string text, Action<TextOptionsBuilder> options)
    {
        var builder = new TextOptionsBuilder(Style);
        options(builder);
        _lines.Add(new TextLine { Text = text, Style = builder.Build() });
        return this;
    }

    public SKSize Measure(SKSize available)
    {
        var totalHeight = _lines.First().Style.ParagraphSpacingBefore;
        foreach (var line in _lines)
        {
            var wrapped = WrapText(line.Text, line.Style, available.Width);
            var font = line.Style.ToFont();
            var renderedHeight = font.Metrics.Descent - font.Metrics.Ascent;
            var lineHeight = renderedHeight + ((line.Style.LineHeight * line.Style.FontSize) - line.Style.FontSize);
            totalHeight += lineHeight * wrapped.Count;
        }

        totalHeight += _lines.Last().Style.ParagraphSpacingAfter;
        return new SKSize(available.Width, totalHeight);
    }

    public void Draw(PageLayout page, SKRect rect)
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

        var top = rect.Top + _lines.First().Style.ParagraphSpacingBefore;
        foreach (var line in _lines)
        {
            var paint = line.Style.ToPaint();
            var font = line.Style.ToFont();
            var wrappedLines = WrapText(line.Text, line.Style, rect.Width);
            foreach (var wrapped in wrappedLines)
            {
                var halfLineHeightAddition = ((line.Style.LineHeight * line.Style.FontSize) - line.Style.FontSize) / 2;
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
}

internal class TextLine
{
    public required string Text { get; init; }

    public required TextStyle Style { get; init; }
}
