namespace InvoiceKit.Pdf.Views;

public interface ITable : IContainer
{
    /// <summary>
    /// Adds a column builder for making custom column widths.
    /// </summary>
    ITable WithColumnWidths(Action<ColumnBuilder> configureColumns);

    /// <summary>
    ///
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    ITable WithHeader(Action<VStack> configure);

    /// <summary>
    ///
    /// </summary>
    /// <param name="configure"></param>
    /// <param name="configureStyle"></param>
    /// <returns></returns>
    ITable WithHeader(Action<VStack> configure, Func<BlockStyle, BlockStyle> configureStyle);

    /// <summary>
    ///
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    public ITable WithFooter(Action<VStack> configure);

    /// <summary>
    ///
    /// </summary>
    /// <param name="configure"></param>
    /// <param name="configureStyle"></param>
    /// <returns></returns>
    public ITable WithFooter(Action<VStack> configure, Func<BlockStyle, BlockStyle> configureStyle);
}

public interface IRow
{
    IRow WithColumnWidths(Action<ColumnBuilder> configureColumns);
}
