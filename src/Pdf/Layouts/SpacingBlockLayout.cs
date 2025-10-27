namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class SpacingBlockLayout(float height) : ILayout
{
    private bool _drawn;

    public SKSize Measure(SKRect available)
    {
        return new SKSize(available.Width, height);
    }

    public LayoutResult Layout(LayoutContext context, LayoutType layoutType)
    {
        if (_drawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var rect = new SKRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Right,
            context.Available.Top + height);

        // Allocates the space but does not draw anything.
        context.TryAllocateRect(rect);

        if (layoutType != LayoutType.RepeatingElement)
        {
            _drawn = true;
        }
        return new LayoutResult([], LayoutStatus.IsFullyDrawn);
    }
}
