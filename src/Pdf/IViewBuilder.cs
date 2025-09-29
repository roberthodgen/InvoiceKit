namespace InvoiceKit.Pdf;

using Layout;

public interface IViewBuilder
{
    internal ILayout ToLayout();

    IReadOnlyCollection<IViewBuilder> Children { get; }
}
