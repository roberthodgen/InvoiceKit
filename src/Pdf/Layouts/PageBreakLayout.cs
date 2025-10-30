namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class PageBreakLayout : ILayout
{
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, available.Height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        // Allocates the rest of the available space and does not return drawables.
        context.TryAllocate(context.Available.Size);
        return new LayoutResult([], LayoutStatus.IsFullyDrawn);
    }
}
