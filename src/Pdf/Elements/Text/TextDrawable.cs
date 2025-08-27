namespace InvoiceKit.Pdf.Elements.Text;

using Containers;
using SkiaSharp;

public sealed class TextDrawable : IDrawable
{
    private string TextLine { get; }

    public TextDrawable(string text)
    {
        TextLine = text;
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
        // Todo: Replace the new objects with ones from layout parent in the constructor.
        page.Canvas.DrawText(TextLine, new SKPoint(), new SKFont(), new SKPaint());
    }
}
