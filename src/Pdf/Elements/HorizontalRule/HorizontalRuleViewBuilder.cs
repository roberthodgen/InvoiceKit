namespace InvoiceKit.Pdf.Elements.HorizontalRule;

public class HorizontalRuleViewBuilder : IViewBuilder
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
