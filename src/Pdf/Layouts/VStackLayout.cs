namespace InvoiceKit.Pdf.Layouts;

using Geometry;

internal class VStackLayout : ILayout
{
    private readonly List<ILayout> _children;

    private readonly ILayout? _header;

    private readonly ILayout? _footer;

    internal VStackLayout(List<ILayout> children, ILayout? header, ILayout? footer)
    {
        _footer = footer;
        _header = header;
        _children = children;
    }

    public LayoutResult Layout(ILayoutContext context)
    {
        if (_children.Count == 0)
        {
            return LayoutResult.FullyDrawn([]);
        }

        var childLayouts = new List<ChildLayout>();
        foreach (var nthChild in Enumerable.Range(0, _children.Count))
        {
            var layout = ChildLayout.CreateChild(_children[nthChild], context);
            if (nthChild == 0 && _header is not null)
            {
                // TODO compose with header into a non-breakable unit
                childLayouts.Add(ChildLayout.CreateChild(_header, context));
            }

            childLayouts.Add(layout);

            if (nthChild == _children.Count - 1 && _footer is not null)
            {
                // TODO compose with footer into a non-breakable unit
                childLayouts.Add(ChildLayout.CreateChild(_footer, context));
            }
        }

        return LayoutResult.Deferred(childLayouts);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext();
    }

    public ILayoutContext GetContext(ILayoutContext parentContext, OuterRect intersectingRect)
    {
        return parentContext.GetVerticalChildContext(intersectingRect);
    }
}
