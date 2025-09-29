namespace InvoiceKit.Pdf.Containers.SpacingBlock;

using SkiaSharp;

internal class SpacingBlockLayout : ILayout
{
    public bool IsFullyDrawn { get; set; }

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
        if (IsFullyDrawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var rect = new SKRect(context.Available.Left, context.Available.Top, context.Available.Right,
            context.Available.Top + Height);

        // If the spacing block cannot fit on the current page, the spacing block is disregarded.
        if (context.TryAllocateRect(rect))
        {
            // Allocates the space but does not draw anything.
        }
        
        // Always returns fully drawn
        IsFullyDrawn = true;
        return new LayoutResult([], LayoutStatus.IsFullyDrawn);
    }
}
