using Inventory.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Inventory.Models.ViewModels;
using Inventory.Models.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Inventory.Models.Procedures;
using Inventory.Attributes;
using Microsoft.AspNet.Identity.EntityFramework;
using Inventory.Models.Tables;
using Inventory.Controllers;
using Inventory;

namespace Inventory.Controllers
{
    [AccessControl]
    public class UsersController : BaseController
    {
        public AdminRepository Repository { get; set; }
        public InventoryDBContext db { get; set; }
        private UserManager<ApplicationUser, int> UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
        }

        public UsersController()
        {
            Repository = new AdminRepository();
            db = new InventoryDBContext();
        }

        [AccessControl("View")]
        [HttpGet]
        public ActionResult Index(User_SC search)
        {
            if (Request.Cookies["users_criteria"] != null)
            {
                search = JsonConvert.DeserializeObject<User_SC>(Request.Cookies["users_criteria"].Value);
            }
            else
            {
                search.PageSize = 10;
                search.PageNo = 1;
            }

            ViewBag.Criteria = search;
            var pagedList = DoSearchUser(search);
            var menus = Repository.Modules_List(Language);
            var roles = RoleManager.Roles.OrderBy(x => x.Id).ToList();
            var model = new UserPageVM
            {
                Users = pagedList,
                Menus = menus,
                Roles = roles,
            };
            return View(model);
        }

        [AccessControl("Search")]
        [HttpPost]
        public ActionResult List(User_SC search, int page = 1)
        {
            search.PageNo = page;
            Response.Cookies.Add(new HttpCookie("users_criteria", JsonConvert.SerializeObject(search)));
            var pagedList = DoSearchUser(search);
            return PartialView("_List", pagedList);
        }

        private IPagedList<User_List> DoSearchUser(User_SC search)
        {
            var list = Repository.UserList(search, Language);
            return list.ToPagedList(search.PageNo ?? 1, search.PageSize);
        }

