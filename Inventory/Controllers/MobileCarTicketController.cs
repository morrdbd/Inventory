using Inventory.Attributes;
using Inventory.HelperCode;
using Inventory.Models;
using Inventory.Models.Repositories;
using Inventory.Models.Tables;
using Inventory.Models.ViewModels;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class MobileCarTicketController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();

        [AccessControl("Add")]
        public ActionResult Add()
        {
            CreateDropDown();
            return View("Form", new MobileCarTicket());
        }

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
                    return Json(model.MobileCarTicketID, JsonRequestBehavior.AllowGet);
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
                e.EmailAddress,
                VisitingDate = e.VisitingDate.ToDateString(Language),
                e.VisitingPlace,
                e.VisitingTime,
                e.VisitingPurpose,
                DepartmentName = db.Departments.Where(r => r.IsActive == true && r.DepartmentID == e.DepartmentID).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault(),
                e.Startkm,
                e.Endkm
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
        public ActionResult Edit(int id)
        {
            CreateDropDown();
            var _ticket = db.MobileCarTickets.SingleOrDefault(t => t.MobileCarTicketID == id);
            if (_ticket == null)
            {
                return HttpNotFound();
            }
            _ticket.VisitingDateForm = _ticket.VisitingDate.ToDateString(Language);
            return View("Form", _ticket);
        }

        [AccessControl("Delete")]
        public JsonResult Delete(int id)
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

        [AccessControl("Delete")]
        public JsonResult ApproveRejectTicket(int id = 0, bool value = false)
        {
            var row = db.MobileCarTickets.Find(id);
            if (row != null)
            {
                try
                {
                    row.IsApproved = value;
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                catch(Exception e) {
                    return Json(false);
                }
            }
            return Json(false);
        }
        [AccessControl("Delete")]
        public JsonResult SendTicketApproveMail(int id = 0)
        {
            var row = db.MobileCarTickets.Find(id);
            if (row != null && !string.IsNullOrWhiteSpace(row.EmailAddress))
            {
                //try
                //{
                    var p = this.ControllerContext;
                string templateFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Views/Emails/MobileCarTicket.html");
                var templateAsString = System.IO.File.ReadAllText(templateFilePath);
                var View_model = CreateModel(row);
                var body = RazorEngine.Engine.Razor.RunCompile(templateAsString, "templateKey", typeof(MobileCarTicket_VM), View_model);
                EmailHelper.SendEmail(row.EmailAddress,body);
                    return Json(true, JsonRequestBehavior.AllowGet);
                //}
                //catch(Exception e) {
                //    return Json(false, JsonRequestBehavior.AllowGet);
                //}
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        [AccessControl("View")]
        public ActionResult View(int id = 0)
        {
            var _record = db.MobileCarTickets.Where(t => t.IsActive == true && t.MobileCarTicketID == id).FirstOrDefault();
            if (_record == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(_record);
            CreateDropDown();
            return View("View", model);
        }

        [AccessControl("Delete")]
        public JsonResult AssignCar(AssignCar model)
        {
            var record = db.MobileCarTickets.Find(model.MobileCarTicketID);
            if (record != null)
            {
                try
                {
                    record.MobileCarID = model.MobileCarID;
                    record.Startkm = model.Startkm;
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }catch(Exception e)
                {
                    return Json(false);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Delete")]
        public JsonResult RecordEndkm(RecordEndkm model)
        {
            var record = db.MobileCarTickets.Find(model.MobileCarTicketID);
            if (record != null)
            {
                try
                {
                    record.Endkm = model.Endkm;
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }catch(Exception e)
                {
                    return Json(false);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        private void CreateDropDown()
        {

            var departmentList = db.Departments.Where(d => d.IsActive == true).Select(d =>
                new { d.DepartmentID, DepartmentName = Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName }).ToList();
            ViewBag.DepartmentDrp = new SelectList(departmentList, "DepartmentID", "DepartmentName");

            var cartListQuery = db.MobileCars.Where(d => d.IsActive == true).Select(d =>
               new { d.MobileCarID, d.CarType, d.NumberPlate }).ToList();
            var cartList = cartListQuery.Select(c=>
            new {c.MobileCarID, CarType = (c.NumberPlate +" "+ AdminRepo.LookupNameByVlueCode(Language,c.CarType))}).ToList();
            ViewBag.CarDrp = new SelectList(cartList, "MobileCarID", "CarType");
        }

        private MobileCarTicket_VM CreateModel(MobileCarTicket model)
        {
            var ticket = new MobileCarTicket_VM()
            {
                MobileCarTicketID = model.MobileCarTicketID,
                DepartmentName = db.Departments.Where(d => d.DepartmentID == model.DepartmentID).Select(d => Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                PersAssignedName = model.PersAssignedName,
                PersAssignedOccupation = model.PersAssignedOccupation,
                EmailAddress = model.EmailAddress,
                VisitingDate = model.VisitingDate.ToDateString(Language),
                VisitingPurpose = model.VisitingPurpose,
                VisitingPlace = model.VisitingPlace,
                VisitingTime = model.VisitingTime,
                IsApproved = model.IsApproved,
            };
            var carInfo = db.MobileCars.Where(c => c.MobileCarID == model.MobileCarID).FirstOrDefault();
            if (model.MobileCarID != null && carInfo != null)
            {
                ticket.MobileCarID = carInfo.MobileCarID;
                ticket.NumberPlate = carInfo.NumberPlate;
                ticket.DriverName = carInfo.DriverName;
                ticket.CarType = AdminRepo.LookupNameByVlueCode(Language, carInfo.CarType);
                ticket.DriverPhoneNo = carInfo.DriverPhoneNo;
                ticket.Startkm = model.Startkm;
                ticket.Endkm = model.Endkm;
            }
            return ticket;
        }
        

    }
}