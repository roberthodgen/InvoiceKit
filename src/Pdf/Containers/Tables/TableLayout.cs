namespace InvoiceKit.Pdf.Containers.Tables;

using SkiaSharp;

internal class TableLayout : ILayout
{
    private bool _drawn { get; set; }

    private List<TableRowViewBuilder> Headers { get; }

    private List<TableRowViewBuilder> Rows { get; }

    private bool ShowRowSeparators { get; }

    internal TableLayout(List<TableRowViewBuilder> headers, List<TableRowViewBuilder> rows, bool showRowSeparators)
    {
        Headers = headers;
        Rows = rows;
        ShowRowSeparators = showRowSeparators;
    }

    public SKSize Measure(SKRect available)
    {
        var width = available.Width; // always fills the available width
        var height = Rows.Sum(row => row.Measure(available).Height);
        height += Headers.Sum(row => row.Measure(available).Height);
        return new SKSize(width, height);
    }

    // Todo: Fix tables
    public LayoutResult Layout(LayoutContext context,  LayoutType layoutType)
    {
        if (_drawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var listDrawables = new List<IDrawable>();
        var totalSize = new SKSize();
        var top = context.Available.Top;

        foreach (var row in Headers)
        {
            while (true)
            {
                var rowHeight = row.Measure(context.Available);
                var rect = new SKRect(context.Available.Left, top, context.Available.Right, top + rowHeight.Height);
                if (context.TryAllocateRect(rect))
                {
                    listDrawables.Add(new TableRowDrawable(rect, row));
                    top += rowHeight.Height;
                    totalSize += rowHeight.Size;
                    break;
                }
                return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
            }
        }

        foreach (var row in Rows)
        {
            while (true)
            {
                var rowHeight = row.Measure(context.Available);
                var rect = new SKRect(context.Available.Left, top, context.Available.Right, top + rowHeight.Height);
                if (context.TryAllocateRect(rect))
                {
                    listDrawables.Add(new TableRowDrawable(rect, row));
                    top += rowHeight.Height;
                    totalSize += rowHeight.Size;
                    break;
                }
                return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
            }
        }

        if (layoutType == LayoutType.DrawOnceElement)
        {
            _drawn = true;
        }
        return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
    }
}
