# InvoiceKit

**InvoiceKit** is a modern, open source **.NET SDK** for generating professional PDF invoices with support for **custom themes**, **invoice line items**, **totals**, **due date calculations**, and more.

> 🚧 **This project is in early development**.
>
> Expect breaking changes, limited functionality, and rapid iteration as the core features are built out.

## ✨ Features

- 📄 **PDF Export** – Generate professional PDF invoices programmatically.
- 🎨 **Theme Support** – Easily swap between themes (classic, modern, contemporary) or define your own.
- ⚙️ **SDK for .NET** – Clean, fluent API designed for integration into any C# or .NET application.
- 🧾 **Invoice Structure** – Supports line items, subtotal/tax/total calculations, due dates, and notes.

## 🔧 Example Usage

```cs
var invoice = new InvoiceBuilder()
    .WithCompany("IT Services LLC")
    .WithClient("ACME,  Inc.")
    .AddItem("Widget", item => item
        .WithQuantity(2)
        .WithPricePerUnit(29.99m))
    .WithStandardTerms(days: 30)
    .Build();

invoice.SaveAsPdf("invoice.pdf");
```

## 🚀 Roadmap to v1

- [x] Build PDF rendering engine
  - [x] Enhance PDF rendering blocks with fluent-API
  - [x] Enhance multi-page support
- [x] Define core domain models (Invoice, Line Items, Customer) w/ unit tests
- [x] Create SDK fluent API
- [ ] Documentation
- [ ] Publish SDK on NuGet

Nice to have:
- [ ] Figure out PDF unit test strategy
- [ ] Add theme support with built-in defaults:
   - [ ] Contemporary
   - [ ] Modern
   - [ ] Classic
- [ ] Custom unit formatters and localization

## 📝 License and OSS

[MIT License](LICENSE)

> 💡 **Open Source Commitment**
>
> InvoiceKit is and always will be open source. This project is built with the developer community in mind. That includes a committment to maintaining InvoiceKit as a free and open source software.
