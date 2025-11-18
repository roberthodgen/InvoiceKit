namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;

internal class BorderDrawable(SKRect rect, BlockStyle style) : IDrawable
{
    /// <summary>
    /// Draws a border within the specified rectangle.
    /// </summary>
    /// <remarks>
    /// The border will be drawn inside the rectangle so as not to expand it.
    /// </remarks>
    public void Draw(IDrawableContext context)
    {
        if (style.Border.Top.IsDrawable())
        {
            var (a, b) = style.Border.GetTopPoints(rect);
            context.Canvas.DrawLine(a, b, style.Border.Top.ToPaint());
        }

        if (style.Border.Bottom.IsDrawable())
        {
            var (a, b) = style.Border.GetBottomPoints(rect);
            context.Canvas.DrawLine(a, b, style.Border.Bottom.ToPaint());
        }

        if (style.Border.Left.IsDrawable())
        {
            var (a, b) = style.Border.GetLeftPoints(rect);
            context.Canvas.DrawLine(a, b, style.Border.Left.ToPaint());
        }

        if (style.Border.Right.IsDrawable())
        {
            var (a, b) = style.Border.GetRightPoints(rect);
            context.Canvas.DrawLine(a, b, style.Border.Right.ToPaint());
        }
    }

    public void Dispose()
    {
        // nothing to dispose of
    }
}
