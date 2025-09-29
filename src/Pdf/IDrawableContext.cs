namespace InvoiceKit.Pdf;

using SkiaSharp;

/// <summary>
/// Wraps a PDF canvas for drawing and provides debug information.
/// </summary>
public interface IDrawableContext : IDisposable
{
    /// <summary>
    /// The canvas to draw on.
    /// </summary>
    SKCanvas Canvas { get; }

    /// <summary>
    /// Indicates whether debug information should be drawn.
    /// </summary>
    bool Debug { get; }
}
