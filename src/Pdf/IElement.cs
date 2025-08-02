namespace InvoiceKit.Pdf;

using Elements.Images;
using Elements.Text;
using Layouts.Stacks;
using Layouts.Tables;

public interface IElement
{
    void WithHStack(Action<HStack> action);

    void WithVStack(Action<VStack> action);

    void WithTable(Action<TableLayoutBuilder> action);

    void WithText(Action<TextBlock> action);

    void WithImage(Func<ImageBuilder, IDrawable> builder);
}
