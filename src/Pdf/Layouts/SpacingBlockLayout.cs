namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

// Todo: should probably be removed with the addition of margin and padding.
internal class SpacingBlockLayout(float height) : ILayout
{
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        // Allocates the space but does not draw anything.
        context.TryAllocate(this);
        return new LayoutResult([], LayoutStatus.IsFullyDrawn);
    }
}
