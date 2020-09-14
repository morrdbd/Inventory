using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class ApproveAndAssignCar
    {

        [Required]
        public int MobileCarTicketID { get; set; }

        [Required]
        public int? MobileCarID { get; set; }
    }
}