namespace InvoiceKit.Pdf.Layouts;

using Geometry;
using SkiaSharp;

internal class ZStack(List<ILayout> children) : ILayout
{
    public LayoutResult Layout(ILayoutContext context)
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
        return LayoutResult.FullyDrawn([]);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        throw new NotImplementedException();
    }

    public ILayoutContext GetContext(ILayoutContext parentContext, OuterRect intersectingRect)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Computes the SKSize needed to fit both SKSizes.
    /// </summary>
    private static SKSize Fit(SKSize a, SKSize b) => new (Math.Max(a.Width, b.Width), Math.Max(a.Height, b.Height));
}
