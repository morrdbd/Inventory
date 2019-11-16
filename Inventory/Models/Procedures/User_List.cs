using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.Procedures
{
    public class User_List
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string UserRole { get; set; }
       
    }
}