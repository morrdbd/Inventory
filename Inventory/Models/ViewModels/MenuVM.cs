using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOMoRR.Models.ViewModels
{
    public class MenuVM
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int MenuLevel { get; set; }
        public int SuperMenuId { get; set; }
        public bool HasSubMenu { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int ModuleId { get; set; }
        public string ActionType { get; set; }
        public bool IsActive { get; set; }
    }
}