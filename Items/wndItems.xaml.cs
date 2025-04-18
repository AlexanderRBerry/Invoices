using Invoices.Common;
using Invoices.Main;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Invoices.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        List<clsItem> itemsList = new List<clsItem> ();

        public wndItems()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose; // Ensures complete shutdown
            clsItemsLogic itemLogic = new clsItemsLogic();
            itemsList = itemLogic.GetAllItems();

            for (int i = 0; i < itemsList.Count; i++)
            {
                //cbItems.Items.Add(itemDetails[i].itemDescription);
                Trace.WriteLine("\n\n\n\nYOOOOOOOOOOOO WND: " + itemsList[i].cost + "    " + itemsList[i].itemDescription);
            }
            dataGrid.ItemsSource = itemsList;
        }

        private void selectedItem(object sender, SelectionChangedEventArgs e)
        {
            
            clsItem selectedItem = new clsItem();
            selectedItem = dataGrid.SelectedItem as clsItem;

            //Trace.WriteLine("\n\n\n\n------------------sender: " + sender.ToString());
            //Trace.WriteLine("\n\n\n\n------------------sender: " + sender.GetType);
            Trace.WriteLine("\n\n\n\n item test" + selectedItem.itemDescription + "     cost: " + selectedItem.cost);

            //display selected item to text boxes
            textBoxCode.Text = selectedItem.itemCode;
            textBoxCost.Text = selectedItem.cost;
            textBoxDesc.Text = selectedItem.itemDescription;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            clsItem item = new clsItem();
            item.cost = textBoxCost.Text;
            item.itemDescription = textBoxDesc.Text;
            item.itemCode = textBoxCode.Text;
            itemsList.Add(item);
            dataGrid.ItemsSource = itemsList;
            dataGrid.Items.Refresh();
            //maybe call addItems here to use sql to actually update the database
        }


    }
}
