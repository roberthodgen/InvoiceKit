namespace InvoiceKit.Pdf.Layouts.Text;

using SkiaSharp;
using Styles.Text;

public class TextBlock : IDrawable
{
    public TextStyle Style { get; }

    private readonly List<TextLine> _lines = new();

    public TextBlock(TextStyle style)
    {
        Style = style;
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
        float totalHeight = Style.Height + Style.ParagraphSpacing.Total;

        foreach (var line in _lines)
        {
            var wrapped = WrapText(line.Text, line.Style, available.Width);
            totalHeight += (wrapped.Count * line.Style.Height);
        }

        return new SKSize(available.Width, totalHeight);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        var top = rect.Top;
        foreach (var line in _lines)
        {
            var paint = line.Style.ToPaint();
            var font = line.Style.ToFont();
            var wrappedLines = WrapText(line.Text, line.Style, rect.Width);
            top += line.Style.ParagraphSpacing.Before;

            foreach (var wrapped in wrappedLines)
            {
                top += line.Style.Height;
                page.Canvas.DrawText(wrapped, rect.Left, top, SKTextAlign.Left, font, paint);
            }

            top += line.Style.ParagraphSpacing.After;
        }

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
