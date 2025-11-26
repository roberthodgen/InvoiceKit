namespace InvoiceKit.Pdf.Views;

using Layouts;

internal class TextViewBuilder(string text, BlockStyle style) : IViewBuilder
{
    public ILayout ToLayout()
    {
        return new TextLayout(style, text);
    }
}
