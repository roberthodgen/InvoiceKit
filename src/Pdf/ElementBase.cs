namespace InvoiceKit.Pdf;

using Elements.Text;
using Elements.Images;
using Layouts.Stacks;
using Layouts.Tables;
using SkiaSharp;
using Styles.Text;

public abstract class ElementBase : IElement
{
    private readonly TextStyle _defaultTextStyle;

    private IDrawable? _drawable;

    private readonly SKRect _rect;

    protected ElementBase(TextStyle defaultTextStyle, SKRect rect)
    {
        _defaultTextStyle = defaultTextStyle;
        _rect = rect;
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

    public void WithText(Func<TextViewBuilder, IDrawable> builder)
    {
        _drawable = builder(new TextViewBuilder(_defaultTextStyle));
    }

    public void WithImage(Func<ImageBuilder, IDrawable> builder)
    {
        _drawable = builder(new ImageBuilder());
    }
}
