namespace InvoiceKit.Pdf.Elements.Images;

using SkiaSharp;

public sealed class ImageBuilder
{
    internal ImageBuilder()
    {
    }

    public IDrawable WithBitmapImage(string path) => new BitmapImage(path);

    public IDrawable WithSvgImage(string path) => new SvgImage(path);
}
