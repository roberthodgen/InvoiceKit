namespace InvoiceKit.Pdf.Styles.Text;

using SkiaSharp;

public class TextOptionsBuilder
{
    private TextStyle _style;

    public TextOptionsBuilder(TextStyle baseStyle)
    {
        _style = baseStyle;
    }

    /// <summary>
    /// Sets the font and style to use.
    /// </summary>
    /// <param name="path">Should be specified as <c>Font Name/Style</c>, e.g.: <c>Open Sans/SemiBold</c>.</param>
    public TextOptionsBuilder Font(string path)
    {
        _style = _style with { FontPath = path, };
        return this;
    }

    public TextOptionsBuilder FontSize(float size)
    {
        _style = _style with { FontSize = size, };
        return this;
    }

    public TextOptionsBuilder Color(SKColor color)
    {
        _style = _style with { Color = color, };
        return this;
    }

    public TextStyle Build() => _style;
}
