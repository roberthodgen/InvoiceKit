namespace InvoiceKit.Pdf.Views;

using Layouts;
using Styles;

internal class TextViewBuilder(string text, BlockStyle style) : IViewBuilder
{
    public ILayout ToLayout()
    {
        return new TextLayout(style, text);
    }
}
