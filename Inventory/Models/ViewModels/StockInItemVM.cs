using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class StockInItemVM
    {
        public int ID { get; set; }

        public int StockInID { get; set; }

        public string ProductName { get; set; }

        public string SizeName { get; set; }

        public int? SizeID { get; set; }

        public string CategoryName { get; set; }

        public int CategoryID { get; set; }

        public string GroupName { get; set; }

        public string ProductCode { get; set; }

        public int UsageTypeID { get; set; }

        public int GroupID { get; set; }

        public string OriginName { get; set; }

        public int? OriginID { get; set; }

        public string BrandName { get; set; }

        public int? BrandID { get; set; }

        public string UnitName { get; set; }

        public int UnitID { get; set; }

        public int Quantity { get; set; }

        public int? AvailableQuantity { get; set; }

        public int UnitPrice { get; set; }

        public string Remarks { get; set; }
    }
}