        public JsonResult CheckUserName(string username, int id)
        {
            try
            {
                username = (username ?? "").Trim().ToLower();
                var user = UserManager.FindByName(username);
                if (user != null)
                {
                    if (user.Id != id)
                    {
                        return Json(false);
                    }
                }
                return Json(true);
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        [AccessControl("Add")]
        public JsonResult Create(User_VM UserVM)
        {
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                return Json(false);
            }
            try
            {
                var TheUser = new ApplicationUser
                {
                    UserName = UserVM.UserName,
                    DisplayName = UserVM.DisplayName,
                    //StationType = UserVM.StationType,
                    //StationCode = UserVM.StationCode,
                    IsActive = true,
                    PassNeedsChange = true,
                    InsertedBy = AppUser.Id,
                    InsertedDate = DateTime.Now,
                };
                var result = UserManager.Create(TheUser, UserVM.Password);
                if (result.Succeeded)
                {
                    result = UserManager.AddToRole(TheUser.Id, UserVM.Role);
                    if (result.Succeeded)
                    {
                        db.ActivityLogs.Add(new ActivityLog
                        {
                            ModifiedTable = "Users",
                            ModfiedId = TheUser.Id,
                            Action = "Insert",
                            UserId = AppUser.Id,
                            ModifiedTime = DateTime.Now,
                            ModifiedData = JsonConvert.SerializeObject(UserVM),
                        });
                        db.SaveChanges();
                        return Json(true);
                    }
                }
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        [AccessControl("Edit")]
        public JsonResult Update(User_VM UserVM)
        {
            ModelState.Remove("Password");
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                return Json(false);
            }
            try
            {
                var TheUser = UserManager.FindById(UserVM.Id);
                if(TheUser != null)
                {
                    TheUser.UserName = UserVM.UserName;
                    TheUser.DisplayName = UserVM.DisplayName;
                    //TheUser.StationType = UserVM.StationType;
                    //TheUser.StationCode = UserVM.StationCode;
                    TheUser.LastUpdatedBy = AppUser.Id;
                    TheUser.LastUpdatedDate = DateTime.Now;

                    var result = UserManager.Update(TheUser);
                    if (result.Succeeded)
                    {
                        var Roles = UserManager.GetRoles(TheUser.Id);
                        if (!Roles.Contains(UserVM.Role))
                        {
                            UserManager.RemoveFromRole(TheUser.Id, Roles[0]);
                            UserManager.AddToRole(TheUser.Id, UserVM.Role);
                        }
                        db.ActivityLogs.Add(new ActivityLog
                        {
                            ModifiedTable = "Users",
                            ModfiedId = TheUser.Id,
                            Action = "Update",
                            UserId = AppUser.Id,
                            ModifiedTime = DateTime.Now,
                            ModifiedData = JsonConvert.SerializeObject(UserVM),
                        });
                        db.SaveChanges();
                        return Json(true);
                    }
                }
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        public JsonResult Load(int id)
        {
            try
            {
                var user = UserManager.FindById(id);
                var roles = UserManager.GetRoles(id);
                if (user != null)
                {
                    var uservm = new User_VM
                    {
                        Id = user.Id,
                        DisplayName = user.DisplayName,
                        UserName = user.UserName,
                        //StationType = user.StationType,
                        //StationCode = user.StationCode,
                        Role = roles.Count != 0 ? roles[0] : "",
                    };
                    return Json(uservm, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Delete")]
        public JsonResult Delete(int id)
        {
            try
            {
                var user = UserManager.FindById(id);
                if (user != null)
                {
                    user.IsActive = false;
                    UserManager.Update(user);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Delete")]
        public JsonResult Restore(int id)
        {
            try
            {
                var user = UserManager.FindById(id);
                if (user != null)
                {
                    user.IsActive = true;
                    UserManager.Update(user);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangePass(int id, string password)
        {
            try
            {
                var user = UserManager.FindById(id);
                if (user != null)
                {
                    UserManager.RemovePassword(id);
                    UserManager.AddPassword(id, password);
                    user.PassNeedsChange = true;
                    UserManager.Update(user);
                    return Json(true);
                }
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        public JsonResult CheckPassword(string password)
        {
            try
            {
                var result = UserManager.CheckPassword(AppUser, password);
                return Json(result);
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        //===========================================================
        //==================ROLE MANAGEMENT==========================
        //===========================================================

        [AccessControl("Add")]
        [HttpPost]
        public JsonResult InsertRole(RoleFormVM model)
        {
            if(ModelState.IsValid == false)
            {
                LogModelErrors();
                return Json(false);
            }
            try
            {
                var role = new CustomRole(model.RoleName);
                var result = RoleManager.Create(role);
                if (result.Succeeded)
                {
                    foreach (var row in model.Menus)
                    {
                        row.RoleId = role.Id;
                        db.RoleAccess.Add(row);
                    }
                    db.SaveChanges();
                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Roles",
                        ModfiedId = role.Id,
                        Action = "Insert",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(model),
                    });
                    db.SaveChanges();
                    return Json(true);
                }
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        public JsonResult LoadRole(int id = 0)
        {
            try
            {
                var role = RoleManager.FindById(id);
                var access = db.Database.SqlQuery<MenuAccessVM>("ModuleRoleAccess @p0", id).ToList();
                return Json(new
                {
                    RoleId = id,
                    RoleName = role?.Name,
                    Access = access,
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [AccessControl("Edit")]
        public JsonResult UpdateRole(RoleFormVM model)
        {
            try
            {
                var role = RoleManager.FindById(model.RoleId);
                if(role != null)
                {
                    role.Name = model.RoleName;
                    var result = RoleManager.Update(role);
                    if (result.Succeeded)
                    {
                        var access = db.RoleAccess.Where(x => x.RoleId == model.RoleId).ToList();
                        access.ForEach(x => db.RoleAccess.Remove(x));
                        foreach (var row in model.Menus)
                        {
                            row.RoleId = role.Id;
                            db.RoleAccess.Add(row);
                        }
                        db.SaveChanges();
                        db.ActivityLogs.Add(new ActivityLog
                        {
                            ModifiedTable = "Roles",
                            ModfiedId = role.Id,
                            Action = "Update",
                            UserId = AppUser.Id,
                            ModifiedTime = DateTime.Now,
                            ModifiedData = JsonConvert.SerializeObject(model),
                        });
                        db.SaveChanges();
                        return Json(true);
                    }
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        public JsonResult GetRoles()
        {
            var data = RoleManager.Roles.OrderBy(x => x.Id).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
