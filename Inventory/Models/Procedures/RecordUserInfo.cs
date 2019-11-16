using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.Procedures
{
    public class RecordUserInfo
    {
        public int InsertedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public string InsertUser { get; set; }
        public string InsertName { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateName { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}