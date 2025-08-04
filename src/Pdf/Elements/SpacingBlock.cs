namespace InvoiceKit.Pdf.Elements;

using SkiaSharp;

/// <summary>
/// Used to add spacing in between blocks.
/// </summary>
public sealed class SpacingBlock : IDrawable
{
    public float Height { get; }

    internal SpacingBlock(float height)
    {
        Height = height;
    }
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, Height);
    }

    public void Draw(MultiPageContext context, SKRect rect)
    {
        if (context.Debug)
        {
            context.GetCurrentPage().Canvas.DrawRect(
                rect,
                new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Lime,
                    StrokeWidth = .5f,
                });
        }
    }

    public void Dispose()
    {
    }
}
