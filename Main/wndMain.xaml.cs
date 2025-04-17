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
using Invoices.Common;
using System.Reflection;
using Invoices.Items;


namespace Invoices.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        wndSearch search;
        wndItems items;
        private List<clsItem> addItems = new List<clsItem>();

        int invoiceNumber;
        int lineItem = 1;
        double totalCost = 0;
        public wndMain()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose; // Ensures complete shutdown
            clsMainLogic mainLogic = new clsMainLogic();
            List<clsItem> itemDetails = mainLogic.GetAllItems();
            
            for (int i = 0; i < itemDetails.Count; i++)
            {
                cbItems.Items.Add(itemDetails[i].itemDescription);
            }
        }

        //after search window is closed, check property SelectedInvoiceID in the search window to see if an invoice is selected. If so load the invoice

        //after items window is closed, check property HasItemsBeenChanged in the Items window to see if any items were updated. If so re-load items in combo box.

        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            dgInvoice.ItemsSource = addItems;
        }

        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            int invoiceNum = 0;
            if (InvoiceDate.Text == null)
            { return; }
            string date = InvoiceDate.Text;
            clsMainLogic.SaveNewInvoice(date);
            invoiceNum = clsMainLogic.GetInvoiceNumber(date);
            PopulateNewInvoice(invoiceNum);
            invoiceNumber = invoiceNum;
            InvoiceNumber.Content = "TBD";
            
        }

        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = cbItems.SelectedItem as string;
            clsMainLogic mainLogic = new clsMainLogic();
            List<clsItem> itemDetails = mainLogic.GetAllItems();
            for (int i = 0; i < itemDetails.Count; i++)
            {
                if (selected == itemDetails[i].itemDescription)
                { Cost.Content = "$" + itemDetails[i].cost; }
            }
        }

        //Populating certain invoice
        private void PopulateInvoice(int num)
        {
            clsMainLogic mainLogic = new clsMainLogic();
            List<clsItem> invoice = mainLogic.GetInvoice(num);
            dgInvoice.ItemsSource = invoice;
        }
        //populate invoice given invoicenum
        private void PopulateNewInvoice(int invoiceNum)
        {
            clsMainLogic mainLogic = new clsMainLogic();
            List<clsItem> invoice = mainLogic.GetInvoice(invoiceNum);
            dgInvoice.ItemsSource = invoice;
        }

        //Search Window
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                search = new wndSearch();
                search.ShowDialog();
                int invoice = 0;
                invoice = search.selectedInvoiceID;
                PopulateInvoice(invoice);
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                items = new wndItems();
                items.ShowDialog();
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            
            
            string selected = cbItems.SelectedItem.ToString();
            string cost = "";
            
            clsMainLogic mainLogic = new clsMainLogic();
            List<clsItem> itemDetails = mainLogic.GetAllItems();
            for (int i = 0; i < itemDetails.Count; i++)
            {
                if (selected == itemDetails[i].itemDescription)
                {
                    cost = itemDetails[i].cost;
                    addItems.Add(itemDetails[i]);
                    
                }
                
            }
            dgInvoice.ItemsSource = addItems;
            double c = double.Parse(cost);
            totalCost += c;
            TotalCost.Content = "$" + totalCost;
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
