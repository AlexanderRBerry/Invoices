using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Main
{
    public class clsMainSQL
    {
        /// <summary>
        /// SQL to update cost of invoice matching an invoice number
        /// </summary>
        /// <param name="invoiceNum">The invoice number of the specified invoice</param>
        /// <returns>string containing SQL to update cost of invoices with a provided invoice number</returns>
        public string UpdateInvoicesSQL(int cost, int invoiceNum)
        {
            return "UPDATE Invoices SET TotalCost = " + cost + "WHERE InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// SQL to insert into LineItems
        /// </summary>
        /// <returns>string containing SQL to insert values into LineItems</returns>
        public string InsertItemsSQL(int invoiceNum, int lineItemNum, string itemCode)
        {
            return "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values(" + invoiceNum + ", " + lineItemNum + ", '" + itemCode + "')";
        }

        /// <summary>
        /// SQL to insert into Invoices
        /// </summary>
        /// <returns>string containing SQL to insert values into Invoices</returns>
        public string InsertInvoiceSQL(string date, int cost)
        {
            return "INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#" + date + "#, " + cost + ")";
        }

        /// <summary>
        /// SQL to Get Invoices matching an invoice number
        /// </summary>
        /// <returns>string containing SQL to get invoices matching an invoice number</returns>
        public string GetInvoiceSQL(int invoiceNum)
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// SQL to Get an Item
        /// </summary>
        /// <returns>string containing SQL to Get ItemCode, ItemDesc, Cost</returns>
        public string GetItemSQL(int cost, int invoiceNum)
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }

        /// <summary>
        /// SQL to Get LineItems
        /// </summary>
        /// <returns>string containing SQL to get item code, and cost</returns>
        public string GetLineItemsSQL(int invoiceNum)
        {
            return "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc WHERE LineItems.ItemCode = ItemDesc.ItemCode AND LineItems.InvoiceNum = " + invoiceNum;
        }
    }
}
