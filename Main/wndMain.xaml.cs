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

        public int invoiceNumber;
        double invoiceCost;
        
        int lineItem = 1;
        
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
            dgInvoice.IsEnabled = true;
            cbItems.IsEnabled = true;
            btnAddItem.IsEnabled = true;
            btnRemoveItem.IsEnabled = true;
        }

        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            clsMainLogic mainLogic = new clsMainLogic();
            //int count = 0;
            //count = mainLogic.CountItems(invoiceNumber);
            //
            //if (count != 0)
            //{
            //    for (int i = 0; i < addItems.Count; i++)
            //    {
            //        //removes all items in LineItems table for displayed invoice
            //       clsMainLogic.RemoveItem(invoiceNumber);
            //    }
            //}
            //int invoiceNum = invoiceNumber;
            //clsMainLogic.EditInvoice(invoiceCost, invoiceNum);

            // TODO: Distinguish between updating an invoice and creating a new one
            // The easiest way to do this is probably to check if the invoice number is already in the database
            // If it isn't we create a new invoice.

            // Return if no date is selected
            if (!InvoiceDate.SelectedDate.HasValue)
            {
                return;
            }
            // Return if no items are added
            if(addItems.Count == 0)
            {
                return;
            }

            // Extract the total cost
            double totalCost = double.Parse(TotalCost.Content.ToString());
            
            // Save the new invoice to the database
            clsMainLogic.SaveNewInvoice(InvoiceDate.SelectedDate.ToString(), totalCost);

            // The invoice number of the last created invoice
            int invoiceNumber = clsMainLogic.GetMaxInvoice();

            int lineNum = 1;
            //adds all items from addItems list to LineItems table.
            for (int i = 0; i < addItems.Count; i++)
            {
                clsMainLogic.AddItem(invoiceNumber, lineNum, addItems[i].itemCode);
                lineNum++;
            }

            // Get all items on the last created invoice
            List<clsItem> lineItems = mainLogic.GetInvoice(invoiceNumber);

            // Display line items
            dgInvoice.ItemsSource = lineItems;

            // Display invoice number
            InvoiceNumber.Content = invoiceNumber;

        }

        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            //int invoiceNum = 0;

            //if (InvoiceDate.Text == null)
            //{ return; }
            //string date = InvoiceDate.Text;
            //clsMainLogic.SaveNewInvoice(date);
            //invoiceNum = clsMainLogic.GetMaxInvoice();
            //PopulateNewInvoice(invoiceNum);
            //invoiceNumber = invoiceNum;
            //InvoiceNumber.Content = "TBD";
            //dgInvoice.IsEnabled = true;


        }

        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = cbItems.SelectedItem as string;
            clsMainLogic mainLogic = new clsMainLogic();
            List<clsItem> itemDetails = mainLogic.GetAllItems();
            for (int i = 0; i < itemDetails.Count; i++)
            {
                if (selected == itemDetails[i].itemDescription)
                { Cost.Content = itemDetails[i].cost; }
            }
            
        }

        //Populating certain invoice
        private void PopulateInvoice(int num)
        {
            clsMainLogic mainLogic = new clsMainLogic();
            List<clsItem> invoice = mainLogic.GetInvoice(num);
            for(int i = 0; i < invoice.Count; i++)
            {
                clsItem item = new clsItem();
                item = invoice[i];
                addItems.Add(invoice[i]);
                invoiceCost += double.Parse(invoice[i].cost);
            }
            dgInvoice.ItemsSource = addItems;
            dgInvoice.Items.Refresh();
            TotalCost.Content = invoiceCost;
        }
        //populate invoice given invoicenum
        private void PopulateNewInvoice(int invoiceNum)
        {
            clsMainLogic mainLogic = new clsMainLogic();
            List<clsItem> invoice = mainLogic.GetInvoice(invoiceNum);
            dgInvoice.ItemsSource = invoice;
            dgInvoice.Items.Refresh();
        }

        //Search Window
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                search = new wndSearch();
                search.ShowDialog(); 
                invoiceNumber = search.selectedInvoiceID;
                invoiceCost = 0;
                addItems.Clear();
                PopulateInvoice(invoiceNumber);

                //TODO: Fill in date field
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
            // If no item is selected don't do anything
            if(cbItems.SelectedIndex == -1)
            {
                return;
            }
            
            string selected = cbItems.SelectedItem.ToString();
            string cost = "";
            
            clsMainLogic mainLogic = new clsMainLogic();
            List<clsItem> itemDetails = mainLogic.GetAllItems();
            int x = 0;
            for (int i = 0; i < itemDetails.Count; i++)
            {
                if (selected == itemDetails[i].itemDescription)
                {
                    cost = itemDetails[i].cost;
                    addItems.Add(itemDetails[i]);
                    //dgInvoice.Items.Add(addItems[x]);
                    x++;
                }
                
            }
            dgInvoice.ItemsSource = addItems;
            dgInvoice.Items.Refresh();
            double c = double.Parse(cost);
            //totalCost += c;
            invoiceCost += c;
            //TotalCost.Content = "$" + invoiceCost;
            TotalCost.Content = invoiceCost;
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            // if no item is selected, do nothing
            if (dgInvoice.SelectedIndex == -1)
            {
                return;
            }
            clsItem item = new clsItem();
            item = (clsItem)dgInvoice.SelectedItem;
            addItems.Remove(item);
            double itemCost = double.Parse(item.cost);
            invoiceCost -= itemCost;
            TotalCost.Content = invoiceCost;
            //TotalCost.Content = "$" + invoiceCost;
            dgInvoice.ItemsSource = addItems;
            dgInvoice.Items.Refresh();
        }

        
    }
}
