using DOMoRR.HelperCode;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        public int UsageTypeID { get; set; }

        public int GroupID { get; set; }

        public int? CategoryID { get; set; }

        public int? UnitID { get; set; }

        public int? PackingID { get; set; }

        public int? SizeID { get; set; }

        [StringLength(100)]
        public string SerialCodeNumber { get; set; }

        [StringLength(300)]
        public string ProductName { get; set; }

        public int? OriginID { get; set; }

        public int? BrandID { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public int ProductCode { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        [AllowFileSize(ErrorMessage = "سایز فایل ضمیمه باید کمتر از 100 ام بی باشد")]
        public HttpPostedFileWrapper FileContent { get; set; }

        public bool IsActive { get; set; }

        public int InsertedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InsertedDate { get; set; }
        [NotMapped]
        public string InsertedDateForm { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdatedDate { get; set; }
    }
}