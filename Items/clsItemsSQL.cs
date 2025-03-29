using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Items
{
    internal class clsItemsSQL
    {

        /**
         * 
         * Items Window
- select ItemCode, ItemDesc, Cost from ItemDesc
- select distinct(InvoiceNum) from LineItems where ItemCode = 'A'
- Update ItemDesc Set ItemDesc = 'abcdef', Cost = 123 where ItemCode = 'A'
- Insert into ItemDesc (ItemCode, ItemDesc, Cost) Values ('ABC', 'blah', 321)
- Delete from ItemDesc Where ItemCode = 'ABC'
         * 
         */

        //tables
            //tableName
                //column1, column2....

            //Invoices
            //ItemDesc
                //ItemCode, ItemDesc, Cost
            //LineItems
                //InvoiceNum, LineItemNum?, ItemCode


        

        //----------------------------------------------Check for ' and , around itemCode (and desc maybe??) if problems, very hard to see----------------------------//

        
        /// ???? comments not working for me for this only?
        public string GetItemDescTableSQL(string itemCode, string itemDesc, double itemCost)
        {
            return "SELECT '" + itemCode + "', " + itemDesc + ", " + itemCost + "FROM ItemDesc"; //should I make the table a string and pass it in as well?
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
            return "UPDATE ItemDesc SET ItemDesc = '" + itemDesc + "', Cost = " + itemCost + " WHERE ItemCode = '" + itemCode + "'";
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
            return "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES ('" + itemCode + "', " + itemDesc + ", " + itemCost + ")";
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


//----------------------------------------------------------------SQL HELP/EXAMPLE----------------------------------------------------------\\

/*
 * 
 * Based on which database you use, here are the connection strings:

Invoice.mdb:
sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";

Invoice.accdb:
sConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Directory.GetCurrentDirectory() + "\\Invoice.accdb";

I have created a database that you should be able to use for your group project.  You do not have to use this database, but it will work just fine.

Invoices table
This table holds the Invoice number, Invoice date, and Total cost.

ItemDesc table
This table holds the Item Code, which is really the name of the item.  
I am using it as a code because it is helpful to the user to be able to find the item quickly.  
So if you put all these items in a drop down box, all the user would have to do is type in one letter and the correct item would appear.  
This table also holds the item’s description and the item’s default cost.

LineItems Table
This table holds all the items that have been put on the invoices.  
It has the Invoice Number, the Line Item Number which corresponds to the position the item is in the datagrid, the Item code, and the Cost for that item. 
You also need the cost for each item here because remember the user has the ability to change the cost.



SQL Help
Here are some SQL statements to help refresh your brain:

Items Window
- select ItemCode, ItemDesc, Cost from ItemDesc
- select distinct(InvoiceNum) from LineItems where ItemCode = 'A'
- Update ItemDesc Set ItemDesc = 'abcdef', Cost = 123 where ItemCode = 'A'
- Insert into ItemDesc (ItemCode, ItemDesc, Cost) Values ('ABC', 'blah', 321)
- Delete from ItemDesc Where ItemCode = 'ABC'

Main Window
- UPDATE Invoices SET TotalCost = 1200 WHERE InvoiceNum = 123
- INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values (123, 1, 'AA')
- INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#4/13/2018#, 100)
- SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = 123
- select ItemCode, ItemDesc, Cost from ItemDesc
- SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = 5000
- DELETE FROM LineItems WHERE InvoiceNum = 5000

Search Window
- SELECT * FROM Invoices
- SELECT * FROM Invoices WHERE InvoiceNum = 5000
- SELECT * FROM Invoices WHERE InvoiceNum = 5000 AND InvoiceDate = #4/13/2018#
- SELECT * FROM Invoices WHERE InvoiceNum = 5000 AND InvoiceDate = #4/13/2018# AND TotalCost = 120
- SELECT * FROM Invoices WHERE TotalCost = 1200
- SELECT * FROM Invoices WHERE TotalCost = 1300 and InvoiceDate = #4/13/2018# 
- SELECT * FROM Invoices WHERE InvoiceDate = #4/13/2018#
- SELECT DISTINCT(InvoiceNum) From Invoices order by InvoiceNum
- SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate
- SELECT DISTINCT(TotalCost) From Invoices order by TotalCost

In order to deal with dates in access make sure to put # signs around the date.

Note:  If a column is an auto increment you do not need to insert the value.  
For instance, if you are inserting a record into the Invoices table, you do not need to include the InvoiceNum field in your insert statement.

In the database that I gave you called "Invoice.mdb" there is a table called "Invoices" that has a column called "InvoiceNum" that is an "AutoNumber".  
What this means is that when you insert a record into this table, 
you don't need to give it the InvoiceNum, just the values for the other fields, and it will create the InvoiceNum for you. 

So what you do is execute a statement like:
   INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#4/8/2016#, 100);
Then you need to query back out the InvoiceNum by executing the statement:
   SELECT MAX(InvoiceNum) FROM Invoices

 * 
 */
