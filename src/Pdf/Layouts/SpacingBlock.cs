namespace InvoiceKit.Pdf.Elements;

using Layouts;
using SkiaSharp;

/// <summary>
/// Used to add spacing in between blocks.
/// </summary>
public sealed class SpacingBlock : IDrawable
{
    private float Height { get; }

    internal SpacingBlock(float height)
    {
        Height = height;
    }
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, Height);
    }

    public void Draw(PageLayout page)
    {
        page.Canvas.DrawRect(
            page.Available,
            new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Lime,
                StrokeWidth = .5f,
            });
    }

    public void Dispose()
    {
    }
}
