namespace InvoiceKit.Sdk;

using Domain.Client;
using Domain.Company;
using Domain.Invoice;
using Domain.Shared.Kernel;
using Pdf;

public sealed class InvoiceBuilder : IInvoiceBuilder
{
    private InvoiceNumber _invoiceNumber = InvoiceNumber.CreateNew("0000");

    private Company? _company;

    private Client? _client;

    private IInvoiceTerms _terms = InvoiceStandardTerms.CreateNewDaysFromNow(CurrentSystemClock.Instance, 0);

    private IDocumentBuilder _documentBuilder = new DocumentBuilder();

    private readonly List<InvoiceLineItem> _lineItems = [];

    public IInvoiceBuilder WithInvoiceNumber(string invoiceNumber)
    {
        _invoiceNumber = InvoiceNumber.CreateNew(invoiceNumber);
        return this;
    }

    public IInvoiceBuilder WithCompany(string companyName)
    {
        var builder = new CompanyBuilder(companyName);
        _company = builder.Build();
        return this;
    }

    public IInvoiceBuilder WithCompany(string companyName, Action<ICompanyBuilder> configureCompany)
    {
        var builder = new CompanyBuilder(companyName);
        configureCompany(builder);
        _company = builder.Build();
        return this;
    }

    public IInvoiceBuilder WithClient(string clientName)
    {
        var builder = new ClientBuilder(clientName);
        _client = builder.Build();
        return this;
    }

    public IInvoiceBuilder WithClient(string clientName, Action<IClientBuilder> configureClient)
    {
        var builder = new ClientBuilder(clientName);
        configureClient(builder);
        _client = builder.Build();
        return this;
    }

    public IInvoiceBuilder WithItem(Action<IInvoiceLineItemBuilder> configureLineItem)
    {
        var builder = new InvoiceLineItemBuilder();
        configureLineItem(builder);
        _lineItems.Add(builder.Build());
        return this;
    }

    public IInvoiceBuilder WithItems<TItem>(IEnumerable<TItem> items,
        Action<IInvoiceLineItemBuilder, TItem> configureLineItem)
    {
        foreach (var item in items)
        {
            var builder = new InvoiceLineItemBuilder();
            configureLineItem(builder, item);
            _lineItems.Add(builder.Build());
        }

        return this;
    }

    public IInvoiceBuilder WithStandardTerms(int days)
    {
        _terms = InvoiceStandardTerms.CreateNewDaysFromNow(CurrentSystemClock.Instance, days);
        return this;
    }

    public IInvoiceBuilder WithDocumentBuilder(IDocumentBuilder documentBuilder)
    {
        _documentBuilder = documentBuilder;
        return this;
    }

    public IInvoiceBuilder WithDueDate(DateOnly dueDate)
    {
        _terms = InvoiceStandardTerms.CreateNewFromDateOnly(dueDate);
        return this;
    }

    public IInvoiceBuilder WithCustomTerms(IInvoiceTerms terms)
    {
        _terms = terms;
        return this;
    }

    public IPdfDocument Build()
    {
        if (_company is null)
        {
            throw new Exception("Company must be set.");
        }

        if (_client is null)
        {
            return _documentBuilder.Build(
                _company.CreateInvoiceWithoutClient(InvoiceDueDate.CreateNew(_terms), _invoiceNumber, _lineItems));
        }

        return _documentBuilder.Build(
            _company.CreateInvoiceForClient(_client, InvoiceDueDate.CreateNew(_terms), _invoiceNumber, _lineItems));
    }
}
