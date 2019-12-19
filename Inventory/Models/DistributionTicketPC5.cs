namespace DOMoRR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DistributionTicketPC5
    {
        public int Id { get; set; }

        public int? TicketNumber { get; set; }

        public DateTime? DistributionDate { get; set; }

        [StringLength(50)]
        public string Distributor { get; set; }

        public int? RequestNumber { get; set; }

        public DateTime? RequestingDate { get; set; }

        [StringLength(250)]
        public string RequestingBranch { get; set; }

        [StringLength(50)]
        public string Quantity { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        [StringLength(250)]
        public string Details { get; set; }

        public int? UnitPrice { get; set; }

        [StringLength(50)]
        public string Rate { get; set; }

        public bool IsActive { get; set; }

        public int InsertedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InsertedDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdatedDate { get; set; }
    }
}
