using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class ReusableDistributionItem
    {
        public int ID { get; set; }

        public int ReusableDistributionID { get; set; }

        public int StockInItemID { get; set; }

        public int Quantity { get; set; }

        public string DealWithAccount { get; set; }

        public bool IsReturned { get; set; }

        public DateTime? ReturnDate { get; set; }

    }
}