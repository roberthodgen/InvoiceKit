namespace InvoiceKit.Pdf;

public interface IPdfDocument : IDisposable
{
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    byte[] Build();
}
