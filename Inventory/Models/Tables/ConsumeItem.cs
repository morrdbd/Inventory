using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class ConsumeItem
    {
        public int ConsumeItemID { get; set; }

        public int ConsumeID { get; set; }

        public int DistributionItemID { get; set; }

        public int Quantity { get; set; }

        public int StockInItemID { get; set; } 

    }
}