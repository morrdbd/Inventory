using Inventory.Attributes;
using Inventory.Models;
using Inventory.Models.Repositories;
using Inventory.Models.Tables;
using Inventory.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class MobileCarTicketController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();

        [HttpGet]
        [AccessControl("Search")]
        public ActionResult Search()
        {
            CreateDropDown();
            ViewBag.search = new MobileCarTicket_Search();
            return View("Search", new MobileCarTicket());
        }

        [AccessControl("Add")]
        public JsonResult Save(MobileCarTicket model)
        {
            ViewBag.search = new MobileCarTicket_Search();
            model.VisitingDate = model.VisitingDateForm.ToDate(Language);
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            try
            {
                if (ModelState.IsValid == true)
                {
                    if (model.MobileCarTicketID == 0)
                    {
                        model.IsActive = true;
                        model.InsertedBy = AppUser.Id;
                        model.InsertedDate = DateTime.Now;
                        db.MobileCarTickets.Add(model);
                    }
                    else
                    {
                        model.LastUpdatedBy = AppUser.Id;
                        model.LastUpdatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                        db.Entry(model).Property(x => x.IsActive).IsModified = false;
                        db.Entry(model).Property(x => x.InsertedBy).IsModified = false;
                        db.Entry(model).Property(x => x.InsertedDate).IsModified = false;
                    }
                    db.SaveChanges();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

        [AccessControl]
        [HttpPost]
        public JsonResult MobileCarTicketPartialList(MobileCarTicket_Search model)
        {
            var _dateFrom = model.DateFrom.ToDate(Language);
            var _dateTo = model.DateTo.ToDate(Language);

            var query = db.MobileCarTickets.Where(c => c.IsActive == true);

            if (model.sMobileCarTicketID != null)
            {
                query = query.Where(c => c.MobileCarTicketID == model.sMobileCarTicketID);
            }
            if (model.sDepartmentID != null)
            {
                query = query.Where(c => c.DepartmentID == model.sDepartmentID);
            }
            if (!string.IsNullOrWhiteSpace(model.sPersAssignedName))
            {
                query = query.Where(c => c.PersAssignedName.Contains(model.sPersAssignedName));
            }
            if (!string.IsNullOrWhiteSpace(model.sPersAssignedOccupation))
            {
                query = query.Where(c => c.PersAssignedOccupation.Contains(model.sPersAssignedOccupation));
            }
            if (!string.IsNullOrWhiteSpace(model.sVisitingPlace))
            {
                query = query.Where(c => c.VisitingPlace.Contains(model.sVisitingPlace));
            }
            if (!string.IsNullOrWhiteSpace(model.sVisitingPurpose))
            {
                query = query.Where(c => c.VisitingPurpose.Contains(model.sVisitingPurpose));
            }

            if (_dateFrom != null)
            {
                query = query.Where(c => c.VisitingDate >= _dateFrom);
            }
            if (_dateTo != null)
            {
                query = query.Where(c => c.VisitingDate <= _dateTo);
            }
            var records = query.OrderBy(e => e.InsertedDate).ToList();
            var data = records.Select(e => new
            {
                e.MobileCarTicketID,
                e.PersAssignedName,
                e.PersAssignedOccupation,
                VisitingDate = e.VisitingDate.ToDateString(Language),
                e.VisitingPlace,
                e.VisistingTime,
                e.VisitingPurpose,
                DepartmentName = db.Departments.Where(r => r.IsActive == true && r.DepartmentID == e.DepartmentID).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault(),
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

        [HttpGet]
        [AccessControl("Edit")]
        public JsonResult MobileCarTicketRecord(int id)
        {
            var obj = db.MobileCarTickets.Find(id);
            if (obj != null)
            {
                obj.VisitingDateForm = obj.VisitingDate.ToDateString(Language);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }



        [AccessControl("Delete")]
        public JsonResult DeleteTicket(int id)
        {
            var obj = db.MobileCarTickets.Find(id);
            if (obj != null)
            {
                obj.IsActive = false;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        private void CreateDropDown()
        {

            var departmentList = db.Departments.Where(d => d.IsActive == true).Select(d =>
                new { d.DepartmentID, DepartmentName = Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName }).ToList();
            ViewBag.DepartmentDrp = new SelectList(departmentList, "DepartmentID", "DepartmentName");
        }
    }
}