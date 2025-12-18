namespace InvoiceKit.Pdf.Drawables;

using Geometry;
using SkiaSharp;

internal class TextDrawable(string text, OuterRect rect, BlockStyle style) : IDrawable
{
    private float HalfLineHeight => (style.LineHeight * style.FontSize - style.FontSize) / 2;

    public void Draw(IDrawableContext context)
    {
        var border = style.Margin.ToBorderRect(rect);
        var padding = style.Border.GetContentRect(border);
        var content = style.Padding.GetContentRect(padding);
        var font = style.ToFont();
        context.Canvas.DrawText(
            text,
            content.ToRect().Left,
            content.ToRect().Top + HalfLineHeight - font.Metrics.Ascent,
            SKTextAlign.Left,
            font,
            style.ForegroundToPaint());
    }

    public void Dispose()
    {
        // no resources
    }
}
