using DOMoRR.Models;
using DOMoRR.Models.Tables;
using DOMoRR.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DOMoRR.Controllers
{
    public class BaseController : Controller
    {
        protected string TextField;
        protected string Language;
        protected ApplicationUser AppUser;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            SetLanguage();
            ViewBag.OfflineAllowed = System.Configuration.ConfigurationManager.AppSettings["OfflineAllowed"].ToString();
            try
            {
                if (requestContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    //var UserManager = new UserManager<ApplicationUser, int>(new CustomUserStore(new UserDbContext()));
                    var owin = requestContext.HttpContext.GetOwinContext();
                    var UserManager = owin.GetUserManager<ApplicationUserManager>();
                    var id = requestContext.HttpContext.User.Identity.GetUserId<int>();
                    AppUser = UserManager.FindById(id);
                    
                    if (AppUser == null || AppUser.IsActive == false)
                    {
                        owin.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        requestContext.HttpContext.Response.Redirect("/");
                    }
                    var Roles = UserManager.GetRoles(id);
                    if (Roles.Count != 0)
                    {
                        var db = new DOMoRRDBContext();
                        var sql = @"select a.* , m.Controller from
                                RoleAccess a join Menus m on a.MenuId = m.MenuId
                                join AspNetRoles r on a.RoleId = r.Id where m.MenuLevel = 1 and r.Name = @p0";
                        var menuAccess = db.Database.SqlQuery<MenuAccessVM>(sql, Roles[0]).ToArray();
                        var UserWithRole = new UserWithRole
                        {
                            AppUser = AppUser,
                            MenuAccess = menuAccess,
                            Roles = UserManager.GetRoles(id).ToArray()
                        };
                        ViewBag.AppUser = UserWithRole;
                    }
                }
                if (requestContext.HttpContext.Request.IsAjaxRequest() == false)
                {
                    PopulateMenu();
                }
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
        }

        private void SetLanguage()
        {
            Language = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            switch (Language)
            {
                case "prs":
                    TextField = "DrName";
                    break;
                case "ps":
                    TextField = "PaName";
                    break;
                default:
                    TextField = "EnName";
                    break;
            }

            ViewBag.Lang = Language;
            ViewBag.TextField = TextField;
        }

        private void PopulateMenu()
        {
            var db = new DOMoRRDBContext();
            var Menu = db.Database.SqlQuery<MenuVM>("Menu_List @p0", Language).ToList();

            ViewBag.Menu = Menu;

            var Controller = ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            var Action = ControllerContext.RouteData.Values["action"].ToString().ToLower();
            bool Exact = true;

            var menu3 = Menu.Where(x => (x.Controller ?? "").ToLower() == Controller && (x.Action ?? "").ToLower() == Action).FirstOrDefault();
            if (menu3 == null)
            {
                Exact = false;
                menu3 = Menu.Where(x => (x.Controller ?? "").ToLower() == Controller && x.MenuLevel == 3).FirstOrDefault();
                if (menu3 == null)
                {
                    menu3 = Menu.Where(x => (x.Controller ?? "").ToLower() == Controller && x.MenuLevel == 2).FirstOrDefault();
                    if (menu3 == null)
                    {
                        menu3 = Menu.Where(x => (x.Controller ?? "").ToLower() == Controller && x.MenuLevel == 1).FirstOrDefault();
                    }
                }
            }

            if (menu3 != null)
            {
                if (menu3.MenuLevel == 3)
                {
                    if (Exact) ViewBag.ActiveMenu3 = menu3.MenuId;

                    var menu2 = Menu.Where(x => x.MenuLevel == 2 && x.MenuId == menu3.SuperMenuId).SingleOrDefault();
                    ViewBag.ActiveMenu2 = menu2.MenuId;

                    var menu1 = Menu.Where(x => x.MenuLevel == 1 && x.MenuId == menu2.SuperMenuId).SingleOrDefault();
                    ViewBag.ActiveMenu1 = menu1.MenuId;
                }
                else if (menu3.MenuLevel == 2)
                {
                    if (Exact) ViewBag.ActiveMenu2 = menu3.MenuId;

                    var menu2 = Menu.Where(x => x.MenuLevel == 1 && x.MenuId == menu3.SuperMenuId).SingleOrDefault();
                    ViewBag.ActiveMenu1 = menu2.MenuId;
                }
                else if (menu3.MenuLevel == 1)
                {
                    if (Exact) ViewBag.ActiveMenu1 = menu3.MenuId;
                }
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception e = filterContext.Exception;
            Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            filterContext.ExceptionHandled = true;

            if (filterContext.HttpContext.Request.IsAjaxRequest() == false)
            {
                var result = new ViewResult { ViewName = "Error" };
                result.ViewBag.Error = 500;
                filterContext.Result = result;
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = new JsonResult { Data = -1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        protected void LogModelErrors()
        {
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(error.Exception);
                }
            }
        }
    }
}
