using Inventory.Attributes;
using Inventory.HelperCode;
using Inventory.Models.Procedures;
using Inventory.Models.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Models.ViewModels
{
    public class User_SC
    {
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
    }


    public class Province_SC
    {
        public string ProvinceName { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
    }

    public class Country_SC
    {
        public string CountryName { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
    }

    public class StockIn_Form_VM
    {
        public int StockInID { get; set; }

        public string M7Number { get; set; }
       
        public string StockInDateForm { get; set; }

        public string OrderNumber { get; set; }

        public string OrderDateForm { get; set; }

        [StringLength(500)]
        public string DeliveryPlace { get; set; }

        [StringLength(500)]
        public string ContractorName { get; set; }

        public string Details { get; set; }

        public StockInItemVM[] StockInItems { get; set; }
    }

    public class Distribution_Form_VM
    {
        public int DistributionID { get; set; }

        public int? TicketNumber { get; set; }

        public string TicketIssuedDateForm { get; set; }

        [StringLength(500)]
        public string Warehouse { get; set; }

        public int? RequestNumber { get; set; }

        public string RequestDateForm { get; set; }

        public int EmpID { get; set; }

        public string EmpName { get; set; }

        public string EmpFatherName { get; set; }

        public string EmpOccupation { get; set; }

        public int EmpDepartmentID { get; set; }

        public string EmpDepartmentName { get; set; }

        public string Details { get; set; }

        public DistributionItemVM[] DistributionItems { get; set; }
    }
    public class DistributionItemVM
    {
        public int ID { get; set; }

        public int ItemID { get; set; }

        public int DistributionID { get; set; }

        public string UnitName { get; set; }

        public int UsageTypeID { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string UsageTypeName { get; set; }

        public string ProductSizeName { get; set; }

        public string ProductCategoryName { get; set; }

        public string ProductGroupName { get; set; }

        public string ProductOriginName { get; set; }

        public string BrandName { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        public int TotalPrice { get; set; }

        public string DealWithAccount { get; set; }

    }

    public class chartData
    {
        public int Month { get; set; }
        public int RecPerMonth { get; set; }
    }
}