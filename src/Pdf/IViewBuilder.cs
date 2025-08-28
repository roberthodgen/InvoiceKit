namespace InvoiceKit.Pdf;

public interface IViewBuilder
{
    ILayout ToLayout(MultiPageContext page);
}
