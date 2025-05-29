namespace InvoiceKit.Pdf;

using SkiaSharp;

public class TextOptionsBuilder
{
    private readonly TextStyle _style = new();

    public TextOptionsBuilder Font(string path)
    {
        _style.FontPath = path;
        return this;
    }

    public TextOptionsBuilder FontSize(float size)
    {
        _style.FontSize = size;
        return this;
    }

    public TextOptionsBuilder Color(SKColor color)
    {
        _style.Color = color;
        return this;
    }

    public TextOptionsBuilder FontWeight(SKFontStyleWeight weight)
    {
        _style.FontWeight = weight;
        return this;
    }

    public TextStyle Build() => _style;
}
