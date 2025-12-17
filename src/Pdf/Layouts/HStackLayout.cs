namespace InvoiceKit.Pdf.Layouts;

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
        return parentContext.GetHorizontalChildContext(parentContext.Available);
    }

    private List<ChildLayout> GetChildLayouts(ILayoutContext context)
    {
        var result = new List<ChildLayout>();
        foreach (var i in Enumerable.Range(0, columns.Count))
        {
            var nthColumn = columns[i];
            result.Add(new ChildLayout(nthColumn, GetContextForNthColumn(i, context)));
        }

        return result;
    }

    private ILayoutContext GetContextForNthColumn(int nthColumn, ILayoutContext context)
    {
        var columnSize = GetColumnSize(context);
        var point = context.Available.Location;
        point.Offset(columnSize.Width * nthColumn, 0);
        return context.GetVerticalChildContext(SKRect.Create(point, columnSize));
    }

    private SKSize GetColumnSize(ILayoutContext context)
    {
        return new SKSize(context.Available.Width / columns.Count, context.Available.Height);
    }
}
