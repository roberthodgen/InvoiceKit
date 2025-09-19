namespace InvoiceKit.Pdf.Elements.HorizontalRule;

public class HorizontalRuleViewBuilder : IViewBuilder
{
    internal HorizontalRuleViewBuilder()
    {
    }

    public ILayout ToLayout()
    {
        return new HorizontalRuleLayout();
    }
}
