namespace InvoiceKit.Pdf.Elements.Text;

using Containers;
using Styles.Text;

public class TextView : IViewBuilder
{
    private readonly TextStyle _style;

    public string Text { get; }

    internal TextView(TextStyle style, string text)
    {
        _style = style;
        Text = text;
    }

    public ILayout ToLayout(PageLayout page)
    {
        return new TextLayout(_style, Text);
    }
}
