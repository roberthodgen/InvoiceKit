namespace InvoiceKit.Pdf.Containers.Tables;

using Geometry;
using SkiaSharp;

internal class TableLayout : ILayout
{
    private List<TableRowViewBuilder> Headers { get; }

    private List<TableRowViewBuilder> Rows { get; }

    internal TableLayout(List<TableRowViewBuilder> headers, List<TableRowViewBuilder> rows)
    {
        Headers = headers;
        Rows = rows;
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
                var rowHeight = row.Measure(context.Available.ToSize().ToSize());
                var rect = new SKRect(context.Available.Left, top, context.Available.Right, top + rowHeight.Height);
                if (context.TryAllocate(new OuterSize(rect.Size.Width, rect.Size.Height)))
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
                var rowHeight = row.Measure(context.Available.ToSize().ToSize());
                var rect = new SKRect(context.Available.Left, top, context.Available.Right, top + rowHeight.Height);
                if (context.TryAllocate(new OuterSize(rect.Size.Width, rect.Size.Height)))
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

    public ILayoutContext GetContext(ILayoutContext parentContext, OuterRect intersectingRect)
    {
        throw new NotImplementedException();
    }
}
