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

        [Required]
        public int DepartmentID { get; set; }

        [Required]
        public string PersAssignedName { get; set; }

        [Required]
        public string PersAssignedOccupation { get; set; }

        public string PhoneNumber { get; set; }

        [NotMapped]
        public string VisitingDateTimeForm { get; set; }

        public DateTime? VisitingDateTime { get; set; }

        [Required]
        public string VisitingPurpose { get; set; }

        [Required]
        public string VisitingPlace { get; set; }

        public bool? IsApproved { get; set; }

        public string RejectionReason { get; set; }

        public int? MobileCarID { get; set; }

        public double? Startkm { get; set; }

        public double? Endkm { get; set; }

        public bool IsActive { get; set; }

        public int InsertedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime InsertedDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdatedDate { get; set; }

    }
}