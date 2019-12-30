using Inventory.Models.Procedures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class ReceiptReportVM
    {
        public int ReceiptReportID { get; set; }

        public int? ReportNumber { get; set; }

        [StringLength(200)]
        public string Organization { get; set; }

        public string ReceiptDateVM { get; set; }

        public int? OrderNumber { get; set; }

        public string Mem3DateVM { get; set; }

        [StringLength(50)]
        public string SubmissionPlace { get; set; }

        [StringLength(50)]
        public string FromContract { get; set; }

        public string Details { get; set; }

        public List<Receipt_Report_Items> ReceiptReportItems { get; set; }

    }
}