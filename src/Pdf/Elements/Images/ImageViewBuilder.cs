namespace InvoiceKit.Pdf.Elements.Images;

using Containers;
using SkiaSharp;

public sealed class ImageViewBuilder : IViewBuilder
{
    private string Path { get; set; } = "";

    private string ImageType { get; set; } = "";

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
