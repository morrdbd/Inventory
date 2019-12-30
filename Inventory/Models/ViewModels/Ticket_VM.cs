using Inventory.Models.Procedures;
using Inventory.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class Ticket_VM
    {
        public int TicketID { get; set; }

        public int? TicketNumber { get; set; }

        public string TicketIssuedDateVM { get; set; }

        [StringLength(500)]
        public string Warehouse { get; set; }

        public int? RequestNumber { get; set; }

        public string RequestDateVM { get; set; }

        [StringLength(500)]
        public string Requester { get; set; }

        public string Details { get; set; }

        public List<Ticket_Items> TicketItems { get; set; }
    }
    

}