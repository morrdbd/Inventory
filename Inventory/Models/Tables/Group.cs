using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        [Required]
        public string EnName { get; set; }

        [Required]
        public string DrName { get; set; }

        [Required]
        public string PaName { get; set; }

        [Required]
        public int UsageTypeId { get; set; }

        public bool IsActive { get; set; }
    }
}