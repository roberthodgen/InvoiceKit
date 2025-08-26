namespace InvoiceKit.Pdf.Elements.Text;

using Layouts;
using SkiaSharp;

public sealed class TextDrawable : IDrawable
{
    private string Text { get; }

    public TextDrawable(string text)
    {
        Text = text;
    }

    public SKSize Measure(SKSize available)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void Draw(PageLayout page)
    {
        // Todo: Replace the new objects with ones from layout parent in the constructor.
        page.Canvas.DrawText(Text, new SKPoint(), new SKFont(), new SKPaint());
    }
}
