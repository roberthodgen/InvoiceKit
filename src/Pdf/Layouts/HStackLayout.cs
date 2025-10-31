namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class HStackLayout(List<ILayout> columns) : ILayout
{
    private bool _drawn;

    public IReadOnlyCollection<ILayout> Children => throw new NotImplementedException();

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
        var columnSize = new SKSize(context.Available.Width / columns.Count, context.Available.Height);

        // Loops for the number of columns once. Children that need a new page are added back to the stack.
        foreach (var index in Enumerable.Range(0, columns.Count))
        {
            var point = context.Available.Location;
            point.Offset(columnSize.Width * index, 0);
            var childContext = context.GetChildContext(SKRect.Create(point, columnSize));
            var result = columns[index].Layout(childContext);
            results.Add(
                new ColumnResult(
                    [new DebugDrawable(childContext.Allocated), ..result.Drawables,],
                    result.Status,
                    childContext));
        }

        var maxHeight = results.MaxBy(result => result.Context.Allocated.Height);
        context.CommitChildContext(maxHeight!.Context);
        var status = LayoutStatus.IsFullyDrawn;
        if (results.Any(result => result.Status == LayoutStatus.NeedsNewPage))
        {
            status = LayoutStatus.NeedsNewPage;
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
