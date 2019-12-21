using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Inventory.Models
{
    public class ReceiptReportMem7
    {
        public int Id { get; set; }

        public int? ReportNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReceiptDate { get; set; }

        public int? OrderNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DistributionDate { get; set; }

        [StringLength(50)]
        public string SubmissionPlace { get; set; }

        [StringLength(50)]
        public string FromContract { get; set; }

        [StringLength(50)]
        public string Quantity { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        [StringLength(250)]
        public string Details { get; set; }

        public int? UnitPrice { get; set; }

        public int? Amount { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }
    }
}
