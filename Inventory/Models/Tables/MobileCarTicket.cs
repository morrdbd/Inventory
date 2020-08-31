using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class MobileCarTicket
    {
        [Key]
        public int MobileCarTicketID { get; set; }

        public int DepartmentID { get; set; }

        public string PersAssignedName { get; set; }

        public string PersAssignedOccupation { get; set; }

        [NotMapped]
        public string VisitingDateForm { get; set; }

        public DateTime? VisitingDate { get; set; }

        public string VisitingPurpose { get; set; }

        public string VisitingPlace { get; set; }

        public string VisistingTime { get; set; }

        public bool IsActive { get; set; }

        public int InsertedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InsertedDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdatedDate { get; set; }

    }
}