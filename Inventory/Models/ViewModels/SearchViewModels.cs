using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class TicketSearch
    {
        public int? TicketNumber { get; set; }
        public int? RequestNumber { get; set; }
        public int EmployeeID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
    public class ItemInUseSearch
    {
        public int? TicketNumber { get; set; }
        public int BranchID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
    public class Durable_StockIn_Search
    {
        public string ReportNumber { get; set; }
        public string RequestNumber { get; set; }
        public string StockInDateFrom { get; set; }
        public string StockInDateTo { get; set; }
        public string RequestDateFrom { get; set; }
        public string RequestDateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

    public class Item_In_Warehouse_Search
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
    public class Employee_Search
    {
        public string sName { get; set; }
        public string sFatherName { get; set; }
        public int? sDepartmentID { get; set; }
        public string sOccupation { get; set; }
        //public string DateFrom { get; set; }
        //public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
    public class ProductSearch
    {
        public int? UsageTypeID { get; set; }

        public int? GroupID { get; set; }

        public int? CategoryID { get; set; }

        public string ProductName { get; set; }

        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
}