namespace InvoiceKit.Pdf.Containers.Tables;

using SkiaSharp;

internal class TableLayout : ILayout
{
    public BlockStyle Style { get; }

    private List<TableRowViewBuilder> Headers { get; }

    private List<TableRowViewBuilder> Rows { get; }

    private bool ShowRowSeparators { get; }

    internal TableLayout(
        List<TableRowViewBuilder> headers,
        List<TableRowViewBuilder> rows,
        bool showRowSeparators,
        BlockStyle style)
    {
        Style = style;
        Headers = headers;
        Rows = rows;
        ShowRowSeparators = showRowSeparators;
    }

    public SKSize Measure(SKSize available)
    {
        var height = Rows.Sum(row => row.Measure(available).Height);
        height += Headers.Sum(row => row.Measure(available).Height);
        return new SKSize(available.Width, height);
    }

    // Todo: Fix tables
    public LayoutResult Layout(ILayoutContext context)
    {
        var listDrawables = new List<IDrawable>();
        var totalSize = new SKSize();
        var top = context.Available.Top;

        foreach (var row in Headers)
        {
            while (true)
            {
                var rowHeight = row.Measure(context.Available.Size);
                var rect = new SKRect(context.Available.Left, top, context.Available.Right, top + rowHeight.Height);
                if (context.TryAllocate(rect.Size))
                {
                    listDrawables.Add(new TableRowDrawable(rect, row));
                    top += rowHeight.Height;
                    totalSize += rowHeight;
                    break;
                }
                return LayoutResult.NeedsNewPage(listDrawables);
            }
        }

        foreach (var row in Rows)
        {
            while (true)
            {
                var rowHeight = row.Measure(context.Available.Size);
                var rect = new SKRect(context.Available.Left, top, context.Available.Right, top + rowHeight.Height);
                if (context.TryAllocate(rect.Size))
                {
                    listDrawables.Add(new TableRowDrawable(rect, row));
                    top += rowHeight.Height;
                    totalSize += rowHeight;
                    break;
                }
                return LayoutResult.NeedsNewPage(listDrawables);
            }
        }

        return LayoutResult.FullyDrawn(listDrawables);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        throw new NotImplementedException();
    }
}
