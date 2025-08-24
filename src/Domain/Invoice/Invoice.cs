namespace InvoiceKit.Domain.Invoice;

using Client;
using Company;

public sealed class Invoice
{
    public InvoiceNumber InvoiceNumber { get; }

    public InvoiceTotal Total => InvoiceTotal.SumInvoiceLineItems(Items);

    public InvoiceDueDate DueDate { get; }

    public Company Company { get; }

    public Client Client { get; }

    private readonly List<InvoiceLineItem> _items;

    public ICollection<InvoiceLineItem> Items => _items.AsReadOnly();

    private Invoice(InvoiceNumber invoiceNumber, InvoiceDueDate dueDate, Client client, Company company)
    {
        DueDate = dueDate;
        InvoiceNumber = invoiceNumber;
        Client = client;
        Company = company;
        _items = [];
    }

    internal static Invoice CreateNew(
        InvoiceNumber invoiceNumber,
        InvoiceDueDate dueDate,
        Client client,
        Company company)
    {
        return new Invoice(invoiceNumber, dueDate, client, company);
    }

    public void AddLineItem(InvoiceLineItem lineItem)
    {
        _items.Add(lineItem);
    }
}
