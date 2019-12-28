using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Inventory.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }

        public int? TicketNumber { get; set; }

        public DateTime? TicketIssuedDate { get; set; }

        [NotMapped]
        public string TicketIssuedDateForm { get; set; }

        [StringLength(500)]
        public string Warehouse { get; set; }

        public int? RequestNumber { get; set; }

        public DateTime? RequestDate { get; set; }

        [NotMapped]
        public string RequestDateForm { get; set; }

        [StringLength(500)]
        public string Requester { get; set; }

        [StringLength(250)]
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
