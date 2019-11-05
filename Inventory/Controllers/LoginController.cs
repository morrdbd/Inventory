using DOMoRR.Attributes;
using DOMoRR.Controllers;
using DOMoRR.Models;
using DOMoRR.Models.Tables;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.Mvc;

namespace DOMoRR.Controllers
{
    [AccessControl]
    public class LoginController : BaseController
    {
        private DOMoRRDBContext db { get; set; }
        private UserManager<ApplicationUser, int> UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        private ApplicationSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public LoginController()
        {
            db = new DOMoRRDBContext();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            try
            {
                var result = SignInManager.PasswordSignIn(username, password, false, false);
                if (result == SignInStatus.Success)
                {
                    var user = UserManager.Find(username, password);
                    if (user.IsActive == true)
                    {
                        user.LastLogin = DateTime.Now;
                        UserManager.Update(user);

                        db.SigninLogs.Add(new SigninLog
                        {
                            UserId = user.Id,
                            Username = user.UserName,
                            UserIP = Request.UserHostAddress,
                            SigninTime = DateTime.Now
                        });
                        db.SaveChanges();

                        var roles = UserManager.GetRoles(user.Id);

                        return Json(new
                        {
                            UserName = user.UserName,
                            Role = roles.Count != 0 ? roles[0] : null
                        });
                    }
                }
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        public JsonResult CheckPassword(string password)
        {
            try
            {
                var id = User.Identity.GetUserId<int>();
                var user = UserManager.FindById(id);
                var result = UserManager.CheckPassword(user, password);
                return Json(result);
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        public JsonResult ChangePass(string CurrPass, string NewPass)
        {
            try
            {
                var id = User.Identity.GetUserId<int>();
                var result = UserManager.ChangePassword(id, CurrPass, NewPass);
                if (result.Succeeded)
                {
                    var TheUser = UserManager.FindById(id);
                    TheUser.PassNeedsChange = false;
                    UserManager.Update(TheUser);
                    return Json(true);
                }
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }
    }
}
