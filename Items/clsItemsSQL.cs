using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Items
{
    internal class clsItemsSQL
    {
        
        /// <summary>
        /// SQL for getting item description table using parameters
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDesc"></param>
        /// <param name="itemCost"></param>
        /// <returns></returns>
        public string GetItemDescTableSQL(string itemCode, string itemDesc, double itemCost)
        {
            return "SELECT '" + itemCode + "', " + itemDesc + ", " + itemCost + "FROM ItemDesc"; //should I make the table a string and pass it in as well?
        }

        /// <summary>
        /// SQL for getting ItemCode, ItemDesc, and Cost from ItemDesc table
        /// </summary>
        /// <returns></returns>
        public string GetItemDescTableSQL()
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }

        /// <summary>
        /// SQL for getting unique invoice numbers from the table LineItems using item code
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string GetUniqueInvoiceNums(string itemCode)
        {
            return "SELECT DISTINCT InvoiceNum FROM LineItems WHERE ItemCode =  '" + itemCode + "'";
        }

        /// <summary>
        /// SQL for updating description and cost from the table ItemDesc using item code
        /// </summary>
        /// <param name="itemDesc"></param>
        /// <param name="itemCost"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string UpdateDescCost(string itemDesc, double itemCost, string itemCode)
        {
            return "UPDATE ItemDesc SET ItemDesc = '" + itemDesc + "', Cost = '" + itemCost + "' WHERE ItemCode = '" + itemCode + "'";
        }

        /// <summary>
        /// SQL for inserting itemCode, itemDesc, and cost into the ItemDesc Table
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDesc"></param>
        /// <param name="itemCost"></param>
        /// <returns></returns>
        public string InsertCodeDescCost(string itemCode, string itemDesc, double itemCost)
        {
            return "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES ('" + itemCode + "', '" + itemDesc + "', '" + itemCost + "')";
        }

        /// <summary>
        /// SQL for deleting item from table ItemDesc using itemCode
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string DeleteItem(string itemCode)
        {
            return "DELETE FROM ItemDesc WHERE ItemCode = '" + itemCode + "'";
        }

    }
}


