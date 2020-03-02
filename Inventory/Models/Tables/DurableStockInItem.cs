using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class DurableStockInItem
    {
        public int ID { get; set; }

        public int StockInID { get; set; }

        public int ProductCode { get; set; }

        public int Price { get; set; }

        public int GroupID { get; set; }

        [NotMapped]
        public string GroupName { get; set; }

        public int CategoryID { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }

        public string ProductName { get; set; }

        public int UnitID { get; set; }

        [NotMapped]
        public string UnitName { get; set; }

        public int? SizeID { get; set; }

        [NotMapped]
        public string SizeName { get; set; }

        public int? OriginID { get; set; }

        [NotMapped]
        public string OriginName { get; set; }

        public int? BrandID { get; set; }

        [NotMapped]
        public string BrandName { get; set; }

        public string Remarks { get; set; }

        public bool IsActive { get; set; }

        public int InsertedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InsertedDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdatedDate { get; set; }
    }
}