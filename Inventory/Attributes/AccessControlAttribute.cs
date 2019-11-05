using DOMoRR.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DOMoRR.Attributes
{
    public class AccessControlAttribute : AuthorizeAttribute
    {
        private string[] Actions;
        public AccessControlAttribute(string actions = null)
        {
            if(actions != null)
            {
                Actions = actions.Split(',');
            }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated == false)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                if(filterContext.HttpContext.Request.IsAjaxRequest() == false)
                {
                    var result = new ViewResult { ViewName = "Error" };
                    result.ViewBag.Error = 401;
                    filterContext.Result = result;
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.Result = new JsonResult { Data = false };
                }
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated == false)
            {
                return false;
            }
            if (Actions == null || Actions.Length == 0)
            {
                return true;
            }

            var id = httpContext.User.Identity.GetUserId<int>();
            var UserManager = httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var UserRoles = UserManager.GetRoles(id);

            if (UserRoles != null && UserRoles.Count != 0)
            {
                var RoleManager = httpContext.GetOwinContext().Get<ApplicationRoleManager>();
                var roleObj = RoleManager.FindByName(UserRoles[0]);
                if (roleObj != null)
                {
                    var db = new DOMoRRDBContext();
                    var controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString().ToLower();
                    var sql = "select MenuId from Menus where MenuLevel = 1 and Controller = @p0";
                    int menuId = db.Database.SqlQuery<int>(sql, controller).FirstOrDefault();
                    if (menuId != 0)
                    {
                        var moduleAccess = db.RoleAccess.Where(x => x.RoleId == roleObj.Id && x.MenuId == menuId).FirstOrDefault();
                        if (moduleAccess != null)
                        {
                            if (moduleAccess.FullControl == true) return true;
                            foreach (var action in Actions)
                            {
                                var actionAccess = moduleAccess.GetType().GetProperty(action).GetValue(moduleAccess);
                                if (Convert.ToBoolean(actionAccess) == true) return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}