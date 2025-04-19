using Invoices.Common;
using Invoices.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Items
{
    internal class clsItemsLogic
    {
        //swag
        //getAllItems that returns all the items in a list for datagrid
        //hasItemChanged bool to make sure we don't delete an item thats already in an invoice
        //"bind" list to data grid?


        //try to get items with sql, then display in trace
        public List<clsItem> GetAllItems()
        {
            try
            {
                List<clsItem> items = new List<clsItem>();
                // Connection to the database
                clsDataAccess db = new clsDataAccess();
                // Class holding relevant SQL statements
                clsItemsSQL sqlStatements = new clsItemsSQL();
                // SQL to retreive all items
                string sSQL = sqlStatements.GetItemDescTableSQL();
                int rows = 0;
                // Dataset holding all items
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add all items to items list
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // object to hold item details
                    clsItem item = new clsItem();

                    // Fill each item attribute with data from the database
                    item.itemCode = row[0].ToString();
                    item.itemDescription = row[1].ToString();
                    item.cost = row[2].ToString();

                    // Add the completed item to items list
                    items.Add(item);
                }

                
                return items;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        public static void AddItem(string itemCode, string itemDesc, double cost)
        {
            clsDataAccess db = new clsDataAccess();
            clsItemsSQL sqlStatements = new clsItemsSQL();
            string sSQL = sqlStatements.InsertCodeDescCost(itemCode, itemDesc, cost);
            db.ExecuteNonQuery(sSQL);
            Trace.WriteLine("\n\n LOGIC code: " + itemCode + "     description: " + itemDesc + "   cost: " + cost);
        }

        public static void EditItem(string itemDesc, double itemCost, string itemCode)
        {
            clsDataAccess db = new clsDataAccess();
            clsItemsSQL sqlStatements = new clsItemsSQL();
            string sSQL = sqlStatements.UpdateDescCost(itemDesc, itemCost, itemCode);
            db.ExecuteNonQuery(sSQL);
        }


        public static bool InvoiceCheck(string itemCode)
        {
            List<clsInvoice> invoices = new List<clsInvoice>();
            clsDataAccess db = new clsDataAccess();
            clsItemsSQL sqlStatments = new clsItemsSQL();
            string sSQL = sqlStatments.GetUniqueInvoiceNums(itemCode);
            int rows = 0;
            DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                clsInvoice invoice = new clsInvoice();
                invoice.invoiceID = row[0].ToString();
                invoices.Add(invoice);
            
            }

            if(invoices.Count > 0)
            {
                return false;
            }
            else
            {
                return true;

            }

        }


    }
}
