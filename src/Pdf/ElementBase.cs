namespace InvoiceKit.Pdf;

using Elements.Text;
using Elements.Images;
using Containers.Stacks;
using Containers.Tables;
using Styles.Text;

public abstract class ElementBase : IElement
{
    private readonly TextStyle _defaultTextStyle;

    private IViewBuilder? _viewBuilder;

    protected ElementBase(TextStyle defaultTextStyle)
    {
        _defaultTextStyle = defaultTextStyle;
    }

    public void WithHStack(Action<HStack> action)
    {
        var hStack = new HStack(_defaultTextStyle);
        action(hStack);
        _viewBuilder = hStack;
    }

    public void WithVStack(Action<VStack> action)
    {
        var vStack = new VStack(_defaultTextStyle);
        action(vStack);
        _viewBuilder = vStack;
    }

    public void WithTable(Action<TableViewBuilder> action)
    {
        var table = new TableViewBuilder(_defaultTextStyle);
        action(table);
        _viewBuilder = table;
    }

    public void WithText(Func<TextViewBuilder, IViewBuilder> builder)
    {
        _viewBuilder = builder(new TextViewBuilder(_defaultTextStyle));
    }

    public void WithImage(Func<ImageViewBuilder, IViewBuilder> builder)
    {
        _viewBuilder = builder(new ImageViewBuilder());
    }
}
