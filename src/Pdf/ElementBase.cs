namespace InvoiceKit.Pdf;

using Elements;
using Elements.Images;
using Layouts.Stacks;
using Layouts.Tables;
using SkiaSharp;
using Styles.Text;

public abstract class ElementBase : IElement, IDrawable
{
    private readonly TextStyle _defaultTextStyle;

    protected IDrawable? _drawable;

    protected ElementBase(TextStyle defaultTextStyle)
    {
        _defaultTextStyle = defaultTextStyle;
    }

    public void WithHStack(Action<HStack> action)
    {
        var hStack = new HStack(_defaultTextStyle);
        action(hStack);
        _drawable = hStack;
    }

    public void WithVStack(Action<VStack> action)
    {
        var vStack = new VStack(_defaultTextStyle);
        action(vStack);
        _drawable = vStack;
    }

    public void WithTable(Action<TableLayoutBuilder> action)
    {
        var table = new TableLayoutBuilder(_defaultTextStyle);
        action(table);
        _drawable = table;
    }

    public void WithText(Func<TextBuilder, IDrawable> builder)
    {
        _drawable = builder(new TextBuilder(_defaultTextStyle));
    }

    public void WithImage(Func<ImageBuilder, IDrawable> builder)
    {
        _drawable = builder(new ImageBuilder());
    }

    public SKSize Measure(SKSize available)
    {
        return _drawable?.Measure(available) ?? SKSize.Empty;
    }

    public void Draw(MultiPageContext context, SKRect rect)
    {
        _drawable?.Draw(context, rect);
    }

    public void Dispose()
    {
        _drawable?.Dispose();
    }
}
