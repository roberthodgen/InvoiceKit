namespace InvoiceKit.Pdf;

public interface IColumnBuilder
{
    List<ColumnWidth> ColumnWidths { get; }

    List<ColumnWidth> Build();
}
