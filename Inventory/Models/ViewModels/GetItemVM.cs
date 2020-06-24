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
        public int ItemID { get; set; }

        [Required]
        public int Quantity { get; set; }

    }
}