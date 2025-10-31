namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;
using Svg.Skia;

internal class ImageLayout : ILayout
{
    private SKSvg? Svg { get; }

    private SKBitmap? Bitmap { get; }

    internal ImageLayout(string path, ImageType type)
    {
        if (type == ImageType.Svg)
        {
            Svg = new SKSvg();
            Svg.Load(path);
        }
        else if (type == ImageType.Bmp)
        {
            using var data = SKData.Create(path);
            using var codec = SKCodec.Create(data);
            Bitmap = SKBitmap.Decode(codec);
        }
        else
        {
            throw new ArgumentException("Unsupported image type", nameof(type));
        }
    }

    public SKSize Measure(SKSize available)
    {
        if (Svg?.Drawable is not null)
        {
            return new SKSize(Svg.Drawable.Bounds.Width, Svg.Drawable.Bounds.Height);
        }

        if (Bitmap is not null)
        {
            return new SKSize(Bitmap.Width, Bitmap.Height);
        }

        throw new Exception("Image not loaded.");
    }

    public LayoutResult Layout(LayoutContext context)
    {
        var listDrawables = new List<IDrawable>();
        if (context.TryAllocate(this, context.Available.Size, out var rect))
        {
            if (Svg is not null)
            {
                listDrawables.Add(new SvgImageDrawable(Svg, rect));
            }
            else if (Bitmap is not null)
            {
                listDrawables.Add(new BitmapImageDrawable(Bitmap, rect));
            }

            return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
        }

        return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
    }
}
