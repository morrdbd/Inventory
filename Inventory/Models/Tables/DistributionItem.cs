using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class DistributionItem
    {
        public int DistributionItemID { get; set; }

        public int DistributionID { get; set; }

        public int Quantity { get; set; }

        public int QuantityConsumed { get; set; }

        public int StockInItemID { get; set; }

        public string DealWithAccount { get; set; }

        public bool IsReturned { get; set; }

        public DateTime? ReturnDate { get; set; }

    }
}