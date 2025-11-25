namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;
using Styles;

internal class VStackLayout : ILayout
{
    private BlockStyle Style { get; }

    private readonly Queue<ILayout> _children;

    private readonly ILayout? _header;

    private readonly ILayout? _footer;

    internal VStackLayout(List<ILayout> children, ILayout? header, ILayout? footer, BlockStyle style)
    {
        Style = style;
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
        var contentRect = Style.GetContentRect(context.Available);

        var footerHeight = _footer?.Measure(contentRect.Size).Height ?? 0f;

        var rectWithoutFooter = new SKRect(
            contentRect.Left,
            contentRect.Top,
            contentRect.Right,
            contentRect.Bottom - footerHeight);

        var stackContext = context.GetChildContext(rectWithoutFooter);

        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
        }

        // Lay out the header
        drawables.AddRange(LayoutHeader(stackContext).Drawables);

        // Lay out the children
        while (_children.Count > 0)
        {
            var childContext = stackContext.GetChildContext();
            var layoutResult = _children.Peek().Layout(childContext);
            drawables.Add(new DebugDrawable(childContext.Allocated, DebugDrawable.AllocatedDebug));
            drawables.AddRange(layoutResult.Drawables);
            stackContext.CommitChildContext(childContext);

            if (layoutResult.Status == LayoutStatus.NeedsNewPage)
            {
                // Lay out the footer
                drawables.AddRange(LayoutFooter(stackContext).Drawables);

                // Add background and border drawables
                drawables.Add(new BorderDrawable(Style.GetBorderRect(stackContext.Allocated), Style.Border));
                drawables.Add(new BackgroundDrawable(Style.GetBackgroundRect(stackContext.Allocated), Style.BackgroundToPaint()));

                // Add debug drawables for margin and padding
                drawables.Add(new DebugDrawable(Style.GetMarginDebugRect(stackContext.Allocated), DebugDrawable.MarginDebug));
                drawables.Add(new DebugDrawable(Style.GetPaddingDebugRect(stackContext.Allocated), DebugDrawable.PaddingDebug));

                context.CommitChildContext(stackContext);

                return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
            }

            _children.Dequeue();
        }

        // Lay out the footer
        drawables.AddRange(LayoutFooter(stackContext).Drawables);

        // Add background and border drawables
        drawables.Add(new BorderDrawable(Style.GetBorderRect(stackContext.Allocated), Style.Border));
        drawables.Add(new BackgroundDrawable(Style.GetBackgroundRect(stackContext.Allocated), Style.BackgroundToPaint()));

        // Add debug drawables for margin and padding
        drawables.Add(new DebugDrawable(Style.GetMarginDebugRect(stackContext.Allocated), DebugDrawable.MarginDebug));
        drawables.Add(new DebugDrawable(Style.GetPaddingDebugRect(stackContext.Allocated), DebugDrawable.PaddingDebug));

        context.CommitChildContext(stackContext);

        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }

    public SKSize Measure(SKSize available)
    {
        if (_children.Count == 0)
        {
            return SKSize.Empty;
        }

        var styleSize = Style.GetStyleSize();
        var sizeAfterStyling = new SKSize(available.Width - styleSize.Width, available.Height - styleSize.Height);
        var headerHeight = _header?.Measure(sizeAfterStyling).Height ?? 0f;
        var footerHeight = _footer?.Measure(sizeAfterStyling).Height ?? 0f;
        var maxChildHeight = _children.Sum(child => child.Measure(sizeAfterStyling).Height);
        var totalHeight = headerHeight + footerHeight + maxChildHeight;
        return new SKSize(available.Width - styleSize.Width, totalHeight);
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
