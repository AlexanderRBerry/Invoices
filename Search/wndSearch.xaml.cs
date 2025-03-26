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
using Exception_Handler;

namespace Invoices.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// This variable will be accessible to the main window.
        /// When an invoice is selected the selectedInvoiceID will be updated with the appropraite value.
        /// If the value remains nevative one, no invoice was selected.
        /// </summary>
        public static int selectedInvoiceID = -1;


        public wndSearch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Hides the wndSearch Screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Updates selectedInvoideID and hides wndSearch Screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            // Invoide ID will be stored in a static variable
            // wndMain will access the invoice ID through the static variable
            //TODO: Update selectedInvoiceID
            this.Hide();
        }
    }
}
