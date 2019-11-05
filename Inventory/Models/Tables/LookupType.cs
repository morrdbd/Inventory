using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOMoRR.Models.Tables
{
    public class LookupType
    {
        [Key]
        public int LookupId { get; set; }
        public string LookupCode { get; set; }
        public string EnName { get; set; }
        public string DrName { get; set; }
        public string PaName { get; set; }
        public bool IsActive { get; set; }
    }
}