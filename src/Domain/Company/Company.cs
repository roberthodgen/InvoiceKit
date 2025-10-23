namespace InvoiceKit.Domain.Company;

using System.Text;
using Client;
using Invoice;

public sealed class Company
{
    public CompanyName Name { get; }

    public CompanyContactName? ContactName { get; private set; }

    public CompanyEmail? Email { get; private set; }

    public CompanyPhone? Phone { get; private set; }

    public CompanyAddress? Address { get; private set; }

    private Company(CompanyName name)
    {
        Name = name;
    }

    public static Company CreateNew(CompanyName name)
    {
        return new Company(name);
    }

    public Invoice CreateInvoiceForClient(
        Client client,
        InvoiceDueDate dueDate,
        InvoiceNumber invoiceNumber,
        List<InvoiceLineItem>? items)
    {
        return Invoice.CreateNewForClient(invoiceNumber, dueDate, client, this, items);
    }

    public Invoice CreateInvoiceWithoutClient(InvoiceDueDate dueDate, InvoiceNumber invoiceNumber,
        List<InvoiceLineItem>? items)
    {
        return Invoice.CreateNewWithoutClient(invoiceNumber, dueDate, this, items);
    }

    public void SetContactName(CompanyContactName contactName)
    {
        ContactName = contactName;
    }

    public void SetEmail(CompanyEmail email)
    {
        Email = email;
    }

    public void SetPhone(CompanyPhone phone)
    {
        Phone = phone;
    }

    public void SetAddress(CompanyAddress address)
    {
        Address = address;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append(Name);
        if (ContactName is not null)
        {
            builder.Append("\n" + ContactName);
        }

        if (Email is not null)
        {
            builder.Append("\n" + Email);
        }

        if (Phone is not null)
        {
            builder.Append("\n" + Phone);
        }

        if (Address is not null)
        {
            builder.Append("\n" + Address);
        }

        return builder.ToString();
    }
}
