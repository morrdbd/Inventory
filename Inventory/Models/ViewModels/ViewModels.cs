using Inventory.Attributes;
using Inventory.Models.Procedures;
using Inventory.Models.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class Ticket_Form_VM
    {
        public int TicketID { get; set; }

        public int? TicketNumber { get; set; }

        public string TicketIssuedDateVM { get; set; }

        [StringLength(500)]
        public string Warehouse { get; set; }

        public int? RequestNumber { get; set; }

        public string RequestDateVM { get; set; }

        public int BranchID { get; set; }

        public string Details { get; set; }

        public TicketItem[] TicketItems { get; set; }
    }

    public class Receipt_Report_Form_VM
    {
        public int ReceiptReportID { get; set; }

        public int? ReportNumber { get; set; }

        [StringLength(200)]
        public string Organization { get; set; }

        public string ReceiptDateVM { get; set; }

        public int? SuggBillNumber { get; set; }

        public string Mem3DateVM { get; set; }

        [StringLength(500)]
        public string DeliveryPlace { get; set; }

        [StringLength(500)]
        public string ObtainedFromContractor { get; set; }

        public string Details { get; set; }

        public ReceiptReportItem[] ReceiptReportItems { get; set; }
    }

    public class TicketSearch
    {
        public int? TicketNumber { get; set; }
        public int? RequestNumber { get; set; }
        public int BranchID { get; set; }
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
    public class Receipt_Report_Search
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
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
        public int sBranchID { get; set; }
        //public string DateFrom { get; set; }
        //public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
  

    public class chartData
    {
        public int Month { get; set; }
        public int RecPerMonth { get; set; }
    }
}