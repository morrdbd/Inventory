using Inventory.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class RoleFormVM
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public RoleAccess[] Menus { get; set; }
    }
}