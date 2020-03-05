using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Inventory.Models
{
    public class StockIn
    {
        [Key]
        public int StockInID { get; set; }

        public string M7Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StockInDate { get; set; }

        public string OrderNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OrderDate { get; set; }

        [StringLength(500)]
        public string DeliveryPlace { get; set; }

        [StringLength(500)]
        public string ContractorName { get; set; }

        public string Details { get; set; }

        [StringLength(300)]
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
