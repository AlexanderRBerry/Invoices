using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Invoices.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Invoices.Main
{
    public class clsMainLogic
    {
        public List<clsItem> GetAllItems()
        {
            try
            {
                List<clsItem> items = new List<clsItem>();
                // Connection to the database
                clsDataAccess db = new clsDataAccess();
                // Class holding relevant SQL statements
                clsMainSQL sqlStatements = new clsMainSQL();
                // SQL to retreive all items
                string sSQL = sqlStatements.GetItemsSQL();
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

        public static void RemoveItem(int invoiceNum)
        {
            clsDataAccess db = new clsDataAccess();
            clsMainSQL sqlStatements = new clsMainSQL();
            string sSQL = sqlStatements.RemoveItemsSQL(invoiceNum);
            db.ExecuteNonQuery(sSQL);
        }

        public int CountItems(int invoiceNum)
        {
            clsDataAccess db = new clsDataAccess();
            clsMainSQL sqlStatements = new clsMainSQL();
            string sSQL = sqlStatements.GetItemsCountSQL(invoiceNum);
            string count = db.ExecuteScalarSQL(sSQL);
            int iCount = Int32.Parse(count);
            return iCount;
        }

        //SaveNewInvoice(clsInvoice)
        public static void SaveNewInvoice(string date)
        {
            clsDataAccess db = new clsDataAccess();
            clsMainSQL sqlStatements = new clsMainSQL();
            string sSQL = sqlStatements.InsertInvoiceSQL(date, 0);//cost initially is $0, is updated as items are added
            db.ExecuteNonQuery(sSQL);
        }
        public static void AddItem(int invoiceNum, int lineItemNum, string itemCode)
        {
            clsDataAccess db = new clsDataAccess();
            clsMainSQL sqlStatements = new clsMainSQL();
            string sSQL = sqlStatements.InsertItemsSQL(invoiceNum, lineItemNum, itemCode);
            db.ExecuteNonQuery(sSQL);
        }
        //gets InvoiceNum given a certain date
        public static int GetInvoiceNumber(string date)
        {
            clsDataAccess db = new clsDataAccess();
            clsMainSQL sqlStatements = new clsMainSQL();
            string sSQL = sqlStatements.GetInvoiceNumSQL(date);
            int number = db.ExecuteNonQuery(sSQL);
            return number;
        }
        //EditInvoice(clsOldInvoice, clsNewInvoice)
        public static void EditInvoice(double cost, int invoiceNum)
        {
            clsDataAccess db = new clsDataAccess();
            clsMainSQL sqlStatements = new clsMainSQL();
            string sSQL = sqlStatements.UpdateInvoicesSQL(cost, invoiceNum);
            db.ExecuteNonQuery(sSQL);
        }
        public static int GetMaxInvoice()
        {
            clsDataAccess db = new clsDataAccess();
            clsMainSQL sqlStatements = new clsMainSQL();
            string sSQL = sqlStatements.GetMaxInvoiceSQL();
            string invoiceNum = db.ExecuteScalarSQL(sSQL);
            int invoice = Int32.Parse(invoiceNum);
            return invoice;
        }
        //GetInvoice(InvoiceNumber) returns clsInvoice - Get the invoice and items for the selected invoice from search window
        public List<clsItem> GetInvoice(int invoiceNum)
        { 
            List<clsItem> invoices = new List<clsItem>();
            clsDataAccess db = new clsDataAccess();
            // Class holding relevant SQL statements
            clsMainSQL sqlStatements = new clsMainSQL();
            // SQL to retreive all items
            string sSQL = sqlStatements.GetLineItemsSQL(invoiceNum);
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
                invoices.Add(item);
            }

            return invoices;
        }

       
    }
}
