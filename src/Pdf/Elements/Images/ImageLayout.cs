namespace InvoiceKit.Pdf.Elements.Images;

using SkiaSharp;
using Svg.Skia;

public class ImageLayout : ILayout
{
    private SKSvg? Svg { get; }

    private SKBitmap? Bitmap { get; }

    internal ImageLayout(string path, string imageType)
    {
        switch (imageType)
        {
            case "svg":
                Svg = new SKSvg();
                Svg.Load(path);
                break;
            case "bmp":
            {
                using var data = SKData.Create(path);
                using var codec = SKCodec.Create(data);
                Bitmap = SKBitmap.Decode(codec);
                break;
            }
            default:
                throw new ArgumentException("Image type not supported.");
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

    public void LayoutPages(MultiPageContext context, bool debug)
    {
        var page = context.GetCurrentPage();
        while (true)
        {
            var size = Measure(page.Available.Size);
            var rect = new SKRect(
                page.Available.Left,
                page.Available.Top,
                page.Available.Left + size.Width,
                page.Available.Top + size.Height);

            if (Svg?.Drawable != null)
            {
                if (page.TryAllocateRect(rect))
                {
                    page.AddDrawable(new SvgImageDrawable(Svg, rect, debug));
                    break;
                }
            }

            if (Bitmap != null)
            {
                if (page.TryAllocateRect(rect))
                {
                    page.AddDrawable(new BitmapImageDrawable(Bitmap, rect, debug));
                    break;
                }
            }

            // Will only be hit if the page is full.
            page.MarkFullyDrawn();
            page = context.GetCurrentPage();
        }
    }
}
