namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class VStackRepeatingLayout(List<ILayout> children) : ILayout
{
    public SKSize Measure(SKSize available)
    {
        return SKSize.Empty;
        if (children.Count == 0)
        {
            return SKSize.Empty;
        }

        return new SKSize(available.Width, children.Sum(child => child.Measure(available).Height));
    }

    public LayoutResult Layout(LayoutContext context)
    {
        return LayoutResult.Deferred(children.Select(child => new ChildLayout(child, context)).ToList());
    }
}
