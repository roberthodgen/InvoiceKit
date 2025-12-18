namespace InvoiceKit.Pdf;

using Geometry;
using SkiaSharp;

/// <summary>
/// A child layout that has been positioned by its parent.
/// </summary>
public sealed class ChildLayout
{
    /// <summary>
    /// The child <see cref="ILayout"/> to be laid out.
    /// </summary>
    public ILayout Layout { get; }

    /// <summary>
    /// The context for the child to be laid in to.
    /// </summary>
    public ILayoutContext Context { get; }

    private ChildLayout(ILayout layout, ILayoutContext context)
    {
        Layout = layout;
        Context = context;
    }

    /// <summary>
    /// Factory method for a new child layout object. Creates and links a child layout context.
    /// </summary>
    public static ChildLayout CreateChild(ILayout layout, ILayoutContext parentContext) =>
        new (layout, layout.GetContext(parentContext));

    /// <summary>
    /// Factory method for a new child layout object. Creates and links a child layout context that intersects with a
    /// given rect.
    /// </summary>
    public static ChildLayout CreateChildIntersecting(
        ILayout layout,
        ILayoutContext parentContext,
        OuterRect intersectingRect) =>
        new (layout, layout.GetContext(parentContext, intersectingRect));

    /// <summary>
    /// Creates a root layout.
    /// </summary>
    internal static ChildLayout CreateRoot(ILayout layout, RootLayoutContext rootLayout)
    {
        return new ChildLayout(layout, rootLayout);
    }
}
