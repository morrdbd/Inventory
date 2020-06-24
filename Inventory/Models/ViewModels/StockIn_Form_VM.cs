using Inventory.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class StockIn_Form_VM
    {
        public int StockInID { get; set; }

        public string M7Number { get; set; }

        public string StockInDateForm { get; set; }

        public string OrderNumber { get; set; }

        public string OrderDateForm { get; set; }

        public string DeliveryPlace { get; set; }

        public string ContractorName { get; set; }

        public string Details { get; set; }

        public StockInItemVM[] StockInItems { get; set; }
    }
}