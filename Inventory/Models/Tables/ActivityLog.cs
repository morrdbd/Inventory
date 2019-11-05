using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOMoRR.Models.Tables
{
    public class ActivityLog
    {
        [Key]
        public int LogId { get; set; }
        public string ModifiedTable { get; set; }
        public long ModfiedId { get; set; }
        public string Action { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedTime { get; set; }
        public string ModifiedData { get; set; }
    }
}