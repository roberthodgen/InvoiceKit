namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class PageBreakLayout : ILayout
{
    public SKSize Measure(SKRect available)
    {
        return new SKSize(available.Width, available.Height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        // Allocates the rest of the available space and does not return drawables.
        context.TryAllocateRect(new SKRect(
                context.Available.Left, context.Available.Top, context.Available.Right, context.Available.Bottom));

        return new LayoutResult([], LayoutStatus.IsFullyDrawn);
    }
}
