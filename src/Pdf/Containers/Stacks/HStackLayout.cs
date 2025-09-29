namespace InvoiceKit.Pdf.Containers.Stacks;

using SkiaSharp;

internal class HStackLayout : ILayout
{
    public bool IsFullyDrawn { get; set; }

    private List<ILayout> Columns { get; }

    internal HStackLayout(List<ILayout> columns)
    {
        Columns = columns;
    }

    /// <summary>
    /// Horizontal stack layout that will split into columns based on the number of children.
    /// </summary>
    public LayoutResult Layout(LayoutContext context)
    {
        if (Columns.Count == 0 || IsFullyDrawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var results = new List<ColumnResult>();
        var columnWidth = context.Available.Width / Columns.Count;

        // Loops for the number of columns once. Children that need a new page are added back to the stack.
        foreach (var i in Enumerable.Range(0, Columns.Count))
        {
            var column = Columns[i];

            var left = context.Available.Left + (columnWidth * i);

            var childContext = new LayoutContext(
                new SKRect(
                    left,
                    context.Available.Top,
                    left + columnWidth,
                    context.Available.Bottom));

            var result = column.Layout(childContext);
            results.Add(new ColumnResult(result.Drawables, result.Status, childContext));
        }

        var maxHeight = results.MaxBy(result => result.Context.Allocated);
        context.CommitChildContext(maxHeight!.Context);
        var status = LayoutStatus.IsFullyDrawn;
        foreach (var result in results)
        {
            if (result.Status == LayoutStatus.NeedsNewPage)
            {
                status = LayoutStatus.NeedsNewPage;
                break;
            }
        }
        var drawables = results.SelectMany(result => result.Drawables).ToList();

        if (status == LayoutStatus.IsFullyDrawn)
        {
            IsFullyDrawn = true;
        }

        return new LayoutResult(drawables, status);
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width / Columns.Count, available.Height);
    }

    private record ColumnResult(IReadOnlyCollection<IDrawable> Drawables, LayoutStatus Status, LayoutContext Context);
}
