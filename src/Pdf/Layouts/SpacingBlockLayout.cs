namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class SpacingBlockLayout(float height) : ILayout
{
    private bool _drawn;

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (_drawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var size = Measure(context.Available.Size);
        var rect = new SKRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Left + size.Width,
            context.Available.Top + size.Height);

        // Allocates the space but does not draw anything.
        context.TryAllocateRect(rect);

        // Always returns fully drawn
        _drawn = true;
        return new LayoutResult([], LayoutStatus.IsFullyDrawn);
    }
}
