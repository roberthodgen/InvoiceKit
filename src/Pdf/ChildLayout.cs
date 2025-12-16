namespace InvoiceKit.Pdf;

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
    public LayoutContext Context { get; }

    public ChildLayout(ILayout layout, LayoutContext context)
    {
        Layout = layout;
        Context = context;
    }
}
