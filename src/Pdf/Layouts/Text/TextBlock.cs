namespace InvoiceKit.Pdf.Layouts.Text;

using SkiaSharp;
using Styles.Text;

public class TextBlock : IDrawable
{
    public TextStyle Style { get; }

    private readonly List<TextLine> _lines = [];

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
        var width = available.Width; // always assume full height
        var height = _lines.Sum(line => line.Style.Height); // TODO multi-line support
        return new SKSize(width, height);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        var y = rect.Top;
        foreach (var line in _lines)
        {
            var paint = line.Style.ToPaint();
            var font = line.Style.ToFont();
            y += line.Style.Height;
            page.Canvas.DrawText(line.Text, rect.Left, y, SKTextAlign.Left, font, paint);
        }

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

internal class TextLine
{
    public required string Text { get; init; }

    public required TextStyle Style { get; init; }
}
