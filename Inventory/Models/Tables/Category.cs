using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string EnName { get; set; }

        [Required]
        public string DrName { get; set; }

        [Required]
        public string PaName { get; set; }

        [Required]
        public int GroupId { get; set; }

        public bool IsActive { get; set; }
    }
}