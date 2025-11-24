namespace InvoiceKit.Pdf.Views;

using Layouts;
using Styles;

/// <summary>
/// Adds an image to the document.
/// </summary>
public sealed class ImageViewBuilder(BlockStyle style) : IViewBuilder
{
    private string Path { get; set; } = "";

    private ImageType Type { get; set; }

    public IViewBuilder WithBitmapImage(string path)
    {
        Path = path;
        Type = ImageType.Bmp;
        return this;
    }

    public IViewBuilder WithSvgImage(string path)
    {
        Path = path;
        Type = ImageType.Svg;
        return this;
    }

    public ILayout ToLayout()
    {
        return new ImageLayout(Path, Type, style);
    }
}
