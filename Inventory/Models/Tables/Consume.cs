using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Inventory.Models
{
    public class Consume
    {
        [Key]
        public int ConsumeID { get; set; }

        public int? DocumentIssueNumber { get; set; }

        public DateTime? DocumentIssuedDate { get; set; }

        public int? OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public int EmployeeID { get; set; }

        public int DepartmentID { get; set; }

        public string Details { get; set; }

        public string FilePath { get; set; }

        public bool IsActive { get; set; }

        public int InsertedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InsertedDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdatedDate { get; set; }
    }
}
