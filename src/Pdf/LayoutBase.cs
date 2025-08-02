namespace InvoiceKit.Pdf;

using Elements;
using Elements.Images;
using Layouts;
using Layouts.Stacks;
using Layouts.Tables;
using SkiaSharp;
using Styles.Text;

public abstract class LayoutBase : ILayout
{
    protected List<IDrawable> Children { get; } = [];

    public TextStyle DefaultTextStyle { get; protected set; }

    protected LayoutBase(TextStyle defaultTextStyle)
    {
        DefaultTextStyle = defaultTextStyle;
    }

    public abstract SKSize Measure(SKSize available);

    public abstract void Draw(PageLayout page, SKRect rect, Func<PageLayout> getNextPage);

    public ILayout AddText(Func<TextBuilder, IDrawable> builder)
    {
        var child = builder(new TextBuilder(DefaultTextStyle));
        Children.Add(child);
        return this;
    }

    public ILayout AddImage(Func<ImageBuilder, IDrawable> builder)
    {
        var child = builder(new ImageBuilder());
        Children.Add(child);
        return this;
    }

    public ILayout AddHorizontalRule()
    {
        var child = new HorizontalRule();
        Children.Add(child);
        return this;
    }

    public ILayout AddHStack(Action<HStack> configure)
    {
        var child = new HStack(DefaultTextStyle);
        configure(child);
        Children.Add(child);
        return this;
    }

    public ILayout AddVStack(Action<VStack> configure)
    {
        var child = new VStack(DefaultTextStyle);
        configure(child);
        Children.Add(child);
        return this;
    }

    public ILayout AddSpacingBlock(float height = 5)
    {
        var child = new SpacingBlock(height);
        Children.Add(child);
        return this;
    }

    public ILayout AddTableBlock(Action<TableLayoutBuilder> configure)
    {
        var child = new TableLayoutBuilder(DefaultTextStyle);
        configure(child);
        Children.Add(child);
        return this;
    }

    public void Dispose()
    {
        foreach (var child in Children)
        {
            child.Dispose();
        }
    }
}
