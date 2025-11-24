namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;
using Styles;

internal class TextDrawable(string text, SKRect rect, BlockStyle style) : IDrawable
{
    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawText(text, rect.Left, rect.Top, SKTextAlign.Left, style.ToFont(), style.ForegroundToPaint());
    }

    public void Dispose()
    {
    }
}
