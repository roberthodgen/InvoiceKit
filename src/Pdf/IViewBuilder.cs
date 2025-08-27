namespace InvoiceKit.Pdf;

using Containers;

public interface IViewBuilder
{
    ILayout ToLayout(PageLayout page);
}
