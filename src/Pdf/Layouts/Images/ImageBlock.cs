namespace InvoiceKit.Pdf.Layouts.Images;

using SkiaSharp;
using Svg.Skia;

/// <summary>
/// Renders an image as a block element.
/// </summary>
public class ImageBlock : IDrawable, IDisposable
{
    public float Width { get; }
    
    public float Height { get; }
    
    private readonly SKBitmap? _bitmap;
    private readonly SKSvg? _svg;

    public ImageBlock(float width, float height, SKBitmap bitmap)
    {
        _bitmap = bitmap;
        Width = width;
        Height = height;
    }

    public ImageBlock(float width, float height, SKSvg svg)
    {
        _svg = svg;
        Width = width;
        Height = height;
    }
    
    public static ImageBlock CreateBitmap(string path) {
        using var data = SKData.Create(path);
        using var codec = SKCodec.Create(data);
        var bitmap = SKBitmap.Decode(codec);
        return new (bitmap.Width, bitmap.Height, bitmap);
    }

    public static ImageBlock CreateSvg(string path)
    {
        var svg = new SKSvg();
        svg.Load(path);
        var size = svg.Drawable?.Bounds.Size ?? throw new ApplicationException("SVG bounds not set");
        return new(size.Width, size.Height, svg);
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(Width, Height);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        if (_bitmap is not null)
        {
            page.Canvas.DrawBitmap(_bitmap, rect.Location);
        }

        if (_svg is not null)
        {
            page.Canvas.DrawPicture(_svg.Picture, rect.Location);
        }
    }

    public void Dispose()
    {
        _bitmap?.Dispose();
    }
}
