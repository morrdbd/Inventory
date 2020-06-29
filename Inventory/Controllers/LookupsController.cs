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
            ViewBag.Lookups_drp = new SelectList(Lookups_drp, "ValueId", TextField);
            return View();
        }

        [AccessControl]
        [HttpPost]
        public JsonResult GroupListPartial(LookupGroupSearch model)
        {
            
            var query = db.LookupValues.Where(c => c.LookupCode == "ITEMGROUP");
            if(model.LookupGroupType != null)
            {
                query = query.Where(l=>l.GroupValueId == model.LookupGroupType);
            }
            if (!string.IsNullOrWhiteSpace(model.LookupName))
            {
                query = query.Where(l=>l.EnName.Contains(model.LookupName) || l.DrName.Contains(model.LookupName) || l.PaName.Contains(model.LookupName));
            }
            var records = query.OrderBy(x => Language == "prs" ? x.DrName : Language == "ps" ? x.PaName : x.EnName).OrderBy(l => l.ForOrdering).ToList();
            var data = records.Select(e => new
            {
                e.ValueId,
                e.EnName,
                e.DrName,
                e.PaName,
                e.ValueCode,
                GroupName = db.LookupValues.Where(r => r.IsActive == true && r.ValueId == e.GroupValueId).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault(),
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
            LookupValue.LookupCode = "ITEMGROUP";
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



    }
}