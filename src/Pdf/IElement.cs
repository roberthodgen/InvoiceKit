namespace InvoiceKit.Pdf;

using Elements.Text;
using Elements.Images;
using Containers.Stacks;
using Containers.Tables;

public interface IElement
{
    void WithHStack(Action<HStack> action);

    void WithVStack(Action<VStack> action);

    void WithTable(Action<TableViewBuilder> action);

    void WithText(Func<TextViewBuilder, IViewBuilder> builder);

    void WithImage(Func<ImageViewBuilder, IViewBuilder> builder);
}
