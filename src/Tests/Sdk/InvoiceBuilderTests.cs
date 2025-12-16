namespace InvoiceKit.Tests.Sdk;

using Domain.Invoice;
using Domain.Shared.Kernel;
using InvoiceKit.Sdk;
using Pdf;

public sealed class InvoiceBuilderTests
{
    [Fact]
    public void InvoiceBuilder_WithCompany_SetsCompany()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany").WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Company.Name.ToString().ShouldBe("TestCompany");
    }

    [Fact]
    public void InvoiceBuilder_SkipWithCompany_ThrowsValidationException()
    {
        var exception = Should.Throw<Exception>(() => new InvoiceBuilder().Build());
        exception.Message.ShouldContain("company");
    }

    [Fact]
    public void InvoiceBuilder_WithCompany_CanSetAddress()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany", builder => builder.WithAddress("123 street", "apartment", "city", "state", "zip", "country")).WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Company.Address!.Address1.ShouldBe("123 street");
        documentBuilder.Invoice.Company.Address!.Address2.ShouldBe("apartment");
        documentBuilder.Invoice.Company.Address!.City.ShouldBe("city");
        documentBuilder.Invoice.Company.Address!.State.ShouldBe("state");
        documentBuilder.Invoice.Company.Address!.ZipCode.ShouldBe("zip");
        documentBuilder.Invoice.Company.Address!.Country.ShouldBe("country");
    }

    [Fact]
    public void InvoiceBuilder_WithCompany_CanSetContactName()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany", builder => builder.WithContactName("Contact Name")).WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Company.ContactName!.ToString().ShouldBe("Contact Name");
    }

    [Fact]
    public void InvoiceBuilder_WithCompany_CanSetPhone()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany", builder => builder.WithPhone("123-456-7890")).WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Company.Phone!.ToString().ShouldBe("123-456-7890");
    }

    [Fact]
    public void InvoiceBuilder_WithCompany_CanSetEmail()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany", builder => builder.WithEmail("finance@company.com")).WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Company.Email!.ToString().ShouldBe("finance@company.com");
    }

    [Fact]
    public void InvoiceBuilder_WithClient_SetsClient()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany").WithClient("TestClient").WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Client!.Name.ToString().ShouldBe("TestClient");
    }

    [Fact]
    public void InvoiceBuilder_SkipWithClient_CreatesInvoiceWithoutClient()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany").WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Client!.ShouldBeNull();
    }

    [Fact]
    public void InvoiceBuilder_WithClient_CanSetAddress()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany").WithClient("TestClient", builder => builder.WithAddress("123 street", "apartment", "city", "state", "zip", "country")).WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Client!.Address!.Address1.ShouldBe("123 street");
        documentBuilder.Invoice.Client!.Address.Address2.ShouldBe("apartment");
        documentBuilder.Invoice.Client!.Address.City.ShouldBe("city");
        documentBuilder.Invoice.Client!.Address.State.ShouldBe("state");
        documentBuilder.Invoice.Client!.Address.ZipCode.ShouldBe("zip");
        documentBuilder.Invoice.Client!.Address.Country.ShouldBe("country");
    }

    [Fact]
    public void InvoiceBuilder_WithClient_CanSetContactName()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany").WithClient("TestClient", builder => builder.WithContactName("Contact Name")).WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Client!.ContactName!.ToString().ShouldBe("Contact Name");
    }

    [Fact]
    public void InvoiceBuilder_WithClient_CanSetPhone()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany").WithClient("TestClient", builder => builder.WithPhone("123-456-7890")).WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Client!.Phone!.ToString().ShouldBe("123-456-7890");
    }

    [Fact]
    public void InvoiceBuilder_WithClient_CanSetEmail()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany").WithClient("TestClient", builder => builder.WithEmail("finance@client.com")).WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Client!.Email!.ToString().ShouldBe("finance@client.com");
    }

    [Fact]
    public void InvoiceBuilder_WithInvoiceNumber_SetsInvoiceNumber()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany").WithInvoiceNumber("121212").WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.InvoiceNumber.ToString().ShouldBe("121212");
    }

    [Fact]
    public void InvoiceBuilder_SkipWiths_SetsDefaultValues()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany").WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.InvoiceNumber.ToString().ShouldBe("0000");
        documentBuilder.Invoice.Items.Count.ShouldBe(0);
        documentBuilder.Invoice.Total.Amount.ShouldBe(0);
        documentBuilder.Invoice.Client.ShouldBeNull();
        documentBuilder.Invoice.DueDate.ShouldBe(InvoiceDueDate.CreateNew(InvoiceStandardTerms.CreateNewDaysFromNow(CurrentSystemClock.Instance, 0)));
    }

    [Fact]
    public void InvoiceBuilder_WithItem_SetsItem()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany")
            .WithItem(item => item.WithPerUnitPrice(10m).WithQuantity(2)).WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Items.Count.ShouldBe(1);
        var item = documentBuilder.Invoice.Items.First();
        item.Description!.ToString().ShouldBe("Item");
        item.PerUnitPrice.Amount.ShouldBe(10m);
        item.Quantity.Value.ShouldBe(2);
        item.Subtotal.Amount.ShouldBe(20m);
    }

    [Fact]
    public void InvoiceBuilder_SkipWithPerUnitPrice_ThrowsException()
    {
        var exception = Should.Throw<Exception>(() => new InvoiceBuilder().WithCompany("TestCompany").WithItem(item => item.WithDescription("Exception")));
        exception.Message.ShouldContain("PerUnitPrice");
    }

    [Fact]
    public void InvoiceBuilder_WithItems_SetsItems()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        List<TestItem> items = [new ("Item1", 10m), new ("Item2", 5m)];
        //Act
        new InvoiceBuilder()
            .WithCompany("TestCompany")
            .WithItems(items, (builder, item) => builder.WithDescription(item.Name).WithPerUnitPrice(item.Total))
            .WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.Items.Count.ShouldBe(items.Count);
        var item1 = documentBuilder.Invoice.Items.First(item => item.Description!.Value == "Item1");
        item1.PerUnitPrice.Amount.ShouldBe(10m);
        var item2 = documentBuilder.Invoice.Items.First(item => item.Description!.Value == "Item2");
        item2.PerUnitPrice.Amount.ShouldBe(5m);
    }

    [Fact(Skip = "Bad test after midnight UTC")]
    public void InvoiceBuilder_WithStandardTerms_SetsStandardTerms()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany").WithStandardTerms(10).WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.DueDate.Value.ShouldBe(DateTime.Today.AddDays(10));
    }

    [Fact]
    public void InvoiceBuilder_WithDueDate_SetsDueDate()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        //Act
        new InvoiceBuilder().WithCompany("TestCompany").WithDueDate(DateOnly.FromDateTime(DateTime.UtcNow.Date)).WithDocumentBuilder(documentBuilder).Build();
        //Assert
        documentBuilder.Invoice.DueDate.Value.ShouldBe(DateTime.UtcNow.Date);
    }

    [Fact]
    public void InvoiceBuilder_WithCustomTerms_SetsCustomTerms()
    {
        //Arrange
        var documentBuilder = new TestDocumentBuilder();
        var systemClock = CurrentSystemClock.Instance.Now.Date;
        var testTerms = new TestTerms(systemClock);
        //Assert
        new InvoiceBuilder()
            .WithCompany("TestCompany")
            .WithCustomTerms(testTerms)
            .WithDocumentBuilder(documentBuilder).Build();
        //Act
        documentBuilder.Invoice.DueDate.Value.ShouldBe(systemClock.Date);
    }

    private sealed record TestItem(string Name, decimal Total);

    private sealed class TestTerms(DateTime dueDate) : IInvoiceTerms
    {
        public DateTime GetDueDate { get; } = dueDate;
    }

    private sealed class TestDocumentBuilder : IDocumentBuilder
    {
        public Invoice Invoice => _invoice ?? throw new ApplicationException("Invoice not set");

        private Invoice? _invoice;

        public IPdfDocument Build(Invoice invoice)
        {
            _invoice = invoice;
            return PdfDocument.UsLetter;
        }

        public IDocumentBuilder WithDocumentSize()
        {
            throw new NotImplementedException();
        }
    }
}
