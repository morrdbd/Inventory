using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(300)]
        public string FatherName { get; set; }

        public int DepartmentID { get; set; }

        [StringLength(300)]
        public string Occupation { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public int InsertedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InsertedDate { get; set; }
        [NotMapped]
        public string InsertedDateForm { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdatedDate { get; set; }

    }
}