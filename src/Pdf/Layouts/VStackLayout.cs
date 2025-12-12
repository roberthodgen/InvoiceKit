namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class VStackLayout : ILayout
{
    private readonly Queue<ILayout> _children;

    private readonly ILayout? _header;

    private readonly ILayout? _footer;

    internal VStackLayout(List<ILayout> children, ILayout? header, ILayout? footer)
    {
        _footer = footer;
        _header = header;
        _children = new Queue<ILayout>(children);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (_children.Count == 0)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var drawables = new List<IDrawable>();

        // Lay out the header
        drawables.AddRange(LayoutHeader(context).Drawables);

        // Get footer size for child context
        var footerSize = _footer?.Measure(context.Available.Size).Height ?? 0f;

        // Lay out the children
        while (_children.Count > 0)
        {
            var childContext = context.GetChildContext(new SKRect(
                context.Available.Left,
                context.Available.Top,
                context.Available.Right,
                context.Available.Bottom - footerSize));
            var layoutResult = _children.Peek().Layout(childContext);
            drawables.Add(new DebugDrawable(childContext.Allocated, DebugDrawable.AllocatedColor));
            drawables.AddRange(layoutResult.Drawables);
            context.CommitChildContext(childContext);

            if (layoutResult.Status == LayoutStatus.NeedsNewPage)
            {
                // Lay out the footer
                drawables.AddRange(LayoutFooter(context).Drawables);
                return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
            }

            _children.Dequeue();
        }

        // Lay out the footer
        drawables.AddRange(LayoutFooter(context).Drawables);
        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }

    public SKSize Measure(SKSize available)
    {
        if (_children.Count == 0)
        {
            return SKSize.Empty;
        }

        var headerHeight = _header?.Measure(available).Height ?? 0f;
        var footerHeight = _footer?.Measure(available).Height ?? 0f;
        var sumChildHeight = _children.Sum(child => child.Measure(available).Height);
        var totalHeight = headerHeight + footerHeight + sumChildHeight;
        return new SKSize(available.Width, totalHeight);
    }

    private LayoutResult LayoutHeader(LayoutContext context)
    {
        if (_header is not null)
        {
            var headerResult = _header.Layout(context);
            if (headerResult.Status == LayoutStatus.NeedsNewPage)
            {
                return new LayoutResult([], LayoutStatus.NeedsNewPage);
            }

            return headerResult;
        }

        return new LayoutResult([], LayoutStatus.IsFullyDrawn);
    }

    private LayoutResult LayoutFooter(LayoutContext context)
    {
        if (_footer is not null)
        {
            var footerResult = _footer.Layout(context);
            if (footerResult.Status == LayoutStatus.NeedsNewPage)
            {
                return new LayoutResult([], LayoutStatus.NeedsNewPage);
            }

            return footerResult;
        }

        return new LayoutResult([], LayoutStatus.IsFullyDrawn);
    }
}
