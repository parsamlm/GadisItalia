using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GadisItalia
{
    public partial class Fornitore : Window
    {
        private Supplier _supplier;

        public Fornitore(Supplier supplier)
        {
            InitializeComponent();
            _supplier = supplier;
            DataContext = _supplier;
            SupplierDetailsLabel.Content = $"Fornitore {_supplier.FornitoreID} - {_supplier.RagioneSocialePerDocumenti}";
            UltimaModificaLabel.Content = _supplier.DataUltAgg;
            DaLabel.Content = _supplier.Responsabile;
        }
    }
}