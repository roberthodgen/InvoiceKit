namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;
using Styles;

internal class HStackLayout(List<ILayout> columns, BlockStyle style) : ILayout
{
    public BlockStyle Style { get; } = style;

    /// <summary>
    /// Horizontal stack layout that will split into columns based on the number of children.
    /// </summary>
    public LayoutResult Layout(LayoutContext context)
    {
        if (columns.Count == 0)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var drawables = new List<IDrawable>();

        var results = new List<ColumnResult>();

        var contentRect = Style.GetContentRect(context.Available);
        var stackContext = context.GetChildContext(contentRect);
        var columnSize = new SKSize(contentRect.Width / columns.Count, contentRect.Height);

        // Loops for the number of columns once. Children that need a new page are added back to the stack.
        foreach (var index in Enumerable.Range(0, columns.Count))
        {
            var point = stackContext.Available.Location;
            point.Offset(columnSize.Width * index, 0);
            var childContext = stackContext.GetChildContext(SKRect.Create(point, columnSize));
            var result = columns[index].Layout(childContext);
            results.Add(new ColumnResult(result.Drawables, result.Status, childContext));
            drawables.Add(new DebugDrawable(childContext.Allocated, DebugDrawable.AllocatedDebug));
        }

        var maxHeight = results.MaxBy(result => result.Context.Allocated.Height);
        stackContext.CommitChildContext(maxHeight!.Context);
        context.CommitChildContext(stackContext);

        // Add background and border drawables.
        drawables.Add(new BorderDrawable(Style.GetBorderRect(stackContext.Allocated), Style.Border));
        drawables.Add(new BackgroundDrawable(Style.GetBackgroundRect(stackContext.Allocated), Style.BackgroundToPaint()));

        // Add margin and padding debug drawables.
        drawables.Add(new DebugDrawable(Style.GetMarginDebugRect(stackContext.Allocated), DebugDrawable.MarginDebug));
        drawables.Add(new DebugDrawable(Style.GetPaddingDebugRect(stackContext.Allocated), DebugDrawable.PaddingDebug));

        drawables.AddRange(results.SelectMany(result => result.Drawables).ToList());
        if (results.Any(result => result.Status == LayoutStatus.NeedsNewPage))
        {
            return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
        }

        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }

    public SKSize Measure(SKSize available)
    {
        if (columns.Count == 0)
        {
            return SKSize.Empty;
        }
        var maxHeight = columns.Max(column => column.Measure(available).Height);
        return new SKSize(available.Width, maxHeight);
    }

    private record ColumnResult(IReadOnlyCollection<IDrawable> Drawables, LayoutStatus Status, LayoutContext Context);
}
