namespace InvoiceKit.Pdf;

using SkiaSharp;

/// <summary>
/// A layout represents a stateful object that will attempt to split itself across multiple pages. While doing so, it
/// may have its <see cref="Layout"/> method called multiple times. <see cref="ILayout"/> objects should track what
/// remains to be laid out and ensure no <see cref="IDrawable"/> is laid out more than once unless desired.
/// </summary>
public interface ILayout
{
    /// <summary>
    /// Used by VStack and HStack to lay out their children.
    /// Will be called multiple times. Every object is responsible for maintaining its own state and preventing
    /// duplication.
    /// </summary>
    /// <param name="context">A context into which all layout and child layout actions should occur.</param>
    LayoutResult Layout(ILayoutContext context);

    /// <summary>
    /// Creates an appropriate <see cref="ILayoutContext"/> for the layout type.
    /// </summary>
    /// <param name="parentContext">A parent context from which to derive a child context.</param>
    ILayoutContext GetContext(ILayoutContext parentContext);

    /// <summary>
    /// Creates an appropriate <see cref="ILayoutContext"/> for the layout within a given rect.
    /// </summary>
    /// <param name="parentContext">A parent context from which to derive a child context.</param>
    /// <param name="intersectingRect">The required intersecting rect for the new context.</param>
    ILayoutContext GetContext(ILayoutContext parentContext, SKRect intersectingRect);
}
