namespace InvoiceKit.Domain.Invoice;

using Client;
using Company;

public sealed class Invoice
{
    public InvoiceNumber InvoiceNumber { get; }

    public InvoiceTotal Total => InvoiceTotal.SumInvoiceLineItems(Items);

    public InvoiceDueDate DueDate { get; }

    public Company Company { get; }

    public Client? Client { get; }

    private readonly List<InvoiceLineItem> _items;

    public ICollection<InvoiceLineItem> Items => _items.AsReadOnly();

    private Invoice(InvoiceNumber invoiceNumber, InvoiceDueDate dueDate, Client? client, Company company, List<InvoiceLineItem>? items)
    {
        DueDate = dueDate;
        InvoiceNumber = invoiceNumber;
        Client = client;
        Company = company;
        _items = items ?? [];
    }

    internal static Invoice CreateNew(
        InvoiceNumber invoiceNumber,
        InvoiceDueDate dueDate,
        Client client,
        Company company,
        List<InvoiceLineItem>? items)
    {
        return new Invoice(invoiceNumber, dueDate, client, company, items);
    }

    internal static Invoice CreateNewWithoutClient(InvoiceNumber invoiceNumber, InvoiceDueDate dueDate, Company company, List<InvoiceLineItem>? items)
    {
        return new Invoice(invoiceNumber, dueDate, null, company, items);
    }

    public void AddLineItem(InvoiceLineItem lineItem)
    {
        _items.Add(lineItem);
    }
}
