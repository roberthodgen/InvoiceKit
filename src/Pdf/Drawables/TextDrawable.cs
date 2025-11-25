namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;
using Styles;

internal class TextDrawable(string text, SKRect rect, BlockStyle style) : IDrawable
{
    private float HalfLineHeight => (style.LineHeight * style.FontSize - style.FontSize) / 2;

    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawRect(rect, style.BackgroundToPaint());
        var location = rect.Top + HalfLineHeight - style.ToFont().Metrics.Ascent;
        context.Canvas.DrawText(text, rect.Left, location, SKTextAlign.Left, style.ToFont(), style.ForegroundToPaint());
    }

    public void Dispose()
    {
    }
}
