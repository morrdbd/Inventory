using DOMoRR.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DOMoRR.Models.ViewModels
{
    public class UserWithRole
    {
        public ApplicationUser AppUser { get; set; }
        public MenuAccessVM[] MenuAccess { get; set; }
        public string[] Roles { get; set; }

        public bool HasAccess(string controller, string action)
        {
            if(MenuAccess != null)
            {
                var accessObj = MenuAccess.FirstOrDefault(x => x.Controller.ToLower() == controller?.ToLower());
                if(accessObj != null)
                {
                    return DoCheckAccess(accessObj, action);
                }
            }
            return false;
        }

        public bool HasAccess(int menuId, string action)
        {
            if (MenuAccess != null)
            {
                var accessObj = MenuAccess.FirstOrDefault(x => x.MenuId == menuId);
                if (accessObj != null)
                {
                    return DoCheckAccess(accessObj, action);
                }
            }
            return false;
        }

        private bool DoCheckAccess(MenuAccessVM accessObj, string action)
        {
            var actionAccess = accessObj.GetType().GetProperty(action).GetValue(accessObj);
            if (accessObj.FullControl == true || Convert.ToBoolean(actionAccess) == true)
            {
                return true;
            }
            return false;
        }
    }
}