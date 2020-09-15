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

            var query = (from t in db.MobileCarTickets
                          join c in db.MobileCars on t.MobileCarID equals c.MobileCarID into AssignedCar
                          from ac in AssignedCar.DefaultIfEmpty()
                          where t.IsActive == true
                          select new {
                              t.IsActive,
                              t.MobileCarTicketID,
                              t.IsApproved,
                              t.PersAssignedName,
                              t.PersAssignedOccupation,
                              t.VisitingDate,
                              t.VisitingTime,
                              t.VisitingPlace,
                              t.VisitingPurpose,
                              t.DepartmentID,
                              t.Startkm,
                              t.Endkm,
                              MobileCarID = (ac == null ? 0 : ac.MobileCarID),
                              DriverName = (ac == null ? "" : ac.DriverName),
                              CarType = (ac == null ? "" : ac.CarType),
                              NumberPlate = (ac == null ? "" : ac.NumberPlate),
                              t.InsertedDate
                          });
            //var query = db.MobileCarTickets.Where(c => c.IsActive == true);

            if (model.sMobileCarTicketID != null)
            {
                query = query.Where(c => c.MobileCarTicketID == model.sMobileCarTicketID);
            }
            if (model.sDepartmentID != null)
            {
                query = query.Where(c => c.DepartmentID == model.sDepartmentID);
            }
            if (model.sMobileCarID != null)
            {
                query = query.Where(c => c.MobileCarID == model.sMobileCarID);
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
            var records = query.OrderByDescending(e => e.InsertedDate).ToList();
            var data = records.Select(e => new
            {
                e.IsApproved,
                e.MobileCarTicketID,
                e.PersAssignedName,
                e.PersAssignedOccupation,
                //e.EmailAddress,
                VisitingDate = e.VisitingDate.ToDateString(Language),
                e.VisitingPlace,
                e.VisitingTime,
                e.VisitingPurpose,
                DepartmentName = db.Departments.Where(r => r.IsActive == true && r.DepartmentID == e.DepartmentID).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault(),
                e.Startkm,
                e.Endkm,
                MobileCar = AdminRepo.LookupNameByVlueCode(Language, e.CarType) + e.NumberPlate,
                e.DriverName,
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
                ReleasePreviousAssignedCar(obj);
                obj.IsActive = false;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Approve")]
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

        [AccessControl("Approve")]
        public JsonResult SendTicketApproveEmail(int id = 0)
        {
            var row = db.MobileCarTickets.Find(id);
            if (row != null)
            {
                try
                {
                var view_model = CreateModel(row);
                    var mobileCar = db.MobileCars.Where(c => c.MobileCarID == view_model.MobileCarID && c.IsActive == true).FirstOrDefault();
                    if (mobileCar == null)
                    {
                        return Json(false);
                    }
                    string body = EmailHelper.CreatMobileCarTicketApproveEmailBody(view_model);
                    string subject = "تایید درخواست موتر سیار";
                    string depEmailAddress = db.Departments.Where(d => d.DepartmentID == row.DepartmentID).Select(d => d.EmailAddress).FirstOrDefault();
                EmailHelper.SendEmail(depEmailAddress, subject,body);
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
                catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        [AccessControl("Approve")]
        public JsonResult SendTicketRejectionEmail(int id = 0)
        {
            var row = db.MobileCarTickets.Find(id);
            if (row != null)
            {
                try
                {
                //var view_model = CreateModel(row);
                    string body = "<p>درخواست شما برای موتر سیار رد شود</p>" +
                        "<p>"+ row.VisitingPlace + " : محل سفر</p>"
                        +"<h3>از طرف ریاست اداری</h3>";
                    string subject = "رد درخواست موتر سیار";
                    string depEmailAddress = db.Departments.Where(d => d.DepartmentID == row.DepartmentID).Select(d => d.EmailAddress).FirstOrDefault();
                    EmailHelper.SendEmail(depEmailAddress, subject,body);
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
                catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
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

        [AccessControl("Approve")]
        public JsonResult ApproveAndAssignCar(ApproveAndAssignCar model)
        {
            var record = db.MobileCarTickets.Find(model.MobileCarTicketID);
            if (record != null && model.MobileCarID != null)
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var mobileCar = db.MobileCars.Where(c => c.MobileCarID == model.MobileCarID && c.IsActive == true && c.IsAvailable == true).FirstOrDefault();
                        if (mobileCar == null)
                        {
                            return Json(false);
                        }
                        ReleasePreviousAssignedCar(record);
                        record.IsApproved = true;
                        record.MobileCarID = model.MobileCarID;
                        record.Startkm = mobileCar.Currentkm;
                        ////////////////////change the availability of mobile car /////////////////////
                        mobileCar.IsAvailable = false;
                        db.SaveChanges();
                        trans.Commit();
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    }
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Recordkm")]
        public JsonResult RecordEndkm(RecordEndkm model)
        {
            var record = db.MobileCarTickets.Find(model.MobileCarTicketID);
            if (record != null && record.MobileCarID != null)
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var mobileCar = db.MobileCars.Where(c => c.MobileCarID == record.MobileCarID && c.IsActive == true).FirstOrDefault();
                        if (mobileCar == null)
                        {
                            return Json(false);
                        }
                        record.Endkm = model.Endkm;
                        ////////////////////change the availability of mobile car /////////////////////
                        mobileCar.IsAvailable = true;
                        db.SaveChanges();
                        trans.Commit();
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    }
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
            var allCartList = cartListQuery.Select(c=>
            new {c.MobileCarID, CarType = (c.NumberPlate +" "+ AdminRepo.LookupNameByVlueCode(Language,c.CarType))}).ToList();
            ViewBag.CarDrp = new SelectList(allCartList, "MobileCarID", "CarType");

            var availableCartListQuery = db.MobileCars.Where(d => d.IsActive == true && d.IsAvailable).Select(d =>
               new { d.MobileCarID, d.CarType, d.NumberPlate }).ToList();
            var availableCartList = availableCartListQuery.Select(c=>
            new {c.MobileCarID, CarType = (c.NumberPlate +" "+ AdminRepo.LookupNameByVlueCode(Language,c.CarType))}).ToList();
            ViewBag.AvailableCarDrp = new SelectList(availableCartList, "MobileCarID", "CarType");
        }

        private void ReleasePreviousAssignedCar(MobileCarTicket model)
        {
            //Relase the previous car if assigned
            if (model.MobileCarID != null)
            {
                var previousAssignedCar = db.MobileCars.Where(c => c.MobileCarID == model.MobileCarID).FirstOrDefault();
                if (previousAssignedCar != null)
                {
                    previousAssignedCar.IsAvailable = true;
                    model.MobileCarID = null;
                    model.Startkm = null;
                    db.SaveChanges();
                }
            }
        }

        private MobileCarTicket_VM CreateModel(MobileCarTicket model)
        {
            var ticket = new MobileCarTicket_VM()
            {
                MobileCarTicketID = model.MobileCarTicketID,
                DepartmentName = db.Departments.Where(d => d.DepartmentID == model.DepartmentID).Select(d => Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                PersAssignedName = model.PersAssignedName,
                PersAssignedOccupation = model.PersAssignedOccupation,
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