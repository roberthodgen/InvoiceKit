namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;
using Styles;

internal class BitmapImageDrawable(SKBitmap bitmap, SKRect rect, BlockStyle style) : IDrawable
{
    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawBitmap(bitmap, rect);
    }

    public void Dispose()
    {
        bitmap.Dispose();
    }
}
