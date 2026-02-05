namespace InvoiceKit.Pdf.Layouts;

using Geometry;

internal class HStackLayout(List<ILayout> columns, List<IColumnWidth>? columnWidths = null) : ILayout
{
    private float _left;
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
        _left = context.Available.Left;
        var result = new List<ChildLayout>();
        foreach (var i in Enumerable.Range(0, columns.Count))
        {
            var columnRect = GetRectForNthColumn(i, context, _left);
            _left += columnRect.Width;
            result.Add(ChildLayout.CreateChildIntersecting(columns[i], context, columnRect));
        }

        return result;
    }

    private OuterRect GetRectForNthColumn(int nthColumn, ILayoutContext context, float left)
    {
        var width = columnWidths is not null
            ? columnWidths[nthColumn].GetColumnWidth(context).Width
            : context.Available.Width / columns.Count;

        if (left + width > context.Available.Right)
        {
            throw new ApplicationException($"Column width exceeds available space at column {nthColumn}");
        }

        return new OuterRect(left, context.Available.Top, left + width, context.Available.Bottom);
    }
}
