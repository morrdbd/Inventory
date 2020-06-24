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

    public class chartData
    {
        public int Month { get; set; }
        public int RecPerMonth { get; set; }
    }
}