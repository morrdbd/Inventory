using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        [Required]
        public string EnName { get; set; }
        [Required]
        public string DrName { get; set; }
        [Required]
        public string PaName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public int InsertedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InsertedDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdatedDate { get; set; }
    }
}