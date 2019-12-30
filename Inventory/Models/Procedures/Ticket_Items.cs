using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.Procedures
{
    public class Ticket_Items
    {
        public int ID { get; set; }
        public int TicketID { get; set; }
        public int Quantity { get; set; }
        public string ItemDetails { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }
        public string DealWithAccount { get; set; }
        public string UnitName { get; set; }
        public int UnitID { get; set; }
    }
}