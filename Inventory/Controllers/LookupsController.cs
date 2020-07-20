using Inventory.Attributes;
using Inventory.Models;
using Inventory.Models.Repositories;
using Inventory.Models.Tables;
using Inventory.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    [AccessControl]
    public class LookupsController : BaseController
    {
        //InventoryDBContext db = new InventoryDBContext();
        //AdminRepository AdminRepo = new AdminRepository();
        private InventoryDBContext db { get; set; }
        private AdminRepository AdminRepo { get; set; }

        public LookupsController()
        {
            db = new InventoryDBContext();
            AdminRepo = new AdminRepository();
        }

        //*******************************************************
        //******************GENERAL LOOKUPS**********************
        //*******************************************************

        [AccessControl("Search")]
        [HttpGet]
        public ActionResult General()
        {
            var Lookups_drp = db.LookupTypes.Where(x => x.IsActive == true)
                .OrderBy(x => Language == "prs" ? x.DrName : Language == "ps" ? x.PaName : x.EnName)
                .Where(i => i.LookupCode != "ITEMGROUP" && i.LookupCode != "ITEMCATEGORY").ToList();
            ViewBag.Lookups_drp = new SelectList(Lookups_drp, "LookupCode", TextField);

            return View();
        }

        [HttpPost]
        public ActionResult LookupValueList(string LookupCode, string ValueName)
        {
            var list = AdminRepo.SearchLookupValue(LookupCode, (ValueName ?? "").Trim(), Language).ToList();
            list = list.Where(i => i.LookupCode != "ITEMGROUP" && i.LookupCode != "ITEMCATEGORY").ToList();
            return PartialView("_LookupValueList", list);
        }

        [AccessControl("Add,Edit")]
        public JsonResult SaveLookupValue(LookupValue LookupValue)
        {
            if (LookupValue.ValueId != 0)
            {
                ModelState.Remove("ValueCode");
            }
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                return Json(false);
            }
            try
            {
                if (LookupValue.ValueId == 0)
                {
                    LookupValue.IsActive = true;
                    db.LookupValues.Add(LookupValue);
                }
                else
                {
                    db.Entry(LookupValue).State = EntityState.Modified;
                    db.Entry(LookupValue).Property(x => x.IsActive).IsModified = false;
                    db.Entry(LookupValue).Property(x => x.ValueCode).IsModified = false;
                }
                db.SaveChanges();
                return Json(true);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        public JsonResult CheckValueCode(int id, string ValueCode)
        {
            var Any = !db.LookupValues.Any(x => x.ValueId != id && x.ValueCode == ValueCode);
            return Json(Any);
        }

        public JsonResult GetLookupValue(int id = 0)
        {
            var obj = db.LookupValues.Find(id);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Delete")]
        public JsonResult DeleteRestoreLookupValue(int id = 0, bool value = false)
        {
            var row = db.LookupValues.Find(id);
            if (row != null)
            {
                row.IsActive = value;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //******************************************************************************
        //***************************Group***********************************************
        //******************************************************************************

        [AccessControl("Search,Add,Edit")]
        [HttpGet]
        public ActionResult Group()
        {
            ViewBag.search = new LookupGroupSearch();
            var Lookups_drp = db.LookupValues.Where(l=> l.IsActive == true && l.LookupCode == "UTYPE")
                .OrderBy(x => Language == "prs" ? x.DrName : Language == "ps" ? x.PaName : x.EnName);
            ViewBag.LookupsUsageType_drp = new SelectList(Lookups_drp, "ValueId", TextField);
            return View();
        }

        [AccessControl("Search")]
        [HttpPost]
        public JsonResult GroupListPartial(LookupGroupSearch model)
        {
            
            var query = db.LookupValues.Where(c => c.LookupCode == "ITEMGROUP");
            if(model.LookupUsageType != null)
            {
                query = query.Where(l=>l.GroupValueId == model.LookupUsageType);
            }
            if (!string.IsNullOrWhiteSpace(model.LookupName))
            {
                query = query.Where(l=>l.EnName.Contains(model.LookupName) || l.DrName.Contains(model.LookupName)
                || l.PaName.Contains(model.LookupName) || l.ValueCode.Contains(model.LookupName));
            }
            var records = query.OrderBy(x => Language == "prs" ? x.DrName : Language == "ps" ? x.PaName : x.EnName).OrderBy(l => l.ForOrdering).ToList();
            var data = records.Select(e => new
            {
                e.ValueId,
                e.EnName,
                e.DrName,
                e.PaName,
                e.ValueCode,
                UsageTypeName = db.LookupValues.Where(r => r.IsActive == true && r.ValueId == e.GroupValueId).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault(),
                e.IsActive
            }).ToList();

            ViewBag.search = model;
            return Json(new
            {
                data = data.Skip(model.start).Take(model.length).ToList(),
                recordsTotal = data.Count(),
                recordsFiltered = data.Count(),
                draw = model.draw,
            });
        }

        [AccessControl("Add,Edit")]
        public JsonResult SaveGroupValue(LookupValue LookupValue)
        {
            if (LookupValue.ValueId != 0)
            {
                ModelState.Remove("ValueCode");
            }
            var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                return Json(false);
            }
            try
            {
                if (LookupValue.ValueId == 0)
                {
                    LookupValue.IsActive = true;
                    db.LookupValues.Add(LookupValue);
                }
                else
                {
                    db.Entry(LookupValue).State = EntityState.Modified;
                    db.Entry(LookupValue).Property(x => x.IsActive).IsModified = false;
                    db.Entry(LookupValue).Property(x => x.ValueCode).IsModified = false;
                }
                db.SaveChanges();
                return Json(true);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        //******************************************************************************
        //***************************Category*******************************************
        //******************************************************************************

        public ActionResult Category()
        {
            ViewBag.search = new LookupCategorySearch();
            var LookupsUsageType_drp = db.LookupValues.Where(l => l.IsActive == true && l.LookupCode == "UTYPE")
                .OrderBy(x => Language == "prs" ? x.DrName : Language == "ps" ? x.PaName : x.EnName);
            ViewBag.LookupsUsageType_drp = new SelectList(LookupsUsageType_drp, "ValueId", TextField);
            return View();
        }

        [AccessControl("Search")]
        [HttpPost]
        public JsonResult CategoryListPartial(LookupCategorySearch model)
        {
            var query = (from l in db.LookupValues
                          join g in db.LookupValues on l.GroupValueId equals g.ValueId
                          join u in db.LookupValues on  g.GroupValueId equals u.ValueId
                          where l.LookupCode == "ITEMCATEGORY"
                          select new
                          {
                              l.ValueId,
                              l.ValueCode,
                              l.EnName,
                              l.DrName,
                              l.PaName,
                              UsageTypeId = u.ValueId,
                              UsageTypeName = (Language == "prs" ? u.DrName : Language == "ps" ? u.PaName : u.EnName),
                              GroupId = g.ValueId,
                              GroupName = (Language == "prs" ? g.DrName : Language == "ps" ? g.PaName : g.EnName),
                              l.IsActive
                          }
                          ).ToList();

            if (model.SLookupUsageType != null)
            {
                query = query.Where(l => l.UsageTypeId == model.SLookupUsageType).ToList();
            }
            if (model.SLookupGroupType != null)
            {
                query = query.Where(l => l.GroupId == model.SLookupGroupType).ToList();
            }
            if (!string.IsNullOrWhiteSpace(model.SLookupName))
            {
                query = query.Where(l => l.EnName.Contains(model.SLookupName) || l.DrName.Contains(model.SLookupName)
                || l.PaName.Contains(model.SLookupName) || l.ValueCode.Contains(model.SLookupName)).ToList();
            }
            var records = query.OrderBy(x => Language == "prs" ? x.DrName : Language == "ps" ? x.PaName : x.EnName).ToList();
            var data = records.Select(e => new
            {
                e.ValueId,
                e.EnName,
                e.DrName,
                e.PaName,
                e.ValueCode,
                e.GroupName,
                e.UsageTypeName,
                e.IsActive
            }).ToList();

            ViewBag.search = model;
            return Json(new
            {
                data = data.Skip(model.start).Take(model.length).ToList(),
                recordsTotal = data.Count(),
                recordsFiltered = data.Count(),
                draw = model.draw,
            });
        }

        public JsonResult GetCategoryLookupValue(int id = 0)
        {
            var obj = (from l in db.LookupValues
                         join g in db.LookupValues on l.GroupValueId equals g.ValueId
                         join u in db.LookupValues on g.GroupValueId equals u.ValueId
                         where l.LookupCode == "ITEMCATEGORY" && l.ValueId == id
                         select new
                         {
                             l.ValueId,
                             l.ValueCode,
                             l.EnName,
                             l.DrName,
                             l.PaName,
                             GroupId = g.ValueId,
                             UsageTypeId = u.ValueId,
                             l.IsActive
                         }
                          ).FirstOrDefault();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Add,Edit")]
        public JsonResult SaveCategoryValue(LookupCategoryVM model)
        {
            if (model.ValueId != 0)
            {
                ModelState.Remove("ValueCode");
            }
            var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                return Json(false);
            }
            try
            {
                if (model.ValueId == 0)
                {
                    var obj = new LookupValue() {
                        LookupCode = "ITEMCATEGORY",
                        ValueCode = model.ValueCode,
                        EnName = model.EnName,
                        DrName = model.DrName,
                        PaName = model.PaName,
                        GroupValueId = model.GroupId,
                        IsActive = true
                    };
                    db.LookupValues.Add(obj);
                }
                else
                {
                    var obj = db.LookupValues.Where(l=>l.ValueId == model.ValueId).FirstOrDefault();
                    obj.ValueCode = model.ValueCode;
                    obj.EnName = model.EnName;
                    obj.DrName = model.DrName;
                    obj.PaName = model.PaName;
                    obj.GroupValueId = model.GroupId;
                    obj.IsActive = true;
                }
                db.SaveChanges();
                return Json(true);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        //******************************************************************************
        //***************************Department*******************************************
        //******************************************************************************

        [AccessControl("Search,Add,Edit")]
        [HttpGet]
        public ActionResult Department()
        {
            ViewBag.search = new DepartmentSearch();
            var departments_drp = db.Departments.ToList()
                .OrderBy(x => Language == "prs" ? x.DrName : Language == "ps" ? x.PaName : x.EnName);
            ViewBag.departments_drp = new SelectList(departments_drp, "DepartmentID", TextField);
            return View();
        }

        [AccessControl("Search")]
        [HttpPost]
        public JsonResult DepartmentListPartial(DepartmentSearch model)
        {
            var query = db.Departments.Select(d=> new
                         {
                             d.DepartmentID,
                             d.EnName,
                             d.DrName,
                             d.PaName,
                             d.IsActive
                         }
                          ).ToList();
            if (!string.IsNullOrWhiteSpace(model.DepartmentName))
            {
                query = query.Where(l => l.EnName.Contains(model.DepartmentName) || l.DrName.Contains(model.DepartmentName)
                || l.PaName.Contains(model.DepartmentName)).ToList();
            }
            var records = query.OrderBy(x => Language == "prs" ? x.DrName : Language == "ps" ? x.PaName : x.EnName).ToList();
            var data = records.Select(e => new
            {
                e.DepartmentID,
                e.EnName,
                e.DrName,
                e.PaName,
                e.IsActive
            }).ToList();

            ViewBag.search = model;
            return Json(new
            {
                data = data.Skip(model.start).Take(model.length).ToList(),
                recordsTotal = data.Count(),
                recordsFiltered = data.Count(),
                draw = model.draw,
            });
        }

        public JsonResult GetDepartment(int id = 0)
        {
            var obj = db.Departments.Find(id);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [AccessControl("Add,Edit")]
        public JsonResult SaveDepartment(Department model)
        {
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                return Json(false);
            }
            try
            {
                if (model.DepartmentID == 0)
                {
                    model.IsActive = true;
                    model.InsertedBy = AppUser.Id;
                    model.InsertedDate = DateTime.Now;
                    db.Departments.Add(model);
                }
                else
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.Entry(model).Property(x => x.IsActive).IsModified = false;
                }
                db.SaveChanges();
                return Json(true);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false);
        }

        [AccessControl("Delete")]
        public JsonResult DeleteRestoreDepartment(int id = 0, bool value = false)
        {
            var row = db.Departments.Find(id);
            if (row != null)
            {
                row.IsActive = value;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


    }
}