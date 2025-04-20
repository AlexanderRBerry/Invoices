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
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsMainSQL sqlStatements = new clsMainSQL();
                string sSQL = sqlStatements.RemoveItemsSQL(invoiceNum);
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public int CountItems(int invoiceNum)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsMainSQL sqlStatements = new clsMainSQL();
                string sSQL = sqlStatements.GetItemsCountSQL(invoiceNum);
                string count = db.ExecuteScalarSQL(sSQL);
                int iCount = Int32.Parse(count);
                return iCount;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //SaveNewInvoice(clsInvoice)
        public static void SaveNewInvoice(string date)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsMainSQL sqlStatements = new clsMainSQL();
                string sSQL = sqlStatements.InsertInvoiceSQL(date, 0);//cost initially is $0, is updated as items are added
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static void SaveNewInvoice(string date, double cost)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsMainSQL sqlStatements = new clsMainSQL();
                string sSQL = sqlStatements.InsertInvoiceSQL(date, cost);//cost initially is $0, is updated as items are added
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        public static void AddItem(int invoiceNum, int lineItemNum, string itemCode)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsMainSQL sqlStatements = new clsMainSQL();
                string sSQL = sqlStatements.InsertItemsSQL(invoiceNum, lineItemNum, itemCode);
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        //gets InvoiceNum given a certain date
        public static int GetInvoiceNumber(string date)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsMainSQL sqlStatements = new clsMainSQL();
                string sSQL = sqlStatements.GetInvoiceNumSQL(date);
                int number = db.ExecuteNonQuery(sSQL);
                return number;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        //EditInvoice(clsOldInvoice, clsNewInvoice)
        public static void EditInvoice(double cost, int invoiceNum)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsMainSQL sqlStatements = new clsMainSQL();
                string sSQL = sqlStatements.UpdateInvoicesSQL(cost, invoiceNum);
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        public static int GetMaxInvoice()
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsMainSQL sqlStatements = new clsMainSQL();
                string sSQL = sqlStatements.GetMaxInvoiceSQL();
                string invoiceNum = db.ExecuteScalarSQL(sSQL);
                int invoice = Int32.Parse(invoiceNum);
                return invoice;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        //GetInvoice(InvoiceNumber) returns clsInvoice - Get the invoice and items for the selected invoice from search window
        // It may be more accurate to say this returns a list of line items for an invoice rather than an invoice
        public List<clsItem> GetInvoice(int invoiceNum)
        {
            try
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
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

       
    }
}
