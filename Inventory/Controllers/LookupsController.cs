using Inventory.Attributes;
using Inventory.Models;
using Inventory.Models.Repositories;
using Inventory.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    [AccessControl]
    public class LookupsController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();
        //******************************************************************************
        //***************************Item***********************************************
        //******************************************************************************
        [AccessControl("Add")]
        public ActionResult Item()
        {
            var model = db.Items.ToList();
            return View(model);
        }
        [AccessControl("View")]
        public ActionResult ItemList()
        {
            var model = db.Items.ToList();
            return PartialView("_ItemList", model);
        }
        [AccessControl("Add,Edit")]
        public JsonResult SaveItem(Item model)
        {
            if (model.ItemID != 0)
            {
                ModelState.Remove("ItemCode");
            }
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                return Json(false);
            }
            try
            {
                if (model.ItemID == 0)
                {
                    model.IsActive = true;
                    db.Items.Add(model);
                }
                else
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.Entry(model).Property(x => x.IsActive).IsModified = false;
                    db.Entry(model).Property(x => x.ItemCode).IsModified = false;
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

        public JsonResult CheckItemCode(int id, string ItemCode)
        {
            var Any = !db.Items.Any(x => x.ItemID != id && x.ItemCode == ItemCode);
            return Json(Any);
        }
        [AccessControl("View")]
        public JsonResult GetItem(int id = 0)
        {
            var obj = db.Items.Find(id);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Delete")]
        public JsonResult DeleteRestoreItem(int id = 0, bool value = false)
        {
            var row = db.Items.Find(id);
            if (row != null)
            {
                row.IsActive = value;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        } 
       
    }
}