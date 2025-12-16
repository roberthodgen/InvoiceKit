namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class HStackLayout(List<ILayout> columns) : ILayout
{
    /// <summary>
    /// Horizontal stack layout that will split into columns based on the number of children.
    /// </summary>
    public LayoutResult Layout(LayoutContext context)
    {
        return LayoutResult.Deferred(GetChildLayouts(context));
    }

    private List<ChildLayout> GetChildLayouts(LayoutContext context)
    {
        var result = new List<ChildLayout>();
        foreach (var i in Enumerable.Range(0, columns.Count))
        {
            var nthColumn = columns[i];
            result.Add(new ChildLayout(nthColumn, GetRectForNthColumn(i, context)));
        }

        return result;
    }

    private LayoutContext GetRectForNthColumn(int nthColumn, LayoutContext context)
    {
        var columnSize = GetColumnSize(context);
        var point = context.Available.Location;
        point.Offset(columnSize.Width * nthColumn, 0);
        return context.GetChildContext(SKRect.Create(point, columnSize));
    }

    private SKSize GetColumnSize(LayoutContext context)
    {
        return new SKSize(context.Available.Width / columns.Count, context.Available.Height);
    }
}
