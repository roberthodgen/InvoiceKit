namespace InvoiceKit.Pdf;

/// <summary>
/// A layout represents a stateful object that will attempt to split itself across multiple pages. While doing so, it
/// may have its <see cref="Layout"/> method called multiple times. <see cref="ILayout"/> objects should track what
/// remains to be laid out and ensure no <see cref="IDrawable"/> is laid out more than once.
/// </summary>
public interface ILayout : IMeasurable
{
    /// <summary>
    /// Used by VStack and HStack to lay out their children.
    /// Will be called multiple times. Every object is responsible for maintaining its own state and preventing
    /// duplication.
    /// </summary>
    LayoutResult Layout(LayoutContext context, LayoutType layoutType);
}
