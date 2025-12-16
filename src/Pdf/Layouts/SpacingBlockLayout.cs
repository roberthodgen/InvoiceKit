namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class SpacingBlockLayout(float height) : ILayout
{
    public LayoutResult Layout(LayoutContext context)
    {
        // Allocates the space but does not draw anything.
        context.TryAllocate(new SKSize(0, height));
        return LayoutResult.FullyDrawn([]);
    }
}
