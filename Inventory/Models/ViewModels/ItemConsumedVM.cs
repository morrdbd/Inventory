using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class ItemConsumedVM
    {
        [Required]
        public int DistributionItemID { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}