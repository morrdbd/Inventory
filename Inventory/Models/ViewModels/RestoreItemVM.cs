using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class RestoreItemVM
    {
        public int ID { get; set; }

        public int RestoreID { get; set; }

        public int ItemID { get; set; }

        public string UnitName { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SizeName { get; set; }

        public string CategoryName { get; set; }

        public string GroupName { get; set; }

        public string OriginName { get; set; }

        public string BrandName { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        public string DealWithAccount { get; set; }
    }
}