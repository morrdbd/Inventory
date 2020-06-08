using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class ExpiredItemVM
    {
        public int ID { get; set; }

        public int ExpiredID { get; set; }

        public int ItemID { get; set; }

        public string UnitName { get; set; }

        public int UsageTypeID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string UsageTypeName { get; set; }

        public string ItemSizeName { get; set; }

        public string ItemCategoryName { get; set; }

        public string ItemGroupName { get; set; }

        public string ItemOriginName { get; set; }

        public string BrandName { get; set; }

        public int Quantity { get; set; }
    }
}