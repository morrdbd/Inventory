using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class PrintLabelVM
    {
        public string BarCode { get; set; }
        public string GroupName { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
    }
}