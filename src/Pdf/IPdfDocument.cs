namespace InvoiceKit.Pdf;

public interface IPdfDocument : IDisposable
{
    /// <summary>
    /// Completes the build and returns a byte array for use in a file stream.
    /// </summary>
    /// <returns></returns>
    byte[] Build();
}
