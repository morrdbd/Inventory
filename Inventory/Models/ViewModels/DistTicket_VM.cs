using Inventory.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class DistTicket_VM
    {
        public DistributionTicketPC5 Ticket { get; set; }

        public IEnumerable<TicketItem> TicketItemData { get; set; }
    }
}