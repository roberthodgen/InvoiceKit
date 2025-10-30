namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class SpacingBlockLayout(float height) : ILayout
{
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        // Allocates the space but does not draw anything.
        context.TryAllocate(Measure(context.Available.Size));

        return new LayoutResult([], LayoutStatus.IsFullyDrawn);
    }
}
