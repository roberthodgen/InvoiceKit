namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class HStackLayout(List<ILayout> columns) : ILayout
{
    private readonly List<ILayout> _columns = [..columns];

    /// <summary>
    /// Horizontal stack layout that will split into columns based on the number of children.
    /// </summary>
    public LayoutResult Layout(LayoutContext context)
    {
        if (_columns.Count == 0)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var results = new List<ColumnResult>();
        var columnSize = new SKSize(context.Available.Width / _columns.Count, context.Available.Height);

        // Loops for the number of columns once. Children that need a new page are added back to the stack.
        foreach (var index in Enumerable.Range(0, _columns.Count))
        {
            var point = context.Available.Location;
            point.Offset(columnSize.Width * index, 0);
            var childContext = context.GetChildContext(SKRect.Create(point, columnSize));
            var result = _columns[index].Layout(childContext);
            results.Add(
                new ColumnResult(
                    [new DebugDrawable(childContext.Allocated), ..result.Drawables,],
                    result.Status,
                    childContext));
        }

        var maxHeight = results.MaxBy(result => result.Context.Allocated.Height);
        context.CommitChildContext(maxHeight!.Context);

        var drawables = results.SelectMany(result => result.Drawables).ToList();
        if (results.Any(result => result.Status == LayoutStatus.NeedsNewPage))
        {
            return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
        }

        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }

    public SKSize Measure(SKRect available)
    {
        if (_columns.Count == 0)
        {
            return SKSize.Empty;
        }
        var maxHeight = _columns.Max(column => column.Measure(available).Height);
        return new SKSize(available.Width, maxHeight);
    }

    private record ColumnResult(IReadOnlyCollection<IDrawable> Drawables, LayoutStatus Status, LayoutContext Context);
}
