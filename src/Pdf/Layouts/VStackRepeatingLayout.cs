namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class VStackRepeatingLayout(List<ILayout> children) : ILayout
{
    public SKSize Measure(SKSize available)
    {
        if (children.Count == 0)
        {
            return SKSize.Empty;
        }

        return new SKSize(available.Width, children.Sum(child => child.Measure(available).Height));
    }

    public LayoutResult Layout(LayoutContext context)
    {
        return new LayoutResult(LayoutStatus.Deferred, GetChildLayouts(context));
    }

    private List<ChildLayout> GetChildLayouts(LayoutContext context)
    {
        var results = new List<ChildLayout>();
        foreach (var child in children)
        {
            var childContext = context.GetChildContext();
            results.Add(new ChildLayout(child, childContext.Allocated));
        }

        return results;
    }
}
