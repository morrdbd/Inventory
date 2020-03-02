﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class DistributionItem
    {
        public int ID { get; set; }

        public int DistributionID { get; set; }

        public int Quantity { get; set; }

        public int ProductCode { get; set; } 

        public int UnitPrice { get; set; }

        public string DealWithAccount { get; set; }

        public bool IsActive { get; set; }

        public int InsertedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InsertedDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdatedDate { get; set; }
    }
}