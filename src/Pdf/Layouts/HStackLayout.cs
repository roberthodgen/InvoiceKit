namespace InvoiceKit.Pdf.Layouts;

using Geometry;
using SkiaSharp;

internal class HStackLayout(List<ILayout> columns) : ILayout
{
    /// <summary>
    /// Horizontal stack layout that will split into columns based on the number of children.
    /// </summary>
    public LayoutResult Layout(ILayoutContext context)
    {
        return LayoutResult.Deferred(GetChildLayouts(context));
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetHorizontalChildContext();
    }

    public ILayoutContext GetContext(ILayoutContext parentContext, OuterRect intersectingRect)
    {
        return parentContext.GetHorizontalChildContext(intersectingRect);
    }

    private List<ChildLayout> GetChildLayouts(ILayoutContext context)
    {
        var result = new List<ChildLayout>();
        foreach (var i in Enumerable.Range(0, columns.Count))
        {
            var nthColumn = columns[i];
            result.Add(ChildLayout.CreateChildIntersecting(nthColumn, context, GetContextForNthColumn(i, context)));
        }

        return result;
    }

    private OuterRect GetContextForNthColumn(int nthColumn, ILayoutContext context)
    {
        var columnSize = GetColumnSize(context);
        var left = context.Available.Left + columnSize.Width * nthColumn;
        return new (left, context.Available.Top, left + columnSize.Width, context.Available.Height);
    }

    private SKSize GetColumnSize(ILayoutContext context)
    {
        return new SKSize(context.Available.Width / columns.Count, context.Available.Height);
    }
}
