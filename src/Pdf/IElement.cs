namespace InvoiceKit.Pdf;

using Elements.Text;
using Elements.Images;
using Layouts.Stacks;
using Layouts.Tables;

public interface IElement
{
    void WithHStack(Action<HStack> action);

    void WithVStack(Action<VStack> action);

    void WithTable(Action<TableLayoutBuilder> action);

    void WithText(Func<TextViewBuilder, IDrawable> builder);

    void WithImage(Func<ImageBuilder, IDrawable> builder);
}
