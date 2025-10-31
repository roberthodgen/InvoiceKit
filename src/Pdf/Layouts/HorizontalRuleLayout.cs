namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class HorizontalRuleLayout : ILayout
{
    private bool _drawn;

    public IReadOnlyCollection<ILayout> Children => [];

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, 1);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (_drawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var listDrawables = new List<IDrawable>();
        var size = Measure(context.Available.Size);

        var rect = new SKRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Left + size.Width,
            context.Available.Top + size.Height);

        if (context.TryAllocateRect(rect))
        {
            listDrawables.Add(new HorizontalRuleDrawable(rect));
            _drawn = true;
            return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
        }

        return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
    }
}
