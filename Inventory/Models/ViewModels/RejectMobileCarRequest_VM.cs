using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class RejectMobileCarRequest_VM
    {
        [Required]
        public int MobileCarTicketID { get; set; }

        [Required]
        public string RejectionReason { get; set; }

        public string VisitingPlace { get; set; }
    }
}