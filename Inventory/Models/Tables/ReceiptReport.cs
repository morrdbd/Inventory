using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Inventory.Models
{
    public class ReceiptReport
    {
        public int ReceiptReportID { get; set; }

        public int? ReportNumber { get; set; }

        [StringLength(200)]
        public string Organization { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReceiptDate { get; set; }

        public int? OrderNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Mem3DateVM { get; set; }

        [StringLength(500)]
        public string SubmissionPlace { get; set; }

        [StringLength(500)]
        public string FromContract { get; set; }

        public string Details { get; set; }

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
