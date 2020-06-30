using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class LookupCategoryVM
    {
        public int ValueId { get; set; }

        [Required]
        public string ValueCode { get; set; }

        [Required]
        public string EnName { get; set; }

        [Required]
        public string DrName { get; set; }

        [Required]
        public string PaName { get; set; }

        public int UsageTypeId { get; set; }

        public int GroupId { get; set; }
    }
}