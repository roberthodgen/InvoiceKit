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
        _children = new (children);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (_children.Count == 0)
        {
            return LayoutResult.FullyDrawn([]); // no children
        }

        var drawables = new List<IDrawable>();

        // Lay out the header
        drawables.AddRange(LayoutHeader(context).Drawables);

        // Get footer size for child context
        var footerSize = _footer?.Measure(context.Available.Size) ?? SKSize.Empty;

        // Lay out the children
        while (_children.Count > 0)
        {
            var childContext = context.GetChildContext(new SKRect(
                context.Available.Left,
                context.Available.Top,
                context.Available.Right,
                context.Available.Bottom - footerSize.Height));
            var layoutResult = _children.Peek().Layout(childContext);
            drawables.Add(new DebugDrawable(childContext.Allocated));
            drawables.AddRange(layoutResult.Drawables);
            context.CommitChildContext(childContext);

            if (layoutResult.Status == LayoutStatus.NeedsNewPage)
            {
                // Lay out the footer
                drawables.AddRange(LayoutFooter(context).Drawables);

                return LayoutResult.NeedsNewPage(drawables); // TODO use deferred
            }

            _children.Dequeue();
        }

        // Lay out the footer
        drawables.AddRange(LayoutFooter(context).Drawables);

        return LayoutResult.FullyDrawn(drawables); // TODO use deferred
    }

    public SKSize Measure(SKSize available)
    {
        if (_children.Count == 0)
        {
            return SKSize.Empty;
        }

        var headerHeight = _header?.Measure(available).Height ?? 0f;
        var footerHeight = _footer?.Measure(available).Height ?? 0f;
        var maxChildHeight = _children.Sum(child => child.Measure(available).Height);
        var totalHeight = headerHeight + footerHeight + maxChildHeight;
        return new SKSize(available.Width, totalHeight);
    }

    private LayoutResult LayoutHeader(LayoutContext context)
    {
        if (_header is not null)
        {
            var headerResult = _header.Layout(context);
            if (headerResult.Status == LayoutStatus.NeedsNewPage)
            {
                return LayoutResult.NeedsNewPage([]);
            }

            return headerResult;
        }

        return LayoutResult.FullyDrawn([]);
    }

    private LayoutResult LayoutFooter(LayoutContext context)
    {
        if (_footer is not null)
        {
            var footerResult = _footer.Layout(context);
            if (footerResult.Status == LayoutStatus.NeedsNewPage)
            {
                return LayoutResult.NeedsNewPage([]);
            }

            return footerResult;
        }

        return LayoutResult.FullyDrawn([]);
    }
}
