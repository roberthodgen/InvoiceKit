namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;

public class BackgroundDrawable(SKRect rect, SKPaint paint) : IDrawable
{
    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawRect(rect, paint);
    }

    public void Dispose()
    {
    }
}
