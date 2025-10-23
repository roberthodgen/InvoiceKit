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
        builder.AppendLine(Name.ToString());
        if (ContactName is not null)
        {
            builder.AppendLine(ContactName.ToString());
        }

        if (Email is not null)
        {
            builder.AppendLine(Email.ToString());
        }

        if (Phone is not null)
        {
            builder.AppendLine(Phone.ToString());
        }

        if (Address is not null)
        {
            builder.AppendLine(Address.ToString());
        }

        return builder.Remove(builder.Length - 1, 1).ToString();
    }
}
