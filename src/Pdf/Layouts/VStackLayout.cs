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

        var stackContext = context.GetChildContext(Style.GetContentRect(context.Available));

        var footerHeight = _footer?.Measure(stackContext.Available.Size).Height ?? 0f;

        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
        }

        // Lay out the header
        drawables.AddRange(LayoutHeader(stackContext).Drawables);

        // Lay out the children
        while (_children.Count > 0)
        {
            var childContext = stackContext.GetChildContext(new SKRect(
                stackContext.Available.Left,
                stackContext.Available.Top,
                stackContext.Available.Right,
                stackContext.Available.Bottom - footerHeight));
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
                drawables.Insert(0, new BackgroundDrawable(Style.GetBackgroundRect(stackContext.Allocated), Style.BackgroundToPaint()));

                // Add debug drawables for margin and padding
                drawables.Add(new DebugDrawable(Style.GetMarginDebugRect(stackContext.Allocated), DebugDrawable.MarginDebug));
                drawables.Add(new DebugDrawable(Style.GetBackgroundRect(stackContext.Allocated), DebugDrawable.PaddingDebug));

                context.CommitChildContext(stackContext);

                return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
            }

            _children.Dequeue();
        }

        // Lay out the footer
        drawables.AddRange(LayoutFooter(stackContext).Drawables);

        // Add background and border drawables
        drawables.Add(new BorderDrawable(Style.GetBorderRect(stackContext.Allocated), Style.Border));
        drawables.Insert(0, new BackgroundDrawable(Style.GetBackgroundRect(stackContext.Allocated), Style.BackgroundToPaint()));

        // Add debug drawables for margin and padding
        drawables.Add(new DebugDrawable(Style.GetMarginDebugRect(stackContext.Allocated), DebugDrawable.MarginDebug));
        drawables.Add(new DebugDrawable(Style.GetBackgroundRect(stackContext.Allocated), DebugDrawable.PaddingDebug));

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
        var sizeAfterStyle = Style.GetSizeAfterStyle(available);
        var headerHeight = _header?.Measure(sizeAfterStyle).Height ?? 0f;
        var footerHeight = _footer?.Measure(sizeAfterStyle).Height ?? 0f;
        var sumChildHeight = _children.Sum(child => child.Measure(sizeAfterStyle).Height);
        var totalHeight = headerHeight + footerHeight + sumChildHeight + styleSize.Height;
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
