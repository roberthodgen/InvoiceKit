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
        return new SKSize(SizeAndLocation.Width, SizeAndLocation.Height);
    }

    public void Dispose()
    {
    }

    public void Draw(PageLayout page)
    {
        var halfLineHeight = ((Style.LineHeight * Style.FontSize) - Style.FontSize) / 2;
        var top = SizeAndLocation.Top + halfLineHeight + Style.ToFont().Metrics.Ascent + Style.ParagraphSpacingBefore;
        page.Canvas.DrawText(TextLine, SizeAndLocation.Left, top, SKTextAlign.Left, Style.ToFont(), Style.ToPaint());
    }
}
