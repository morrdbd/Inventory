using Inventory.HelperCode;
using Inventory.Models.Procedures;
using Inventory.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class StockInVM
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

        public List<StockInItemVM> StockInItems { get; set; }

    }
}