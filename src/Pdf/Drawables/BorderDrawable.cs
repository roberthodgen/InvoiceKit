namespace InvoiceKit.Pdf.Drawables;

using Geometry;
using SkiaSharp;

internal class BorderDrawable(OuterRect rect, BlockStyle style) : IDrawable
{
    private readonly BorderRect _borderRect = style.Margin.ToBorderRect(rect);

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
            var (a, b) = GetTopPoints();
            context.Canvas.DrawLine(a, b, style.Border.Top.ToPaint());
        }

        if (style.Border.Bottom.IsDrawable())
        {
            var (a, b) = GetBottomPoints();
            context.Canvas.DrawLine(a, b, style.Border.Bottom.ToPaint());
        }

        if (style.Border.Left.IsDrawable())
        {
            var (a, b) = GetLeftPoints();
            context.Canvas.DrawLine(a, b, style.Border.Left.ToPaint());
        }

        if (style.Border.Right.IsDrawable())
        {
            var (a, b) = GetRightPoints();
            context.Canvas.DrawLine(a, b, style.Border.Right.ToPaint());
        }
    }

    public void Dispose()
    {
        // nothing to dispose of
    }

    private (SKPoint, SKPoint) GetTopPoints()
    {
        return (
            new SKPoint(_borderRect.Left + (style.Border.Top.Width / 2), _borderRect.Top),
            new SKPoint(_borderRect.Right - (style.Border.Top.Width / 2), _borderRect.Top));
    }

    private (SKPoint, SKPoint) GetBottomPoints()
    {
        return (
            new SKPoint(_borderRect.Left + (style.Border.Bottom.Width / 2), _borderRect.Bottom),
            new SKPoint(_borderRect.Right - (style.Border.Bottom.Width / 2), _borderRect.Bottom));
    }

    private (SKPoint, SKPoint) GetLeftPoints()
    {
        return (
            new SKPoint(_borderRect.Left + style.Border.Left.Width, _borderRect.Top + (style.Border.Left.Width / 2)),
            new SKPoint(_borderRect.Left + style.Border.Left.Width, _borderRect.Bottom - (style.Border.Left.Width / 2)));
    }

    private (SKPoint, SKPoint) GetRightPoints()
    {
        return (
            new SKPoint(_borderRect.Right - style.Border.Right.Width, _borderRect.Top + (style.Border.Right.Width / 2)),
            new SKPoint(_borderRect.Right - style.Border.Right.Width, _borderRect.Bottom - (style.Border.Right.Width / 2)));
    }
}
