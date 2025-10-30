namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class VStackLayout : ILayout
{
    private readonly Queue<ILayout> _children;

    private readonly ILayout? _header;

    private readonly ILayout? _footer;

    private readonly bool _repeating;

    internal VStackLayout(List<ILayout> children, ILayout? header, ILayout? footer, bool repeating)
    {
        _footer = footer;
        _header = header;
        _children = new (children);
        _repeating = repeating;
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (_children.Count == 0)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        if (_repeating)
        {
            return RepeatingLayout(context);
        }

        var drawables = new List<IDrawable>();

        // Lay out the header
        if (_header is not null)
        {
            var headerContext = context.GetChildContext();
            var headerResult = LayoutHeader(headerContext);
            drawables.AddRange(headerResult.Drawables);
            drawables.Add(new DebugDrawable(headerContext.Allocated));
            context.CommitChildContext(headerContext);
        }

        // Get footer size and context to use on child layout.
        var footerSize = _footer?.Measure(context.Available.Size) ?? SKSize.Empty;
        var footerContext = context.GetChildContext(
            new SKRect(
                context.Available.Left,
                context.Available.Bottom - footerSize.Height,
                context.Available.Right,
                context.Available.Bottom));

        // Lay out the footer
        if (_footer is not null)
        {
            var footerResult = LayoutFooter(footerContext);
            drawables.AddRange(footerResult.Drawables);
            drawables.Add(new DebugDrawable(footerContext.Allocated));
        }

        var rectWithoutFooter = new SKRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Right,
            context.Available.Bottom - footerSize.Height);

        // Lay out all the children, then commit the footer context.
        while (_children.Count > 0)
        {
            var childContext = context.GetChildContext(rectWithoutFooter);
            var layoutResult = _children.Peek().Layout(childContext);
            drawables.Add(new DebugDrawable(childContext.Allocated));
            drawables.AddRange(layoutResult.Drawables);
            context.CommitChildContext(childContext);

            if (layoutResult.Status == LayoutStatus.NeedsNewPage)
            {
                context.CommitChildContext(footerContext);
                return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
            }

            _children.Dequeue();
        }

        // Commits children, draws footer, and completes layout.
        context.CommitChildContext(footerContext);
        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }

    /// <summary>
    /// Lays out repeating elements and does not remove them from the queue.
    /// </summary>
    /// <remarks>Repeating elements cannot have more repeating elements.</remarks>
    private LayoutResult RepeatingLayout(LayoutContext context)
    {
        var drawables = new List<IDrawable>();

        foreach (var child in _children)
        {
            var childContext = context.GetChildContext();
            var result = child.Layout(childContext);
            drawables.AddRange(result.Drawables);
            context.CommitChildContext(childContext);

            if (result.Status == LayoutStatus.NeedsNewPage)
            {
                return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
            }
        }

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
        var maxChildHeight = _children.Max(child => child.Measure(available).Height);
        var totalHeight = headerHeight + footerHeight + maxChildHeight;
        return new SKSize(available.Width, totalHeight);
    }

    private LayoutResult LayoutHeader(LayoutContext context)
    {
        var headerResult =  _header!.Layout(context);
        if (headerResult.Status == LayoutStatus.NeedsNewPage)
        {
            return new LayoutResult([], LayoutStatus.NeedsNewPage);
        }
        return headerResult;
    }

    private LayoutResult LayoutFooter(LayoutContext context)
    {
        var footerResult = _footer!.Layout(context);
        if (footerResult.Status == LayoutStatus.NeedsNewPage)
        {
            return new LayoutResult([], LayoutStatus.NeedsNewPage);
        }
        return footerResult;
    }
}
