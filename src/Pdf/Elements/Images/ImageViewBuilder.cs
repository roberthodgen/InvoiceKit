namespace InvoiceKit.Pdf.Elements.Images;

public sealed class ImageViewBuilder : IViewBuilder
{
    private string Path { get; set; } = "";

    private string ImageType { get; set; } = "";

    public IReadOnlyCollection<IViewBuilder> Children => [];

    internal ImageViewBuilder()
    {
    }

    public IViewBuilder WithBitmapImage(string path)
    {
        Path = path;
        ImageType = "bmp";
        return this;
    }

    public IViewBuilder WithSvgImage(string path)
    {
        Path = path;
        ImageType = "svg";
        return this;
    }

    public ILayout ToLayout()
    {
        return new ImageLayout(Path, ImageType);
    }
}
