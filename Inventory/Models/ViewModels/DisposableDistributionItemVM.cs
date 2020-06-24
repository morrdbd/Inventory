using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class DisposableDistributionItemVM
    {
        public int ID { get; set; }

        public int ItemID { get; set; }

        public int DisposableDistributionID { get; set; }

        public string UnitName { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string ItemSizeName { get; set; }

        public string ItemCategoryName { get; set; }

        public string ItemGroupName { get; set; }

        public string ItemOriginName { get; set; }

        public string BrandName { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

    }
}