namespace InvoiceKit.Pdf.Elements.Images;

using Containers;
using SkiaSharp;

public sealed class ImageViewBuilder : IViewBuilder
{
    private IDrawable? _drawable;

    internal ImageViewBuilder()
    {
    }

    public IViewBuilder WithBitmapImage(string path)
    {
        // Todo: Fill in correct SKRect
        _drawable = new BitmapImageDrawable(path, new SKRect());
        return this;
    }

    public IViewBuilder WithSvgImage(string path)
    {
        // Todo: Fill in correct SKRect
        _drawable = new SvgImageDrawable(path, new SKRect());
        return this;
    }

    public ILayout ToLayout()
    {
        throw new NotImplementedException();
    }
}
