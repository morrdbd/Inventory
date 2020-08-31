using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class RestoredItemVM
    {
        [Required]
        public int DistributionItemID { get; set; }

        [Required]
        public string ItemInCondition { get; set; }
    }
}