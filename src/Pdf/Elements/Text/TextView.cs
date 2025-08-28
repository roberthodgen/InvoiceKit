namespace InvoiceKit.Pdf.Elements.Text;

using Containers;
using Styles.Text;

public class TextView : IViewBuilder
{
    private readonly TextStyle _style;

    private string Text { get; }

    internal TextView(TextStyle style, string text)
    {
        _style = style;
        Text = text;
    }

    public ILayout ToLayout(MultiPageContext context)
    {
        return new TextLayout(_style, Text);
    }
}
