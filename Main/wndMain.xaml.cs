using Invoices.Search;
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

namespace Invoices.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        public wndMain()
        {
            InitializeComponent();

        }
        //after search window is closed, check property SelectedInvoiceID in the search window to see if an invoice is selected. If so load the invoice

        //after items window is closed, check property HasItemsBeenChanged in the Items window to see if any items were updated. If so re-load items in combo box.

        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
