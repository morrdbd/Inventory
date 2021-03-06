﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class TicketSearch
    {
        public int? TicketNumber { get; set; }
        public string Warehouse { get; set; }
        public int? RequestNumber { get; set; }
        public int? DepartmentID { get; set; }
        public int? EmployeeID { get; set; }
        public string IssuedDateFrom { get; set; }
        public string IssuedDateTo { get; set; }
        public string RequestDateFrom { get; set; }
        public string RequestDateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

    public class DistributedItemsSearch
    {
        public int? DepartmentID { get; set; }

        public int? EmployeeID { get; set; }

        public int? UsageTypeID { get; set; }

        public string BarCode { get; set; }

        public int? UnitID { get; set; }

        public int? GroupID { get; set; }

        public int? CategoryID { get; set; }

        public string ItemName { get; set; }

        public string ItemCode { get; set; }

        public int? SizeID { get; set; }

        public int? OriginID { get; set; }

        public int? BrandID { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

    public class ConsumedSearch
    {
        public int? DepartmentID { get; set; }

        public int? EmployeeID { get; set; }

        public int? GroupID { get; set; }

        public int? CategoryID { get; set; }

        public string BarCode { get; set; }

        public string ItemName { get; set; }

        public string ItemCode { get; set; }

        public int? SizeID { get; set; }

        public int? OriginID { get; set; }

        public int? BrandID { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

    public class StockIn_Form_Search
    {
        public string ReportNumber { get; set; }
        public string RequestNumber { get; set; }
        public string StockInDateFrom { get; set; }
        public string StockInDateTo { get; set; }
        public string OrderDateFrom { get; set; }
        public string OrderDateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

    public class StockIn_Search
    {
        public string BarCode { get; set; }

        public string ReportNumber { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public string ItemInCondition { get; set; }

        public int? UsageTypeID { get; set; }

        public int? GroupID { get; set; }

        public int? CategoryID { get; set; }

        public string ItemName { get; set; }

        public string ItemCode { get; set; }

        public int? SizeID { get; set; }

        public int? OriginID { get; set; }

        public int? BrandID { get; set; }

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

    public class Item_Search
    {
        public int? UsageTypeID { get; set; }

        public int? GroupID { get; set; }

        public int? CategoryID { get; set; }

        public string BarCode { get; set; }

        public string ItemName { get; set; }

        public string ItemCode { get; set; }

        public int? UnitID { get; set; }

        public int? SizeID { get; set; }

        public int? OriginID { get; set; }

        public int? BrandID { get; set; }

        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

    public class RestoreItemSearch
    {
        public int? EmployeeID { get; set; }

        public int? GroupID { get; set; }

        public int? CategoryID { get; set; }

        public string ItemName { get; set; }

        public string BarCode { get; set; }

        public string ItemCode { get; set; }

        public int? UnitID { get; set; }

        public int? SizeID { get; set; }

        public int? OriginID { get; set; }

        public int? BrandID { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

    public class ItemsRestoredSearch
    {
        public int? DocumentIssuedNo { get; set; }

        public int? DepartmentID { get; set; }

        public int? EmployeeID { get; set; }

        public string ItemInCondition { get; set; }

        public string BarCode { get; set; }

        public int? GroupID { get; set; }

        public int? CategoryID { get; set; }

        public string ItemName { get; set; }

        public string ItemCode { get; set; }

        public int? UnitID { get; set; }

        public int? SizeID { get; set; }

        public int? OriginID { get; set; }

        public int? BrandID { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
    public class ConsumeItemsSearch
    {
        public int? DepartmentID { get; set; }

        public int? GroupID { get; set; }

        public int? CategoryID { get; set; }

        public string BarCode { get; set; }

        public string ItemName { get; set; }

        public string ItemCode { get; set; }

        public int? SizeID { get; set; }

        public int? OriginID { get; set; }

        public int? BrandID { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
    public class LookupGroupSearch
    {
        public int? LookupUsageType { get; set; }
        public string LookupName { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

    public class LookupCategorySearch
    {
        public int? SLookupUsageType { get; set; }
        public int? SLookupGroupType { get; set; }
        public string SLookupName { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

    public class DepartmentSearch
    {
        public int? ParentDepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

    public class MobileCarTicket_Search
    {
        public int? sMobileCarTicketID { get; set; }
        public int? sDepartmentID { get; set; }
        public int? sMobileCarID { get; set; }
        public string sPersAssignedName { get; set; }
        public string sPersAssignedOccupation { get; set; }
        public string sVisitingPurpose { get; set; }
        public string sVisitingPlace { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

    public class MobileCar_Search
    {
        public string sCarType { get; set; }
        public string sNumberPlate { get; set; }
        public string sDriverName { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }

}