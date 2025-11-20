namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;

public class BackgroundDrawable(SKRect rect, BlockStyle style) : IDrawable
{
    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawRect(rect, style.BackgroundToPaint());
    }

    public void Dispose()
    {
    }
}
