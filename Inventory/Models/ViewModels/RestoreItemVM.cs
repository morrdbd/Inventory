using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class RestoreItemVM
    {

        public int RestoreItemID { get; set; }

        public int DistributionItemID { get; set; }

        public int StockInItemID { get; set; }

        public string ItemInCondition { get; set; }

        public string BarCode { get; set; }

        public string UnitName { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string UsageTypeName { get; set; }

        public string ItemSizeName { get; set; }

        public string ItemCategoryName { get; set; }

        public string ItemGroupName { get; set; }

        public string ItemOriginName { get; set; }

        public string BrandName { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        public string DealWithAccount { get; set; }

        //public int? TicketNumber { get; set; }

        //public string TicketIssuedDateForm { get; set; }

        //public string Warehouse { get; set; }

        //public int? RequestNumber { get; set; }

        //public string RequestDateForm { get; set; }

        public string EmpName { get; set; }

        public string EmpFatherName { get; set; }

        public string EmpOccupation { get; set; }

        public string EmpDepartmentName { get; set; }

        //public string Details { get; set; }
    }
}