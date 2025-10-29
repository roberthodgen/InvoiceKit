namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class SpacingBlockLayout(float height) : ILayout
{
    public SKSize Measure(SKRect available)
    {
        return new SKSize(available.Width, height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        // Allocates the space but does not draw anything.
        context.TryAllocateRect(new SKRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Right,
            context.Available.Top + height));

        return new LayoutResult([], LayoutStatus.IsFullyDrawn);
    }
}
