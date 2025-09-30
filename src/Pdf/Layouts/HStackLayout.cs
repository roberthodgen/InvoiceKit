namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class HStackLayout(List<ILayout> columns) : ILayout
{
    private bool _drawn;

    /// <summary>
    /// Horizontal stack layout that will split into columns based on the number of children.
    /// </summary>
    public LayoutResult Layout(LayoutContext context)
    {
        if (columns.Count == 0 || _drawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var results = new List<ColumnResult>();
        var columnWidth = context.Available.Width / columns.Count;

        // Loops for the number of columns once. Children that need a new page are added back to the stack.
        foreach (var i in Enumerable.Range(0, columns.Count))
        {
            var column = columns[i];
            var left = context.Available.Left + (columnWidth * i);

            var childContext = new LayoutContext(
                new SKRect(
                    left,
                    context.Available.Top,
                    left + columnWidth,
                    context.Available.Bottom));

            var columnDrawables = new List<IDrawable>();
            var result = column.Layout(childContext);
            columnDrawables.Add(new DebugDrawable(childContext.Allocated));
            columnDrawables.AddRange(result.Drawables);
            results.Add(new ColumnResult(columnDrawables, result.Status, childContext));
        }

        var maxHeight = results.MaxBy(result => result.Context.Allocated.Height);
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
            _drawn = true;
        }

        return new LayoutResult(drawables, status);
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width / columns.Count, available.Height);
    }

    private record ColumnResult(IReadOnlyCollection<IDrawable> Drawables, LayoutStatus Status, LayoutContext Context);
}
