namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;
using Styles;

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
