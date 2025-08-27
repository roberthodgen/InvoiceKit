namespace InvoiceKit.Pdf.Elements.Images;

using Containers;

public sealed class ImageViewBuilder : IViewBuilder
{
    private IDrawable? _drawable;

    internal ImageViewBuilder()
    {
    }

    public IViewBuilder WithBitmapImage(string path)
    {
        _drawable = new BitmapImageDrawable(path);
        return this;
    }

    public IViewBuilder WithSvgImage(string path)
    {
        _drawable = new SvgImageDrawable(path);
        return this;
    }

    public ILayout ToLayout(PageLayout page)
    {
        throw new NotImplementedException();
    }
}
