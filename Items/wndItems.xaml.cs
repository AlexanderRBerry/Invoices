﻿using Invoices.Common;
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

            
            dataGrid.ItemsSource = itemsList;
        }

        private void selectedItem(object sender, SelectionChangedEventArgs e)
        {
            
            clsItem selectedItem = new clsItem();
            selectedItem = dataGrid.SelectedItem as clsItem;


            //display selected item to text boxes
            if (selectedItem != null)
            {
                textBoxCode.Text = selectedItem.itemCode;
                textBoxCost.Text = selectedItem.cost;
                textBoxDesc.Text = selectedItem.itemDescription;
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            clsItem item = new clsItem();
            item.cost = textBoxCost.Text;
            double cost = double.Parse(item.cost);
            item.itemDescription = textBoxDesc.Text;
            item.itemCode = textBoxCode.Text;
            bool testPassed = true;

            //checking if itemCode exists
            for (int i = 0; i < itemsList.Count; i++)
            {
                if (itemsList[i].itemCode == item.itemCode)
                {
                    testPassed = false;
                    MessageBox.Show("Item code already exists, cannot add item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if(testPassed == true)
            {
                itemsList.Add(item);
                Trace.WriteLine("\n\n BUTTON code: " + item.itemCode + "     description: " + item.itemDescription + "   cost: " + cost.GetType());
                clsItemsLogic.AddItem(item.itemCode, item.itemDescription, cost);
                
            }

            //TODO: USE SQL
            dataGrid.ItemsSource = itemsList;
            dataGrid.Items.Refresh();
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            clsItem item = new clsItem();
            item.cost = textBoxCost.Text;
            double cost = double.Parse(textBoxCost.Text); //using this for sql, might not have to
            item.itemDescription = textBoxDesc.Text;
            item.itemCode = textBoxCode.Text;
            bool passedInvoiceCheck = false;
            passedInvoiceCheck = clsItemsLogic.InvoiceCheck(item.itemCode);
            
            for(int i = 0; i < itemsList.Count; i++)
            {
                if (itemsList[i].itemCode == textBoxCode.Text)
                {
                    if(passedInvoiceCheck == true)
                    {
                        itemsList[i].cost = textBoxCost.Text;
                        itemsList[i].itemDescription = textBoxDesc.Text;
                    }
                    else
                    {
                        MessageBox.Show("Item belongs to invoice, cannot edit item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
            }

            //TODO: USE SQL

            dataGrid.ItemsSource = itemsList;
            dataGrid.Items.Refresh();

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            clsItem item = new clsItem();
            item.cost = textBoxCost.Text;
            item.itemDescription = textBoxDesc.Text;
            item.itemCode = textBoxCode.Text;
            bool passedInvoiceCheck = false;
            passedInvoiceCheck = clsItemsLogic.InvoiceCheck(item.itemCode);

            for (int i = 0; i < itemsList.Count; i++)
            {
                if (itemsList[i].itemCode == textBoxCode.Text)
                {
                    if (passedInvoiceCheck == true)
                    {
                        itemsList.RemoveAt(i); //can't use itemsList.Remove(item) because it removes based on reference(like a pointer) so when I do clsItem item = new clsItem();, even tho I fill it up with the same info, its a different item therefore a different reference
                        dataGrid.ItemsSource = itemsList;
                        dataGrid.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Item belongs to invoice, cannot delete item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }

            //TODO: USE SQL

            
        }




    }
}
