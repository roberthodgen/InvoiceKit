namespace InvoiceKit.Pdf.Drawables;

using Geometry;
using SkiaSharp;

internal class TextDrawable(string text, ContentRect rect, BlockStyle style) : IDrawable
{
    private float HalfLineHeight => (style.LineHeight * style.FontSize - style.FontSize) / 2;

    public void Draw(IDrawableContext context)
    {
        var font = style.ToFont();
        context.Canvas.DrawText(
            text,
            rect.ToRect().Left,
            rect.ToRect().Top + HalfLineHeight - font.Metrics.Ascent,
            SKTextAlign.Left,
            font,
            style.ForegroundToPaint());
    }

    public void Dispose()
    {
        // no resources
    }
}
