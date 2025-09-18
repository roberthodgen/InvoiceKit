namespace InvoiceKit.Pdf.Containers;

using SkiaSharp;

/// <summary>
/// Used to add spacing in between blocks.
/// </summary>
public sealed class SpacingBlock : IViewBuilder
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

    public void Draw(SKCanvas canvas, PageLayout page)
    {
        canvas.DrawRect(
            page.Available,
            new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Lime,
                StrokeWidth = .5f,
            });
    }

    public ILayout ToLayout()
    {
        throw new NotImplementedException();
    }
}
