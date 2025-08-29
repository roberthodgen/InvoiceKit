namespace InvoiceKit.Pdf.Elements.Text;

using Containers;
using SkiaSharp;
using Styles.Text;

public sealed class TextDrawable : IDrawable
{
    private string TextLine { get; }

    public SKRect SizeAndLocation { get; }

    private TextStyle Style { get; }

    public TextDrawable(string text, SKRect sizeAndLocation, TextStyle style)
    {
        TextLine = text;
        SizeAndLocation = sizeAndLocation;
        Style = style;
    }

    public SKSize Measure(SKSize available)
    {
        // Todo: Measure text size. Already implemented somewhere else.
        return new SKSize();
    }

    public void Dispose()
    {
    }

    public void Draw(PageLayout page)
    {
        page.Canvas.DrawText(TextLine, SizeAndLocation.Left, SizeAndLocation.Top, SKTextAlign.Left, Style.ToFont(), Style.ToPaint());
    }
}
