using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class ExpiredItem
    {
        public int ID { get; set; }

        public int ExpiredID { get; set; }

        public int ItemID { get; set; } 

    }
}