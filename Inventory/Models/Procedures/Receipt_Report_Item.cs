using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.Procedures
{
    public class StockIn_Item
    {
        public int ID { get; set; }
        public int StockInID { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }
        public string Remarks { get; set; }
        public string UnitName { get; set; }
        public int UnitID { get; set; }
    }
}