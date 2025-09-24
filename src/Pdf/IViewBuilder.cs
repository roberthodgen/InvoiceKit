namespace InvoiceKit.Pdf;

public interface IViewBuilder
{
    ILayout ToLayout();

    IReadOnlyCollection<IViewBuilder> Children { get; }
}
