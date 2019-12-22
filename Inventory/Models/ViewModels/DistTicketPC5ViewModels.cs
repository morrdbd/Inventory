using Inventory.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class DistTicketPC5ViewModels
    {
        public int Id { get; set; }

        public int? TicketNumber { get; set; }

        public DateTime? TicketIssuedDate { get; set; }

        [StringLength(500)]
        public string Warehouse { get; set; }

        public int? RequestNumber { get; set; }

        public DateTime? RequestingDate { get; set; }

        [StringLength(500)]
        public string Requester { get; set; }

        [StringLength(250)]
        public string Details { get; set; }

        public List<TicketItem> TicketItems { get; set; }

    }
}