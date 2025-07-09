namespace InvoiceKit.Sdk;

using Domain;
using Pdf;

public sealed class InvoiceBuilder
{
    public InvoiceBuilder()
    {
    }

    public InvoiceBuilder WithCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public InvoiceBuilder AddItem(InvoiceLineItem item)
    {
        throw new NotImplementedException();
    }

    public InvoiceBuilder WithDueDate(InvoiceDueDate dueDate)
    {
        throw new NotImplementedException();
    }

    public PdfDocument Build()
    {
        throw new NotFiniteNumberException();
    }
}
