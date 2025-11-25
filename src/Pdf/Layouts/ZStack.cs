namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class ZStack(List<ILayout> children) : ILayout
{
    /// <summary>
    /// A ZStack's measured size is one that fits all children.
    /// </summary>
    public SKSize Measure(SKSize available)
    {
        return children.Aggregate(SKSize.Empty, (size, child) => Fit(size, child.Measure(available)));
    }

    public LayoutResult Layout(LayoutContext context)
    {
        // var size = Measure(context.Available.Size);
        // if (context.TryAllocate(this))
        // {
        //     var rendered = new List<IDrawable>();
        //     foreach (var child in children)
        //     {
        //         var childContext = context.GetChildContext();
        //         var result = child.Layout(childContext);
        //         context.CommitChildContext(childContext);
        //         rendered.AddRange(result.Drawables);
        //     }
        //
        //     return new LayoutResult(rendered, LayoutStatus.IsFullyDrawn);
        // }
        return new LayoutResult(LayoutStatus.IsFullyDrawn, []);
    }

    /// <summary>
    /// Computes the SKSize needed to fit both SKSizes.
    /// </summary>
    private static SKSize Fit(SKSize a, SKSize b) => new (Math.Max(a.Width, b.Width), Math.Max(a.Height, b.Height));
}
