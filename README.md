# InvoiceKit

**InvoiceKit** is a modern, open source **.NET SDK** for generating professional PDF invoices with support for **custom themes**, **invoice line items**, **totals**, **due date calculations**, and more.

> 🚧 **This project is in early development**. Expect breaking changes, limited functionality, and rapid iteration as the core features are built out.

## ✨ Features

- 📄 **PDF Export** – Generate professional PDF invoices programmatically.
- 🎨 **Theme Support** – Easily swap between themes (classic, modern, contemporary) or define your own.
- ⚙️ **SDK for .NET** – Clean, fluent API designed for integration into any C# or .NET application.
- 🧾 **Invoice Structure** – Supports line items, subtotal/tax/total calculations, due dates, and notes.

## 🔧 Example Usage

```cs
var invoice = new InvoiceBuilder()
    .WithCustomer("Acme Inc.")
    .AddItem("Widget", 2, 29.99m)
    .WithDueDate(DateTime.UtcNow.AddDays(30))
    .Build();

invoice.SaveAsPdf("invoice.pdf");
```

## 🚀 Roadmap

- [ ] Define core domain models (Invoice, LineItem, Customer, Theme)
- [ ] Unit tests
- [ ] Build PDF rendering engine (initial theme)
- [ ] Publish SDK on NuGet
- [ ] Add multiple themes
   - [ ] Contemporary
   - [ ] Modern
   - [ ] Classic
- [ ] Documentation

## 📝 License and OSS

MIT Licenseed.

> 💡 **Open Source Commitment**
> InvoiceKit is and always will be open source. This project is built with the developer community in mind. That includes a committment to maintaining InvoiceKit as a free and open source software.
