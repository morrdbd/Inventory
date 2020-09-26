using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class MobileCarTicket_VM
    {
        public int MobileCarTicketID { get; set; }

        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string PersAssignedName { get; set; }

        public string PersAssignedOccupation { get; set; }

        public string PhoneNumber { get; set; }

        public string VisitingDateTime { get; set; }

        public string VisitingPurpose { get; set; }

        public string VisitingPlace { get; set; }

        public bool? IsApproved { get; set; }

        public string RejectionReason { get; set; }

        public int? MobileCarID { get; set; }

        public string NumberPlate { get; set; }

        public string CarType { get; set; }

        public string DriverName { get; set; }

        public string DriverPhoneNo { get; set; }

        public double? Startkm { get; set; }

        public double? Endkm { get; set; }

        public bool IsActive { get; set; }

        public int InsertedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InsertedDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdatedDate { get; set; }
    }
}