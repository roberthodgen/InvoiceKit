namespace InvoiceKit.Pdf.Containers.SpacingBlock;

using SkiaSharp;

public class SpacingBlockLayout : ILayout
{
    private float Height { get; }

    internal SpacingBlockLayout(float height)
    {
        Height = height;
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, Height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        while (true)
        {
            var rect = new SKRect(context.Available.Left, context.Available.Top, context.Available.Right,
                context.Available.Top + Height);
            if (context.TryAllocateRect(rect))
            {
                // Allocates the space but does not draw anything.
                break;
            }

            // Will only be hit if the page is full.
            return new LayoutResult([], LayoutStatus.NeedsNewPage);
        }

        return new LayoutResult([], LayoutStatus.IsFullyDrawn);
    }
}
