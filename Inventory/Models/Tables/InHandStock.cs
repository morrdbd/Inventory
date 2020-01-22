using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class InHandStock
    {
        public int ID { get; set; }
        public int ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}