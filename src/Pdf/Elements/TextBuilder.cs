namespace InvoiceKit.Pdf.Elements;

using SkiaSharp;
using Styles.Text;

public class TextBuilder
{
    public TextStyle Style { get; private set; }

    internal TextBuilder(TextStyle style)
    {
        Style = style;
    }

    public IDrawable WithText(string text)
    {
        return new Text(Style, text);
    }

    public TextBuilder ParagraphSpacing(float? before = null, float? after = null)
    {
        if (before.HasValue)
        {
            Style = Style with { ParagraphSpacing = Style.ParagraphSpacing with { Before = before.Value, }, };
        }

        if (after.HasValue)
        {
            Style = Style with { ParagraphSpacing = Style.ParagraphSpacing with { After = after.Value, }, };
        }

        return this;
    }

    public TextBuilder LineHeight(float lineHeight)
    {
        Style = Style with { LineHeight = lineHeight, };
        return this;
    }



    /// <summary>
    /// Sets the font and style to use.
    /// </summary>
    /// <param name="path">Should be specified as <c>Font Name/Style</c>, e.g.: <c>Open Sans/SemiBold</c>.</param>
    public TextBuilder Font(string path)
    {
        Style = Style with { FontPath = path, };
        return this;
    }

    public TextBuilder FontSize(float size)
    {
        Style = Style with { FontSize = size, };
        return this;
    }

    public TextBuilder Color(SKColor color)
    {
        Style = Style with { Color = color, };
        return this;
    }
}
