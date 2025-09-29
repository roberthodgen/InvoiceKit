namespace InvoiceKit.Pdf;

public interface ILayout : IMeasurable
{
    /// <summary>
    /// Used by VStack and HStack to lay out their children.
    /// Will be called multiple times. Every object is responsible for maintaining its own state and preventing duplication.
    /// </summary>
    LayoutResult Layout(LayoutContext context);

    /// <summary>
    /// Should be used to mark the layout complete to prevent duplicate renders.
    /// </summary>
    bool IsFullyDrawn { get; set; }
}
