using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GadisItalia;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
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
            var suppliers = context.Suppliers.ToList();
            SupplierList.ItemsSource = suppliers;
            SupplierList.Items.Refresh();
        }
    }

    private void SupplierList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SupplierList.SelectedItem is Supplier selectedSupplier)
        {
            var fornitoreWindow = new Fornitore(selectedSupplier);
            fornitoreWindow.Show();
        }
    }

}