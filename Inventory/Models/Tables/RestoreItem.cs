using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class RestoreItem
    {
        [Key]
        public int RestoreItemID { get; set; }

        [Required]
        public int RestoreID { get; set; }

        [Required]
        public int StockInItemID { get; set; }

        [Required]
        public int DistributionItemID { get; set; }

        [Required]
        public string ItemInCondition { get; set; }

    }
}