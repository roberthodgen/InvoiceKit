namespace InvoiceKit.Pdf.Containers.Tables;

using SkiaSharp;

internal class TableLayout : ILayout
{
    public bool IsFullyDrawn { get; set; }

    private List<TableRowViewBuilder> Headers { get; }

    private List<TableRowViewBuilder> Rows { get; }

    private bool ShowRowSeparators { get; }

    public IReadOnlyCollection<ILayout> Children => throw new NotImplementedException();


    internal TableLayout(List<TableRowViewBuilder> headers, List<TableRowViewBuilder> rows, bool showRowSeparators)
    {
        Headers = headers;
        Rows = rows;
        ShowRowSeparators = showRowSeparators;
    }

    public SKSize Measure(SKSize available)
    {
        var width = available.Width; // always fills the available width
        var height = Rows.Sum(row => row.Measure(available).Height);
        height += Headers.Sum(row => row.Measure(available).Height);
        return new SKSize(width, height);
    }

    // Todo: Fix tables, they do not work at all.
    public LayoutResult Layout(LayoutContext context)
    {
        if (IsFullyDrawn) return new LayoutResult([], LayoutStatus.IsFullyDrawn);

        var listDrawables = new List<IDrawable>();
        var totalSize = new SKSize();
        var top = context.Available.Top;

        foreach (var row in Headers)
        {
            while (true)
            {
                var rowHeight = row.Measure(context.Available.Size);
                var rect = new SKRect(context.Available.Left, top, context.Available.Right, top + rowHeight.Height);
                if (context.TryAllocateRect(rect))
                {
                    listDrawables.Add(new TableRowDrawable(rect, row));
                    top += rowHeight.Height;
                    totalSize += rowHeight;
                    break;
                }
                return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
            }
        }

        foreach (var row in Rows)
        {
            while (true)
            {
                var rowHeight = row.Measure(context.Available.Size);
                var rect = new SKRect(context.Available.Left, top, context.Available.Right, top + rowHeight.Height);
                if (context.TryAllocateRect(rect))
                {
                    listDrawables.Add(new TableRowDrawable(rect, row));
                    top += rowHeight.Height;
                    totalSize += rowHeight;
                    break;
                }
                return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
            }
        }

        return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
    }
}
