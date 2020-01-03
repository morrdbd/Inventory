using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class Item
    {
        public int ItemID { get; set; }

        [StringLength(100)]
        public string ItemCode { get; set; }

        [StringLength(300)]
        public string EnName { get; set; }

        [StringLength(300)]
        public string DrName { get; set; }

        [StringLength(300)]
        public string PaName { get; set; }

        public bool IsActive { get; set; }
    }
}