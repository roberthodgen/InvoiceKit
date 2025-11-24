namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;
using Styles;

internal class BorderDrawable(SKRect rect, BoxBorder border) : IDrawable
{
    /// <summary>
    /// Draws a border within the specified rectangle.
    /// </summary>
    /// <remarks>
    /// The border will be drawn inside the rectangle so as not to expand it.
    /// </remarks>
    public void Draw(IDrawableContext context)
    {
        if (border.Top.IsDrawable())
        {
            var (a, b) = border.GetTopPoints(rect);
            context.Canvas.DrawLine(a, b, border.Top.ToPaint());
        }

        if (border.Bottom.IsDrawable())
        {
            var (a, b) = border.GetBottomPoints(rect);
            context.Canvas.DrawLine(a, b, border.Bottom.ToPaint());
        }

        if (border.Left.IsDrawable())
        {
            var (a, b) = border.GetLeftPoints(rect);
            context.Canvas.DrawLine(a, b, border.Left.ToPaint());
        }

        if (border.Right.IsDrawable())
        {
            var (a, b) = border.GetRightPoints(rect);
            context.Canvas.DrawLine(a, b, border.Right.ToPaint());
        }
    }

    public void Dispose()
    {
        // nothing to dispose of
    }
}
