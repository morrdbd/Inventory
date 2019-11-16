using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class LookupValue
    {
        [Key]
        public int ValueId { get; set; }
        [Required]
        public string LookupCode { get; set; }
        [Required]
        public string ValueCode { get; set; }
        [Required]
        public string EnName { get; set; }
        [Required]
        public string DrName { get; set; }
        [Required]
        public string PaName { get; set; }
        public string ForOrdering { get; set; }
        public bool IsActive { get; set; }
    }
}