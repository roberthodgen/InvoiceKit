namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;
using Svg.Skia;

internal class ImageLayout : ILayout
{
    private bool _drawn;

    private SKSvg? Svg { get; }

    private SKBitmap? Bitmap { get; }

    public IReadOnlyCollection<ILayout> Children => [];

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
        if (Svg?.Drawable != null)
        {
            return new SKSize(Svg.Drawable.Bounds.Width, Svg.Drawable.Bounds.Height);
        }

        if (Bitmap != null)
        {
            return new SKSize(Bitmap.Width, Bitmap.Height);
        }

        throw new Exception("Image not loaded.");
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (_drawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var listDrawables = new List<IDrawable>();
        var size = Measure(context.Available.Size);

        var rect = new SKRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Left + size.Width,
            context.Available.Top + size.Height);

        if (Svg?.Drawable != null)
        {
            if (context.TryAllocateRect(rect))
            {
                listDrawables.Add(new SvgImageDrawable(Svg, rect));
                _drawn = true;
                return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
            }
        }

        if (Bitmap != null)
        {
            if (context.TryAllocateRect(rect))
            {
                listDrawables.Add(new BitmapImageDrawable(Bitmap, rect));
                _drawn = true;
                return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
            }
        }

        // Will only be hit if the page is full.
        return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
    }
}
