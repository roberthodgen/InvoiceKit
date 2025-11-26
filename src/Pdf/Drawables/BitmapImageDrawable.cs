namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;

internal class BitmapImageDrawable(SKBitmap bitmap, SKRect rect) : IDrawable
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
