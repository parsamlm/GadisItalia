using System.Windows;
using System.Windows.Controls;

namespace GadisItalia;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        using (var context = new GadisDbContext())
        {
            var suppliers = context.Suppliers.Where(s => s.FlagAttivo).ToList();
            SupplierList.ItemsSource = suppliers;
            SupplierList.Items.Refresh();
        }
    }

    private void SupplierList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SupplierList.SelectedItem is Supplier selectedSupplier)
        {
            var supplierPreview = new SupplierPreviewModel(selectedSupplier);
            var fornitoreWindow = new Fornitore(supplierPreview);
            fornitoreWindow.Show();
        }
    }

}