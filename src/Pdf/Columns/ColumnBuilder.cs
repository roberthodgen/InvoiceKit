namespace InvoiceKit.Pdf.Columns;

using SkiaSharp;

public class ColumnBuilder
{
    private readonly List<(string text, TextStyle style)> _lines = [];

    public ColumnBuilder AddTextLine(string text)
    {
        return AddTextLine(text, _ => { });
    }

    public ColumnBuilder AddTextLine(string text, Action<TextOptionsBuilder> options)
    {
        var builder = new TextOptionsBuilder();
        options(builder);
        var style = builder.Build();
        _lines.Add((text, style));
        return this;
    }

    public void Render(SKCanvas canvas, SKRect rect)
    {
        var y = rect.Top;
        foreach (var (text, style) in _lines)
        {
            var paint = style.ToPaint();
            var font = style.ToFont();
            y += font.Size;
            canvas.DrawText(text, rect.Left, y, SKTextAlign.Left, font, paint);
            y += 4;
        }
    }
}
