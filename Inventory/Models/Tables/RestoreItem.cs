using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class RestoreItem
    {
        [Key]
        public int ID { get; set; }

        public int RestoreID { get; set; }

        public int ItemID { get; set; } 

    }
}