namespace InvoiceKit.Pdf;

using ExCSS;
using SkiaSharp;

public interface ILayoutContext
{
    /// <summary>
    /// Available space for drawing.
    /// </summary>
    public SKRect Available { get; }
}
