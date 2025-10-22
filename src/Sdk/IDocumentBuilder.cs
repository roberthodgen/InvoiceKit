namespace InvoiceKit.Sdk;

using Domain.Invoice;
using Pdf;

public interface IDocumentBuilder
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns></returns>
    IPdfDocument Build(Invoice invoice);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    // Example
    IDocumentBuilder WithDocumentSize();
}
