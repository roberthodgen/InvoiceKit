namespace InvoiceKit.Pdf;

using SkiaSharp;

public interface ILayout : IMeasurable
{
    /// <summary>
    /// Used by VStack and HStack to lay out their children.
    /// </summary>
    LayoutResult Layout(LayoutContext context);
}
