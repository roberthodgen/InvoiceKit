namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;
using Styles;

internal class TextDrawable(string text, SKRect rect, BlockStyle style) : IDrawable
{
    public void Draw(IDrawableContext context)
    {
        if (context.Debug)
        {
            context.Canvas.DrawLine(
                rect.Left,
                rect.Top,
                rect.Right,
                rect.Bottom,
                new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.LightGray,
                    StrokeWidth = 1f,
                });
        }

        context.Canvas.DrawText(text, rect.Left, rect.Top, SKTextAlign.Left, style.ToFont(), style.ForegroundToPaint());
    }

    public void Dispose()
    {
    }
}
