using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Search
{
    /// <summary>
    /// Contains all SQL statements related to clsSearchLogic
    /// </summary>
    public class clsSearchSQL
    {
        /// <summary>
        /// SQL to retrieve all invoices
        /// </summary>
        /// <returns>string containing SQL to retrieve all invoices</returns>
        public string GetInvoicesSQL()
        {
            return "SELECT * FROM Invoices";
        }

        /// <summary>
        /// SQL to retrieve invoices matching an invoice number
        /// </summary>
        /// <param name="invoiceNum">The invoice number of the specified invoice</param>
        /// <returns>string containing SQL to retrieve invoices with a provided invoice number</returns>
        public string GetInvoicesSQL(string invoiceNum)
        {
            return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// SQL to retrieve invoices matching an invoice number and date
        /// </summary>
        /// <param name="invoiceNum">The invoice number of the specified invoice</param>
        /// <param name="invoiceDate">The date of the invoice</param>
        /// <returns>string containing SQL to retrieve invoices with a provided invoice number and date</returns>
        public string GetInvoicesSQL(string invoiceNum, string invoiceDate)
        {
            return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum +
                   " AND InvoiceDate = #" + invoiceDate + "#";
        }

        /// <summary>
        /// SQL to retrieve invoices matching an invoice number, date, and total cost
        /// </summary>
        /// <param name="invoiceNum">The invoice number of the specified invoice</param>
        /// <param name="invoiceDate">The date of the invoice</param>
        /// <param name="invoiceCost">The invoice total cost</param>
        /// <returns>string containing SQL to retrieve invoices with a provided invoice number, date, and cost</returns>
        public string GetInvoicesSQL(string invoiceNum, string invoiceDate, int invoiceCost)
        {
            return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum +
                   " AND InvoiceDate = #" + invoiceDate + "#" + " AND TotalCost = " + invoiceCost;
        }

        /// <summary>
        /// SQL to retrieve invoices with a specific total cost
        /// </summary>
        /// <param name="invoiceCost">The invoice total cost</param>
        /// <returns>string containing sql to retrieve invoices with a provided cost</returns>
        public string GetInvoicesByCostSQL(int invoiceCost)
        {
            return "SELECT * FROM Invoices WHERE TotalCost = " + invoiceCost;
        }

        /// <summary>
        /// SQL to retrieve invoices with a specific total cost and date
        /// </summary>
        /// <param name="invoiceCost">The invoice total cost</param>
        /// <param name="invoiceDate">The date of the invoice</param>
        /// <returns>string containing sql to retrieve invoices with a provided cost and date</returns>
        public string GetInvoicesSQL(int invoiceCost, string invoiceDate)
        {
            return "SELECT * FROM Invoices WHERE TotalCost = " + invoiceCost +
                   " AND InvoiceDate = #" + invoiceDate + "#";
        }

        /// <summary>
        /// SQL to retrieve invoices with a specific date
        /// </summary>
        /// <param name="invoiceDate">The date of the invoice</param>
        /// <returns>atring containing sql to retrieve invoices with a provided date</returns>
        public string GetInvoicesByDateSQL(string invoiceDate)
        {
            return "SELECT * FROM Invoices WHERE InvoiceDate = #" + invoiceDate + "#";
        }

        /// <summary>
        /// SQL to retrieve distinct invoice numbers
        /// </summary>
        /// <param name="invoiceNum">The invoice number of the specified invoice</param>
        /// <returns>string sql to retrieve distinct invoice numbers order by invoice number</returns>
        public string GetInvoiceNumbersSQL(string invoiceNum)
        {
            return "SELECT DISTINCT(" + invoiceNum + ") FROM Invoices ORDER BY " + invoiceNum;
        }

        /// <summary>
        /// SQL to retrieve distinct invoice dates
        /// </summary>
        /// <param name="invoiceDate">The date of the invoice</param>
        /// <returns>string sql to retrieve distinct invoice dates order by invoice date</returns>
        public string GetInvoiceDatesSQL(string invoiceDate)
        {
            return "SELECT DISTINCT(" + invoiceDate + ") FROM Invoices ORDER BY " + invoiceDate;
        }

        /// <summary>
        /// SQL to retrieve distinct invoice total costs
        /// </summary>
        /// <param name="invoiceCost">The invoice total cost</param>
        /// <returns>string sql to retrieve distinct invoice total costs order by invoice total costs</returns>
        public string GetInvoiceCosts(int invoiceCost)
        {
            return "SELECT DISTINCT(" + invoiceCost + ") FROM Invoices ORDER BY " + invoiceCost;
        }
    }
}
