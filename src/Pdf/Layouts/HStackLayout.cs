namespace InvoiceKit.Pdf.Layouts;

using Geometry;

internal class HStackLayout(List<Column> columns) : ILayout
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
        var left = context.Available.Left;
        var result = new List<ChildLayout>();
        foreach (var i in Enumerable.Range(0, columns.Count))
        {
            var columnSize = columns[i].ColumnWidth.GetColumnSize(context);
            if (left + columnSize.Width > context.Available.Right)
            {
                throw new ApplicationException($"Column {i} exceeds available width.");
            }

            var rect = new OuterRect(left, context.Available.Top, left + columnSize.Width, context.Available.Bottom);
            left += columnSize.Width;
            result.Add(ChildLayout.CreateChildIntersecting(columns[i].Layout, context, rect));
        }

        return result;
    }
}
