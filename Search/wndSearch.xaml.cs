using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Invoices.Common;

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
        private static int iSelectedInvoiceID;
        /// <summary>
        /// iSelectedInvoiceID getter/setter
        /// </summary>
        public int selectedInvoiceID
        {
            get { return iSelectedInvoiceID; }
            set { iSelectedInvoiceID = value; }
        }


        public wndSearch()
        {
            try
            {
                InitializeComponent();

                // Reset window to its default state
                ResetSearchWindow();

                // Reset selected invoice ID
                selectedInvoiceID = -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Resets search window to its original state
        /// Binds data grid to all invoices
        /// Clears combo box selections
        /// Binds invoice number combo box to distinct invoice IDs
        /// Binds invoice date combo box to distinct invoice dates
        /// Binds invoice costs combo box to distinct invoice total costs
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ResetSearchWindow()
        {
            try
            {
                // Class containing search logic and database connections
                clsSearchLogic searchLogic = new clsSearchLogic();

                // List off all invoices
                List<clsInvoice> invoices = searchLogic.GetInvoices();

                // Bind invoices list to datagrid
                dgInvoices.ItemsSource = invoices;

                // Clear combo box selections
                ClearComboBoxSelections();

                // Get distinct invoice IDs
                List<string> invoiceDetails = searchLogic.GetInvoiceIDs();

                // Bind invoice IDs to Invoice IDs combo box
                cbInvoiceNumber.ItemsSource = invoiceDetails;

                // Get distinct invoice dates
                invoiceDetails = searchLogic.GetInvoiceDates();

                // Bind invoice dates to Invoice dates combo box
                cbInvoiceDate.ItemsSource = invoiceDetails;

                // Get distinct invoice total costs
                invoiceDetails = searchLogic.GetInvoiceCosts();

                //invoiceDetails.Sort(); // This doesn't seem to be necessary // TODO: REMOVE if not needed
                // There is a requirement that total cost need to be sorted in the combo box from smallest to largest
                // However, the costs seem to already be sorted when viewed from the combo box

                // Bind invoice costs to Invoice costs combo box
                cbCosts.ItemsSource = invoiceDetails;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Hides the wndSearch Screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // ResetSearchWindow(); This may not need to be here. Will need to test if this is always called in wndSearch()
            // If not, this may be a good place to call it again.
            this.Hide();
        }

        /// <summary>
        /// Updates static variable selectedInvoideID to be accessed by main.
        /// Hides wndSearch Screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            // No invoice is selected. Do nothing.
            if (dgInvoices.SelectedIndex == -1)
            {
                return;
            }

            // Get the currently selected invoice
            clsInvoice currentInvoice = (clsInvoice)dgInvoices.SelectedItem;

            // Store the selected invocies ID in selectedInvoiceID to be accessed by main.
            selectedInvoiceID = Int32.Parse(currentInvoice.invoiceID);

            // Hide this window
            this.Hide();
        }

        /// <summary>
        /// Updates the invoices datagrid based on the selected items of each combobox
        /// </summary>
        public void UpdateInvoiceDatagrid()
        {
            // Current invoice ID
            string invoiceID = "";

            // Current invoice Date
            string invoiceDate = "";

            // Current invoice total cost
            int invoiceTotalCost = -1;

            // If invoice ID is selected set invoiceID variable
            if(cbInvoiceNumber.SelectedIndex != -1)
            {
                invoiceID = cbInvoiceNumber.SelectedItem.ToString();
            }

            // If date is selected set invoiceDate variable
            if(cbInvoiceDate.SelectedIndex != -1)
            {
                invoiceDate = cbInvoiceDate.SelectedItem.ToString();
            }

            // If total cost is selected set invoiceTotalCost variable
            if(cbCosts.SelectedIndex != -1)
            {
                invoiceTotalCost = Int32.Parse(cbCosts.SelectedItem.ToString());
            }

            // No combo boxes are selected. Do nothing
            if(string.IsNullOrEmpty(invoiceID) && string.IsNullOrEmpty(invoiceDate) && invoiceTotalCost == -1)
            {
                return;
            }

            // Class containing search logic
            clsSearchLogic searchLogic = new clsSearchLogic();

            /* 
             * 
             * What follows are several if statements to determine what combination of combo boxes are filled 
             * and which functions to call.
             * 
             */

            // All three combo boxes have a selected item
            if(!string.IsNullOrEmpty(invoiceID) && !string.IsNullOrEmpty(invoiceDate) && invoiceTotalCost != -1)
            {
                // List to hold invoices matching search criteria
                List<clsInvoice> invoices = searchLogic.GetInvoices(invoiceID, invoiceDate, invoiceTotalCost);

                // Bind invoices list to datagrid
                dgInvoices.ItemsSource = invoices;

                // Nothing else to do. Return
                return;
            }

            // Only invoiceID is selected
            if(!string.IsNullOrEmpty(invoiceID) && string.IsNullOrEmpty(invoiceDate) && invoiceTotalCost == -1)
            {
                // List to hold invoices matching search criteria
                List<clsInvoice> invoices = searchLogic.GetInvoicesByID(invoiceID);

                // Bind invoices list to datagrid
                dgInvoices.ItemsSource = invoices;

                // Nothing else to do. Return
                return;
            }

            // Only invoice date is selected
            if (string.IsNullOrEmpty(invoiceID) && !string.IsNullOrEmpty(invoiceDate) && invoiceTotalCost == -1)
            {
                // List to hold invoices matching search criteria
                List<clsInvoice> invoices = searchLogic.GetInvoicesByDate(invoiceDate);

                // Bind invoices list to datagrid
                dgInvoices.ItemsSource = invoices;

                // Nothing else to do. Return
                return;
            }

            // Only invoice total cost is selected
            if (string.IsNullOrEmpty(invoiceID) && string.IsNullOrEmpty(invoiceDate) && invoiceTotalCost != -1)
            {
                // List to hold invoices matching search criteria
                List<clsInvoice> invoices = searchLogic.GetInvoicesByCost(invoiceTotalCost);

                // Bind invoices list to datagrid
                dgInvoices.ItemsSource = invoices;

                // Nothing else to do. Return
                return;
            }

            // Only invoice ID and tdate are selected
            if (!string.IsNullOrEmpty(invoiceID) && !string.IsNullOrEmpty(invoiceDate) && invoiceTotalCost == -1)
            {
                // List to hold invoices matching search criteria
                List<clsInvoice> invoices = searchLogic.GetInvoices(invoiceID, invoiceDate);

                // Bind invoices list to datagrid
                dgInvoices.ItemsSource = invoices;

                // Nothing else to do. Return
                return;
            }

            // Only invoice ID and total cost is selected
            if (!string.IsNullOrEmpty(invoiceID) && string.IsNullOrEmpty(invoiceDate) && invoiceTotalCost != -1)
            {
                // List to hold invoices matching search criteria
                List<clsInvoice> invoices = searchLogic.GetInvoices(invoiceID, invoiceTotalCost);

                // Bind invoices list to datagrid
                dgInvoices.ItemsSource = invoices;

                // Nothing else to do. Return
                return;
            }

            // Only invoice date and total cost are selected
            if (string.IsNullOrEmpty(invoiceID) && !string.IsNullOrEmpty(invoiceDate) && invoiceTotalCost != -1)
            {
                // List to hold invoices matching search criteria
                List<clsInvoice> invoices = searchLogic.GetInvoicesByDateAndCost(invoiceDate, invoiceTotalCost);

                // Bind invoices list to datagrid
                dgInvoices.ItemsSource = invoices;
            }

        }

        /// <summary>
        /// Resets the search window to its original state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            // Reset search window
            ResetSearchWindow();
        }

        /// <summary>
        /// Clears all combo boxes by setting their selected index to -1
        /// </summary>
        public void ClearComboBoxSelections()
        {
            // Clear combo box selections
            cbInvoiceNumber.SelectedIndex = -1;
            cbInvoiceDate.SelectedIndex = -1;
            cbCosts.SelectedIndex = -1;
        }

        /// <summary>
        /// Call UpdateInvoiceDatagrid()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Nothing is selected. To nothing.
            if (cbInvoiceNumber.SelectedIndex == -1)
            {
                return;
            }

            // Update datagrid based on combobox selections
            UpdateInvoiceDatagrid();
        }

        /// <summary>
        /// Call UpdateInvoiceDatagrid()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Nothing is selected. To nothing.
            if (cbInvoiceDate.SelectedIndex == -1)
            {
                return;
            }

            // Update datagrid based on combobox selections
            UpdateInvoiceDatagrid();
        }

        /// <summary>
        /// Call UpdateInvoiceDatagrid()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Nothing is selected. To nothing.
            if (cbCosts.SelectedIndex == -1)
            {
                return;
            }

            // Update datagrid based on combobox selections
            UpdateInvoiceDatagrid();
        }


    }
}
