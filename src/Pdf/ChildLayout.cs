namespace InvoiceKit.Pdf;

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

    internal ChildLayout(ILayout layout, ILayoutContext context)
    {
        Layout = layout;
        Context = context;
    }

    public static ChildLayout CreateVertical(ILayout layout, ILayoutContext parentContext)
    {
        return new ChildLayout(layout, parentContext.GetVerticalChildContext(parentContext.Available));
    }

    public static ChildLayout CreateHorizontal(ILayout layout, ILayoutContext parentContext)
    {
        return new ChildLayout(layout, parentContext.GetHorizontalChildContext(parentContext.Available));
    }

    internal static ChildLayout CreateRoot(ILayout layout, RootLayoutContext rootLayout)
    {
        return new ChildLayout(layout, rootLayout);
    }
}
