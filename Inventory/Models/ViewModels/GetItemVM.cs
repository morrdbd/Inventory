using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class GetItemVM
    {
        [Required]
        public int StockInItemID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string DealWithAccount { get; set; }

    }
}