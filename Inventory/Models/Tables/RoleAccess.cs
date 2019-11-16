using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.Tables
{
    [Table("RoleAccess")]
    public class RoleAccess
    {
        [Key]
        public int RoleAccessId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool FullControl { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Search { get; set; }
        public bool View { get; set; }
        public bool Delete { get; set; }
        public bool Print { get; set; }
        public bool Download { get; set; }
    }
}