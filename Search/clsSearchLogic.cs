using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Exception_Handler;
using Invoices.Common;

namespace Invoices.Search
{
    public class clsSearchLogic
    {
        /// <summary>
        /// Gets a list of all invoices in the database
        /// </summary>
        /// <returns>List<clsInvoice> of all invoices</returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoices()
        {
            try
            {
                // List to hold invoices
                List<clsInvoice> invoices = new List<clsInvoice>();

                // Connection to the database
                clsDataAccess db = new clsDataAccess();

                // Class holding relevant SQL statements
                clsSearchSQL sqlStatements = new clsSearchSQL();

                // SQL to retreive all invoices
                string sSQL = sqlStatements.GetInvoicesSQL();

                // The number of rows returned
                int rows = 0;

                // Dataset holding all invoices
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add all invoices to invoices list
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // dr[i] will hold the value for the ith column of the given row

                    // object to hold invoice details
                    clsInvoice invoice = new clsInvoice();

                    // Fill each invoice attribute with data from the database
                    invoice.invoiceID = row[0].ToString();
                    invoice.invoiceDate = row[1].ToString();
                    invoice.totalCost = row[2].ToString();

                    // Add the completed invoice to invoices list
                    invoices.Add(invoice);
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

        /// <summary>
        /// Gets a list of all invoices matching search criteria
        /// </summary>
        /// <param name="invoiceID">Search Criteria invoice ID</param>
        /// <returns>List<clsInvoice> of invoices</returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoicesByID(string invoiceID)
        {
            try
            {
                // List to hold invoices
                List<clsInvoice> invoices = new List<clsInvoice>();

                // Connection to the database
                clsDataAccess db = new clsDataAccess();

                // Class holding relevant SQL statements
                clsSearchSQL sqlStatements = new clsSearchSQL();

                // SQL to retreive all invoices matching invoiceID
                string sSQL = sqlStatements.GetInvoicesSQL(invoiceID);

                // The number of rows returned
                int rows = 0;

                // Dataset holding all invoices matching search criteria
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add all invoices to invoices list
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // dr[i] will hold the value for the ith column of the given row

                    // object to hold invoice details
                    clsInvoice invoice = new clsInvoice();

                    // Fill each invoice attribute with data from the database
                    invoice.invoiceID = row[0].ToString();
                    invoice.invoiceDate = row[1].ToString();
                    invoice.totalCost = row[2].ToString();

                    // Add the completed invoice to invoices list
                    invoices.Add(invoice);
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

        /// <summary>
        /// Gets a list of all invoices matching the seach criteria
        /// </summary>
        /// <param name="invoiceID">Search Criteria invoice ID</param>
        /// <param name="invoiceDate">Search Criteria invoice date</param>
        /// <returns>List<clsInvoice> of invoices</returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoices(string invoiceID, string invoiceDate)
        {
            try
            {
                // List to hold invoices
                List<clsInvoice> invoices = new List<clsInvoice>();

                // Connection to the database
                clsDataAccess db = new clsDataAccess();

                // Class holding relevant SQL statements
                clsSearchSQL sqlStatements = new clsSearchSQL();

                // SQL to retreive all invoices matching search criteria
                string sSQL = sqlStatements.GetInvoicesSQL(invoiceID, invoiceDate);

                // The number of rows returned
                int rows = 0;

                // Dataset holding all invoices matching criteria
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add all invoices to invoices list
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // dr[i] will hold the value for the ith column of the given row

                    // object to hold invoice details
                    clsInvoice invoice = new clsInvoice();

                    // Fill each invoice attribute with data from the database
                    invoice.invoiceID = row[0].ToString();
                    invoice.invoiceDate = row[1].ToString();
                    invoice.totalCost = row[2].ToString();

                    // Add the completed invoice to invoices list
                    invoices.Add(invoice);
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

        /// <summary>
        /// Gets a list of all invoices matching the seach criteria
        /// </summary>
        /// <param name="invoiceID">Search Criteria invoice ID</param>
        /// <param name="invoiceCost">Search Criteria invoice cost</param>
        /// <returns>List<clsInvoice> of invoices</returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoices(string invoiceID, int invoiceCost)
        {
            try
            {
                // List to hold invoices
                List<clsInvoice> invoices = new List<clsInvoice>();

                // Connection to the database
                clsDataAccess db = new clsDataAccess();

                // Class holding relevant SQL statements
                clsSearchSQL sqlStatements = new clsSearchSQL();

                // SQL to retreive all invoices matching search criteria
                string sSQL = sqlStatements.GetInvoicesByIDAndCost(invoiceID, invoiceCost);

                // The number of rows returned
                int rows = 0;

                // Dataset holding all invoices matching criteria
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add all invoices to invoices list
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // dr[i] will hold the value for the ith column of the given row

                    // object to hold invoice details
                    clsInvoice invoice = new clsInvoice();

                    // Fill each invoice attribute with data from the database
                    invoice.invoiceID = row[0].ToString();
                    invoice.invoiceDate = row[1].ToString();
                    invoice.totalCost = row[2].ToString();

                    // Add the completed invoice to invoices list
                    invoices.Add(invoice);
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

        /// <summary>
        /// Gets a list of all invoices matching the seach criteria
        /// </summary>
        /// <param name="invoiceID">Search Criteria invoice ID</param>
        /// <param name="invoiceDate">Search Criteria invoice date</param>
        /// <param name="invoiceCost">Search Criteria invoice total cost</param>
        /// <returns>List<clsInvoice> of invoices</returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoices(string invoiceID, string invoiceDate, int invoiceCost)
        {
            try
            {
                // List to hold invoices
                List<clsInvoice> invoices = new List<clsInvoice>();

                // Connection to the database
                clsDataAccess db = new clsDataAccess();

                // Class holding relevant SQL statements
                clsSearchSQL sqlStatements = new clsSearchSQL();

                // SQL to retreive all invoices matching search criteria
                string sSQL = sqlStatements.GetInvoicesSQL(invoiceID, invoiceDate, invoiceCost);

                // The number of rows returned
                int rows = 0;

                // Dataset holding all invoices
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add all invoices to invoices list
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // dr[i] will hold the value for the ith column of the given row

                    // object to hold invoice details
                    clsInvoice invoice = new clsInvoice();

                    // Fill each invoice attribute with data from the database
                    invoice.invoiceID = row[0].ToString();
                    invoice.invoiceDate = row[1].ToString();
                    invoice.totalCost = row[2].ToString();

                    // Add the completed invoice to invoices list
                    invoices.Add(invoice);
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

        

        /// <summary>
        /// Gets a list of all invoices matching search criteria
        /// </summary>
        /// <param name="invoiceDate">Search Criteria invoice date</param>
        /// <returns>List<clsInvoice> of invoices</returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoicesByDate(string invoiceDate)
        {
            try
            {
                // List to hold invoices
                List<clsInvoice> invoices = new List<clsInvoice>();

                // Connection to the database
                clsDataAccess db = new clsDataAccess();

                // Class holding relevant SQL statements
                clsSearchSQL sqlStatements = new clsSearchSQL();

                // SQL to retreive all invoices matching invoice date
                string sSQL = sqlStatements.GetInvoicesByDateSQL(invoiceDate);

                // The number of rows returned
                int rows = 0;

                // Dataset holding all invoices matching search criteria
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add all invoices to invoices list
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // dr[i] will hold the value for the ith column of the given row

                    // object to hold invoice details
                    clsInvoice invoice = new clsInvoice();

                    // Fill each invoice attribute with data from the database
                    invoice.invoiceID = row[0].ToString();
                    invoice.invoiceDate = row[1].ToString();
                    invoice.totalCost = row[2].ToString();

                    // Add the completed invoice to invoices list
                    invoices.Add(invoice);
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

        /// <summary>
        /// Gets a list of all invoices matching search criteria
        /// </summary>
        /// <param name="invoiceCost">Search Criteria invoice total cost</param>
        /// <returns>List<clsInvoice> of invoices</returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoicesByCost(int invoiceCost)
        {
            try
            {
                // List to hold invoices
                List<clsInvoice> invoices = new List<clsInvoice>();

                // Connection to the database
                clsDataAccess db = new clsDataAccess();

                // Class holding relevant SQL statements
                clsSearchSQL sqlStatements = new clsSearchSQL();

                // SQL to retreive all invoices with a matching total cost
                string sSQL = sqlStatements.GetInvoicesByCostSQL(invoiceCost);

                // The number of rows returned
                int rows = 0;

                // Dataset holding all invoices matching search criteria
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add all invoices to invoices list
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // dr[i] will hold the value for the ith column of the given row

                    // object to hold invoice details
                    clsInvoice invoice = new clsInvoice();

                    // Fill each invoice attribute with data from the database
                    invoice.invoiceID = row[0].ToString();
                    invoice.invoiceDate = row[1].ToString();
                    invoice.totalCost = row[2].ToString();

                    // Add the completed invoice to invoices list
                    invoices.Add(invoice);
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

        /// <summary>
        /// Gets a list of all invoices matching search criteria
        /// </summary>
        /// <param name="invoiceDate">Search Criteria invoice date</param>
        /// <param name="invoiceCost">Search Criteria invoice total cost</param>
        /// <returns>List<clsInvoice> of invoices</returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoicesByDateAndCost(string invoiceDate, int invoiceCost)
        {
            try
            {
                // List to hold invoices
                List<clsInvoice> invoices = new List<clsInvoice>();

                // Connection to the database
                clsDataAccess db = new clsDataAccess();

                // Class holding relevant SQL statements
                clsSearchSQL sqlStatements = new clsSearchSQL();

                // SQL to retreive all invoices with a matching total cost and invoice date
                string sSQL = sqlStatements.GetInvoicesSQL(invoiceCost, invoiceDate);

                // The number of rows returned
                int rows = 0;

                // Dataset holding all invoices matching search criteria
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add all invoices to invoices list
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // dr[i] will hold the value for the ith column of the given row

                    // object to hold invoice details
                    clsInvoice invoice = new clsInvoice();

                    // Fill each invoice attribute with data from the database
                    invoice.invoiceID = row[0].ToString();
                    invoice.invoiceDate = row[1].ToString();
                    invoice.totalCost = row[2].ToString();

                    // Add the completed invoice to invoices list
                    invoices.Add(invoice);
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

        /// <summary>
        /// Get all distinct invoice IDs
        /// </summary>
        /// <returns>List<string> distinct invoice IDs</returns>
        /// <exception cref="Exception"></exception>
        public List<string> GetInvoiceIDs()
        {
            try
            {
                // List to hold invoice IDs
                List<string> invoiceIDs = new List<string>();

                // Connection to the database
                clsDataAccess db = new clsDataAccess();

                // Class holding relevant SQL statements
                clsSearchSQL sqlStatements = new clsSearchSQL();

                // SQL to retreive all distinct invoice IDs
                string sSQL = sqlStatements.GetInvoiceNumbersSQL();

                // The number of rows returned
                int rows = 0;

                // Dataset holding all invoice IDs
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add each invoice ID to invoiceIDs
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // Get invoice ID
                    string invoiceID = row[0].ToString();

                    invoiceIDs.Add(invoiceID);
                }

                return invoiceIDs;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get all distinct invoice Dates
        /// </summary>
        /// <returns>List<string> distinct invoice dates</returns>
        /// <exception cref="Exception"></exception>
        public List<string> GetInvoiceDates()
        {
            try
            {
                // List to hold invoice dates
                List<string> invoiceDates = new List<string>();

                // Connection to the database
                clsDataAccess db = new clsDataAccess();

                // Class holding relevant SQL statements
                clsSearchSQL sqlStatements = new clsSearchSQL();

                // SQL to retreive all distinct invoice dates
                string sSQL = sqlStatements.GetInvoiceDatesSQL();

                // The number of rows returned
                int rows = 0;

                // Dataset holding all invoice dates
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add each invoice date to invoiceDates
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // Get invoice ID
                    string invoiceDate = row[0].ToString();

                    invoiceDates.Add(invoiceDate);
                }

                return invoiceDates;
            }
            catch (Exception ex)
            {
                //Throw an exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get all distinct invoice total costs
        /// </summary>
        /// <returns>List<string> distinct invoice total costs</returns>
        /// <exception cref="Exception"></exception>
        public List<string> GetInvoiceCosts()
        {
            try
            {
                // List to hold invoice total costs
                List<string> invoiceCosts = new List<string>();

                // Connection to the database
                clsDataAccess db = new clsDataAccess();

                // Class holding relevant SQL statements
                clsSearchSQL sqlStatements = new clsSearchSQL();

                // SQL to retreive all distinct invoice total costs
                string sSQL = sqlStatements.GetInvoiceCostsSQL();

                // The number of rows returned
                int rows = 0;

                // Dataset holding all invoices total costs
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref rows);

                // Add each invoice cost to invoiceCosts
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // Get invoice ID
                    string invoiceCost = row[0].ToString();

                    invoiceCosts.Add(invoiceCost);
                }

                return invoiceCosts;
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
