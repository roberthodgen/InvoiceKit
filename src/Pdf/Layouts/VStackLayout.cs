namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

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

        // Todo: header and footer might not render properly, might act like regular layout instead of repeating.
        // Header and footer were allocated before children leaving space for header and footer at all times.
        // Might be a way to handle this in the layout engine instead of in the stacks.

        var childLayouts = new List<ChildLayout>();

        // if (_header is not null)
        // {
        //     childLayouts.Add(new ChildLayout(_header, context.Available));
        // }

        childLayouts.AddRange(_children.Select(child => new ChildLayout(child, context)));

        // if (_footer is not null)
        // {
        //     childLayouts.Add(new ChildLayout(_footer, context.Available));
        // }

        return LayoutResult.Deferred(childLayouts);
    }
}
