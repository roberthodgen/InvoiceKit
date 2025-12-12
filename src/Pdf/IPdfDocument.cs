namespace InvoiceKit.Pdf;

public interface IPdfDocument : IDisposable
{
    /// <summary>
    /// Completes the build and returns a byte array for use in a file stream.
    /// </summary>
    /// <returns></returns>
    byte[] Build();

    /// <summary>
    /// Configures the document's default style.
    /// </summary>
    /// <param name="configureStyle">The base/parent style which may be modified as needed.</param>
    PdfDocument WithDocumentStyle(Func<BlockStyle, BlockStyle> configureStyle);
}
