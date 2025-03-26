using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Common
{
    /// <summary>
    /// Data structure for an invoice item
    /// </summary>
    public class clsItem
    {
        /// <summary>
        /// A single char between A-Z, functions as an ID for the item
        /// </summary>
        private string sItemCode;

        /// <summary>
        /// A short description of the item
        /// </summary>
        private string sItemDescription;

        /// <summary>
        /// The cost of the item
        /// </summary>
        private string sCost;

        /// <summary>
        /// Getter/Setter for sItemCode
        /// </summary>
        public string itemCode
        {
            get { return sItemCode; }
            set { sItemCode = value; }
        }

        /// <summary>
        /// Getter/Setter for sItemDescription
        /// </summary>
        public string itemDescription
        {
            get { return sItemDescription; }
            set { sItemDescription = value; }
        }

        /// <summary>
        /// Getter/Setter for sCost
        /// </summary>
        public string cost
        {
            get { return sCost; }
            set { sCost = value; }
        }
    }
}
