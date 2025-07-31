namespace InvoiceKit.Pdf;

using Layouts;
using SkiaSharp;

/// <summary>
/// Used to add spacing in between blocks.
/// </summary>
public class SpacingBlock : IDrawable
{
    public float Height { get; }

    public SpacingBlock(float height)
    {
        Height = height;
    }
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, Height);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        if (page.Debug)
        {
            page.Canvas.DrawRect(
                rect,
                new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Lime,
                    StrokeWidth = .5f,
                });
        }
    }
}
