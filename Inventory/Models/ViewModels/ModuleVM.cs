using Inventory.Models.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class ModuleVM
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
    }

    public class UserPageVM
    {
        public List<ModuleVM> Menus { get; set; }
        public List<CustomRole> Roles { get; set; }
        public PagedList.IPagedList<User_List> Users { get; set; }
    }

    public class MenuAccessVM
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool FullControl { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Search { get; set; }
        public bool View { get; set; }
        public bool Delete { get; set; }
        public bool Print { get; set; }
        public bool Approve { get; set; }
        public bool Recordkm { get; set; }
        public bool Download { get; set; }
        public string Controller { get; set; }
    }
}