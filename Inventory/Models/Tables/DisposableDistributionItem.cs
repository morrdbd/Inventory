using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class DisposableDistributionItem
    {
        public int ID { get; set; }

        public int DisposableDistributionID { get; set; }

        public int Quantity { get; set; }

        public int StockInItemID { get; set; } 

    }
}