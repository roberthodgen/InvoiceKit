namespace InvoiceKit.Sdk;

using Domain.Invoice;
using Pdf;

public sealed class DocumentBuilder : IDocumentBuilder
{
    public IPdfDocument Build(Invoice invoice)
    {
        return PdfDocument.UsLetter;
    }

    public IDocumentBuilder WithDocumentSize()
    {
        throw new NotImplementedException();
    }
}
