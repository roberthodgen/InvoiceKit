namespace InvoiceKit.Pdf.Elements.Text;

using SkiaSharp;
using Styles.Text;

public sealed class TextViewBuilder : IViewBuilder
{
    private TextStyle Style { get; set; }

    private string Text { get; set; } = "";

    public IReadOnlyCollection<IViewBuilder> Children => [];

    internal TextViewBuilder(TextStyle style)
    {
        Style = style;
    }

    public IViewBuilder WithText(string text)
    {
        Text = text;
        return this;
    }

    public TextViewBuilder ParagraphSpacing(float? before = null, float? after = null)
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

    public TextViewBuilder LineHeight(float lineHeight)
    {
        Style = Style with { LineHeight = lineHeight, };
        return this;
    }

    /// <summary>
    /// Sets the font and style to use.
    /// </summary>
    /// <param name="path">Should be specified as <c>Font Name/Style</c>, e.g.: <c>Open Sans/SemiBold</c>.</param>
    public TextViewBuilder Font(string path)
    {
        Style = Style with { FontPath = path, };
        return this;
    }

    public TextViewBuilder FontSize(float size)
    {
        Style = Style with { FontSize = size, };
        return this;
    }

    public TextViewBuilder Color(SKColor color)
    {
        Style = Style with { Color = color, };
        return this;
    }

    public ILayout ToLayout()
    {
        return new TextLayout(Style, Text);
    }

}
