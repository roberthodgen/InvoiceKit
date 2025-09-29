namespace InvoiceKit.Pdf.Elements.Images;

using Layout;

public sealed class ImageViewBuilder : IViewBuilder
{
    private string Path { get; set; } = "";

    private ImageType ImageType { get; set; }

    public IReadOnlyCollection<IViewBuilder> Children => [];

    internal ImageViewBuilder()
    {
    }

    public IViewBuilder WithBitmapImage(string path)
    {
        Path = path;
        ImageType = ImageType.Bmp;
        return this;
    }

    public IViewBuilder WithSvgImage(string path)
    {
        Path = path;
        ImageType = ImageType.Svg;
        return this;
    }

    public ILayout ToLayout()
    {
        return new ImageLayout(Path, ImageType);
    }
}
