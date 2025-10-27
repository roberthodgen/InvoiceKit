namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class HorizontalRuleLayout : ILayout
{
    private bool _drawn;

    public SKSize Measure(SKRect available)
    {
        return new SKSize(available.Width, 1);
    }

    public LayoutResult Layout(LayoutContext context, LayoutType layoutType)
    {
        if (_drawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var listDrawables = new List<IDrawable>();
        var size = Measure(context.Available);

        var rect = new SKRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Left + size.Width,
            context.Available.Top + size.Height);

        if (context.TryAllocateRect(rect))
        {
            if (layoutType == LayoutType.DrawOnceElement)
            {
                _drawn = true;
            }
            listDrawables.Add(new HorizontalRuleDrawable(rect));
            return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
        }

        return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
    }
}
