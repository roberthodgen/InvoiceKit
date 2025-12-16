namespace InvoiceKit.Pdf.Layouts;

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
            return LayoutResult.FullyDrawn([]);
        }

        // Todo: header and footer might not render properly, might act like regular layout instead of repeating.
        // Header and footer were allocated before children leaving space for header and footer at all times.
        // Might be a way to handle this in the layout engine instead of in the stacks.

        var childLayouts = new List<ChildLayout>();

        // if (_header is not null)
        // {
        //     childLayouts.Add(new ChildLayout(_header, context.Available));
        // }

        while (_children.Count > 0)
        {
            var childContext = context.GetChildContext(); // Todo: Account for header and footer size
            var child =  _children.Peek();
            var childSize = child.Measure(context.Available.Size);
            if (context.TryAllocate(childSize))
            {
                childLayouts.Add(new ChildLayout(child, childContext));
                _children.Dequeue();
                context.CommitChildContext(childContext);
            }
            else
            {
                return LayoutResult.NeedsNewPage([]);
            }
        }

        // if (_footer is not null)
        // {
        //     childLayouts.Add(new ChildLayout(_footer, context.Available));
        // }

        return LayoutResult.Deferred(childLayouts);
    }

    public SKSize Measure(SKSize available)
    {
        return SKSize.Empty; // Todo: update the return to be a MeasureResult
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
}
