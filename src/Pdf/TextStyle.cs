namespace InvoiceKit.Pdf;

using SkiaSharp;

public class TextStyle
{
    public string? FontPath { get; set; }

    public float FontSize { get; set; } = 16f;

    public SKColor Color { get; set; } = SKColors.Black;

    public SKFontStyleWeight FontWeight { get; set; } = SKFontStyleWeight.Normal;

    public SKPaint ToPaint()
    {
        var paint = new SKPaint
        {
            Color = Color,
            IsAntialias = true
        };

        return paint;
    }

    public SKFont ToFont()
    {
        var font = new SKFont
        {
            Size = FontSize,
        };

        if (!string.IsNullOrWhiteSpace(FontPath) && File.Exists(FontPath))
        {
            font.Typeface = SKTypeface.FromFile(FontPath);
        }

        font.Typeface = SKTypeface.FromFamilyName(
            font.Typeface?.FamilyName ?? SKFontManager.Default.GetFamilyName(0),
            new SKFontStyle(SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright));

        return font;
    }
}
