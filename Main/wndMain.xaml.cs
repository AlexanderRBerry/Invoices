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
        //List to store items in an invoice
        private List<clsItem> addItems = new List<clsItem>();

        //invoiceNumber of displayed invoice
        public int invoiceNumber = 0;

        //total cost of displayed invoice
        double invoiceCost;

        public wndMain()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose; // Ensures complete shutdown

            //calls loadItems method
            loadItems();
        }

        //loads all items from database to combo box
        private void loadItems()
        {
            try
            {
                //connecting to clsMainLogic
                clsMainLogic mainLogic = new clsMainLogic();

                //list of all items
                List<clsItem> itemDetails = mainLogic.GetAllItems();

                //adding items to items drop down
                for (int i = 0; i < itemDetails.Count; i++)
                {
                    cbItems.Items.Add(itemDetails[i].itemDescription);
                }
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //Enables user to edit displayed invoice
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {   try
            {
                dgInvoice.IsEnabled = true;
                cbItems.IsEnabled = true;
                btnAddItem.IsEnabled = true;
                btnRemoveItem.IsEnabled = true;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //saves items from displayed invoice to databse
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clsMainLogic mainLogic = new clsMainLogic();
                
                //count to determine if invoice exists on LineItems table
                int count = 0;

                //Calls method for counting number of items in LineItems table given invoiceNumber
                count = mainLogic.CountItems(invoiceNumber);

                //if Invoice already exists
                if (count != 0)
                {
                    //removes all items in LineItems table for displayed invoice
                    for (int i = 0; i < addItems.Count; i++)
                    {
                        clsMainLogic.RemoveItem(invoiceNumber);
                    }
                }

                //updates invoice cost
                clsMainLogic.EditInvoice(invoiceCost, invoiceNumber);

                //int to increment lineNum
                int lineNum = 1;

                //adds all items from addItems list to LineItems table.
                for (int i = 0; i < addItems.Count; i++)
                {
                    clsMainLogic.AddItem(invoiceNumber, lineNum, addItems[i].itemCode);
                    lineNum++;
                }

                // Display invoice number
                InvoiceNumber.Content = invoiceNumber;

                //disable add/remove button, and datagrid/combo box
                dgInvoice.IsEnabled = false;
                cbItems.IsEnabled = false;
                btnAddItem.IsEnabled = false;
                btnRemoveItem.IsEnabled = false;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        //Creates new invoice given user inputed date
        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int invoiceNum = 0;
                
                //clears addItems list every time a new invoice is created
                addItems.Clear();

                //resets invoiceCost to 0 when created
                invoiceCost = 0;

                // Return if no date is selected
                if (!InvoiceDate.SelectedDate.HasValue)
                {
                    return;
                }

                //set date value to selected date
                string date = InvoiceDate.SelectedDate.Value.ToString();

                //calls method to save new invoice
                clsMainLogic.SaveNewInvoice(date);

                //sets invoiceNum to last created invoice
                invoiceNum = clsMainLogic.GetMaxInvoice();

                //shows new invoice
                PopulateNewInvoice(invoiceNum);

                //sets invoiceNumber to displayed invoice
                invoiceNumber = invoiceNum;

                //invoice Number TBD, will display after clicking save
                InvoiceNumber.Content = "TBD";
                dgInvoice.IsEnabled = true;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        //method for combo box selection changing
        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //selected item
                string selected = cbItems.SelectedItem as string;

                //connection to clsMainLogic
                clsMainLogic mainLogic = new clsMainLogic();

                //list for all items in database
                List<clsItem> itemDetails = mainLogic.GetAllItems();
                for (int i = 0; i < itemDetails.Count; i++)
                {
                    //if selected item equals current item in list
                    if (selected == itemDetails[i].itemDescription)
                    //set cost and display
                    { Cost.Content = itemDetails[i].cost; }
                }
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        //Populating certain invoice
        private void PopulateInvoice(int num)
        {
            try
            {
                clsMainLogic mainLogic = new clsMainLogic();
                //list for items on selected invoice
                List<clsItem> invoice = mainLogic.GetInvoice(num);
                for (int i = 0; i < invoice.Count; i++)
                {
                    //setting instance of item
                    clsItem item = new clsItem();
                    item = invoice[i];
                    //adding all items from selected invoice to addItems list
                    addItems.Add(invoice[i]);
                    //setting cost of all items on invoice
                    invoiceCost += double.Parse(invoice[i].cost);
                }
                dgInvoice.ItemsSource = addItems;
                dgInvoice.Items.Refresh();
                TotalCost.Content = invoiceCost;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        //populate invoice given invoicenum
        private void PopulateNewInvoice(int invoiceNum)
        {
            try
            {
                clsMainLogic mainLogic = new clsMainLogic();
                //list for items on invoice
                List<clsItem> invoice = mainLogic.GetInvoice(invoiceNum);
                //displaying new invoice
                dgInvoice.ItemsSource = invoice;
                dgInvoice.Items.Refresh();
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //Search Window
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                search = new wndSearch();
                search.ShowDialog(); //show search screen

                //getting selected invoice number 
                invoiceNumber = search.selectedInvoiceID;
                //displaying invoice number
                InvoiceNumber.Content = invoiceNumber;
                //resetting invoiceCost to 0
                invoiceCost = 0;
                //clearing addItems list for new invoice
                addItems.Clear();
                //displaying invoice
                PopulateInvoice(invoiceNumber);
                //enabling user to edit invoice
                dgInvoice.IsEnabled = true;
                cbItems.IsEnabled = true;
                btnRemoveItem.IsEnabled = true;
                btnAddItem.IsEnabled = true;

            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //Items window
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                items = new wndItems();
                items.ShowDialog();
                
                //reloading items in comnbo box, in case items on database changed
                loadItems();
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
            try
            {
                // If no item is selected don't do anything
                if (cbItems.SelectedIndex == -1)
                {
                    return;
                }
                //selected item
                string selected = cbItems.SelectedItem.ToString();
                string cost = "";

                clsMainLogic mainLogic = new clsMainLogic();

                //list of items in database
                List<clsItem> itemDetails = mainLogic.GetAllItems();
                for (int i = 0; i < itemDetails.Count; i++)
                {
                    //if selected equals current item in list
                    if (selected == itemDetails[i].itemDescription)
                    {
                        //set cost
                        cost = itemDetails[i].cost;
                        //add item to addItems list
                        addItems.Add(itemDetails[i]);
                    }
                }
                //displaying selected item in data grid
                dgInvoice.ItemsSource = addItems;
                dgInvoice.Items.Refresh();
                //setting price of item
                double c = double.Parse(cost);
                //saving total cost of invoice
                invoiceCost += c;
                //displaying total cost of invoice
                TotalCost.Content = invoiceCost;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // if no item is selected, do nothing
                if (dgInvoice.SelectedIndex == -1)
                {
                    return;
                }

                //saving item in clsItem
                clsItem item = new clsItem();
                item = (clsItem)dgInvoice.SelectedItem;

                //removing selected item from addItems List
                addItems.Remove(item);
                //setting cost of selected item
                double itemCost = double.Parse(item.cost);
                //updating total cost of invoice
                invoiceCost -= itemCost;
                //displaying new total cost
                TotalCost.Content = invoiceCost;
                //displaying new invoice
                dgInvoice.ItemsSource = addItems;
                dgInvoice.Items.Refresh();
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
