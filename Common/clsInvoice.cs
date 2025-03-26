using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Common
{
    /// <summary>
    /// Data structure for an invoice
    /// </summary>
    public class clsInvoice
    {
        /// <summary>
        /// Invoice ID
        /// </summary>
        private string sInvoiceID;

        /// <summary>
        /// Invoice data mm/dd/yyyy
        /// </summary>
        private string sInvoiceDate;

        /// <summary>
        /// Total cost on invoice
        /// </summary>
        private string sTotalCost;

        /// <summary>
        /// Getter/Setter for sInvoiceID
        /// </summary>
        public string invoiceID
        {
            get { return sInvoiceID; }
            set { sInvoiceID = value; }
        }

        /// <summary>
        /// Getter/Setter for sInvoiceDate
        /// </summary>
        public string InvoiceDate
        {
            get { return sInvoiceDate; }
            set { sInvoiceDate = value; }
        }

        /// <summary>
        /// Getter/Setter for sTotalCost
        /// </summary>
        public string TotalCost
        {
            get { return sTotalCost; }
            set { sTotalCost = value; }
        }
    }
}
