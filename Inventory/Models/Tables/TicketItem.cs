using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class TicketItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int UnitID { get; set; }
        public string Details { get; set; }
        public int UnitPrice { get; set; }
        public string DealWithAccount { get; set; }
    }
}