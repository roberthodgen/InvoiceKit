namespace InvoiceKit.Sdk;

using Pdf;

public interface IInvoiceBuilder
{
    /// <summary>
    /// Sets the invoice number.
    /// </summary>
    IInvoiceBuilder WithNumber(string invoiceNumber);

    /// <summary>
    /// Configures your company information, i.e.: the issuer of the invoice.
    /// </summary>
    IInvoiceBuilder WithCompany(string companyName, Action<IInvoiceCompanyBuilder> configureCompany);

    /// <summary>
    /// Configures a customer, i.e.: the person who receives the invoice.
    /// </summary>
    IInvoiceBuilder WithCustomer(string customerName, Action<IInvoiceCompanyBuilder> configureClient);

    /// <summary>
    /// Adds one line item to the invoice.
    /// </summary>
    IInvoiceBuilder WithItem(Action<IInvoiceLineItemBuilder> configureLineItem);

    /// <summary>
    /// Adds multiple invoice items to the invoice.
    /// </summary>
    /// <remarks>
    /// May be called multiple times to add multiple items.
    /// </remarks>
    IInvoiceBuilder WithItems<TItem>(
        IEnumerable<TItem> items,
        Action<IInvoiceLineItemBuilder, TItem> configureLineItem);

    /// <summary>
    /// Sets the invoice terms.
    /// </summary>
    /// <remarks>
    /// Uses today's date to calculate the due date based on the terms. Also see <see cref="WithDueDate"/> to explicitly
    /// set the due date.
    /// </remarks>
    IInvoiceBuilder WithStandardTerms(int days);

    /// <summary>
    /// Sets the invoice's due date.
    /// </summary>
    /// <remarks>
    /// Explicitly set the due date. Also see <see cref="WithStandardTerms"/> to automatically calculate the due date.
    /// </remarks>
    IInvoiceBuilder WithDueDate();

    /// <summary>
    /// Builds a PDF invoice which may be saved.
    /// </summary>
    PdfDocument Build();
}
