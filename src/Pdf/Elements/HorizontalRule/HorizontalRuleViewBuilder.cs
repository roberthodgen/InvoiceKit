namespace InvoiceKit.Pdf.Elements.HorizontalRule;

using Layout;

public sealed class HorizontalRuleViewBuilder : IViewBuilder
{
    public IReadOnlyCollection<IViewBuilder> Children => [];

    internal HorizontalRuleViewBuilder()
    {
    }

    public ILayout ToLayout()
    {
        return new HorizontalRuleLayout();
    }
}
