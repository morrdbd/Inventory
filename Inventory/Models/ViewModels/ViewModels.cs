using DOMoRR.Attributes;
using DOMoRR.Models.Procedures;
using DOMoRR.Models.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace DOMoRR.Models.ViewModels
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

    public class ClientSearch
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string OriginProvince { get; set; }
        public string CurrentProvince { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
    public class MinisterVisitorSearch
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MeetingOrganizer { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
    public class RequisitionSearch
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string DocumentTypeCodeSearch { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
    public class SuggestAndOrderSearch
    {
        public string DocumentTypeSearch { get; set; }
        public int SerialNumber { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
    public class ArchiveSearch
    {
        public string ArchiveNumberS { get; set; }
        public string SendToS { get; set; }
        public string SenderS { get; set; }
        public string SummaryS { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int PageSize { get; set; }
        public int? PageNo { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
    public class SecretairatSearch
    {
        public string DocumentNumberS { get; set; }
        public string SendToS { get; set; }
        public string SenderS { get; set; }
        public string SummaryS { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
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