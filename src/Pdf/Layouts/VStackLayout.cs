namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class VStackLayout(List<ILayout> children, ILayout? header, ILayout? footer) : ILayout
{
    private readonly ILayout? _header = header;

    private readonly ILayout? _footer = footer;

    private readonly Queue<ILayout> _children = new (children);

    private bool _drawn;

    public LayoutResult Layout(LayoutContext context, LayoutType layoutType)
    {
        if (_children.Count == 0 || _drawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var drawables = new List<IDrawable>();
        LayoutHeader(context, drawables);

        // Creates a new context that saves space for the footer
        var footerSize = _footer?.Measure(context.Available) ?? SKSize.Empty;
        var footerContext = context.GetChildContext(
            new SKRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Right,
            context.Available.Bottom + footerSize.Height));

        while (_children.Count > 0)
        {
            var childContext = footerContext.GetChildContext();
            var layout = _children.Peek();
            var layoutResult = layout.Layout(childContext, layoutType);
            drawables.Add(new DebugDrawable(childContext.Allocated));
            drawables.AddRange(layoutResult.Drawables);
            footerContext.CommitChildContext(childContext);

            if (layoutResult.Status == LayoutStatus.IsFullyDrawn)
            {
                _children.Dequeue();
            }
            else
            {
                context.CommitChildContext(footerContext);
                LayoutFooter(context, drawables);
                return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
            }
        }

        if (layoutType == LayoutType.DrawOnceElement)
        {
            _drawn = true;
        }
        context.CommitChildContext(footerContext);
        LayoutFooter(context, drawables);
        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }

    public SKSize Measure(SKRect available)
    {
        if (_children.Count == 0)
        {
            return new SKSize(available.Width, available.Height);
        }
        var maxHeight = _children.Max(child => child.Measure(available).Height);
        return new SKSize(available.Width, maxHeight);
    }

    private void LayoutHeader(LayoutContext context, List<IDrawable> drawables)
    {
        if (_header is not null)
        {
            var headerResult = _header.Layout(context, LayoutType.RepeatingElement);
            drawables.AddRange(headerResult.Drawables);
        }
    }

    private void LayoutFooter(LayoutContext context, List<IDrawable> drawables)
    {
        if (_footer is not null)
        {
            var footerResult = _footer.Layout(context, LayoutType.RepeatingElement);
            drawables.AddRange(footerResult.Drawables);
        }
    }
}
