using Inventory.Attributes;
using Inventory.Controllers;
using Inventory.Models;
using Inventory.Models.Procedures;
using Inventory.Models.Repositories;
using Inventory.Models.Tables;
using Inventory.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOMoRR.Controllers
{
    public class ReceiptReportMem7Controller : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();
        [AccessControl("Add")]

        public ActionResult Add()
        {
            CreateDropDown();
            return View("Form", new ReceiptReportVM());
        }

        private void CreateDropDown()
        {
            var unitList = db.LookupValues.Where(l => l.LookupCode == "UNIT" && l.IsActive == true).Select(l =>
             new { l.ValueId, UnitName = Language == "prs" ? l.DrName : Language == "ps" ? l.PaName : l.EnName }).ToList();
            ViewBag.UnitDrp = new SelectList(unitList, "ValueId", "UnitName");
        }

        private ReceiptReportVM CreateModel(ReceiptReport _ReceiptReport)
        {
            return new ReceiptReportVM
            {
                ReceiptReportID = _ReceiptReport.ReceiptReportID,
                ReportNumber = _ReceiptReport.ReportNumber,
                Organization = _ReceiptReport.Organization,
                ReceiptDateVM = _ReceiptReport.ReceiptDate.ToDateString(Language),
                DeliveryPlace = _ReceiptReport.DeliveryPlace,
                SuggBillNumber = _ReceiptReport.SuggBillNumber,
                ObtainedFromContractor = _ReceiptReport.ObtainedFromContractor,
                Mem3DateVM = _ReceiptReport.Mem3Date.ToDateString(Language),
                Details = _ReceiptReport.Details,
                ReceiptReportItems = db.ReceiptReportItems.Where(i => i.IsActive == true && i.ReceiptReportID == _ReceiptReport.ReceiptReportID).Select(i => new Receipt_Report_Item {
                    ID = i.ID,
                    ReceiptReportID = i.ReceiptReportID,
                    Quantity = i.Quantity,
                    ItemDetails = i.ItemDetails,
                    UnitPrice = i.UnitPrice,
                    TotalPrice =  i.Quantity * i.UnitPrice,
                    Remarks = i.Remarks,
                    UnitID = i.UnitID,
                    UnitName = db.LookupValues.Where(l=>l.ValueId == i.UnitID).Select(l=> Language == "prs" ? l.DrName : Language == "ps" ? l.PaName : l.EnName).FirstOrDefault()
                }).ToList()
            };
        }

        [HttpPost]
        [AccessControl("Add")]
        public JsonResult Insert(Receipt_Report_Form_VM model)
        {
            if (ModelState.IsValid == false)
            {
                LogModelErrors();
                return Json(false);
            }
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    var _ReceiptReport = new ReceiptReport
                    {
                        ReportNumber = model.ReportNumber,
                        Organization = model.Organization,
                        ReceiptDate = model.ReceiptDateVM.ToDate(Language),
                        DeliveryPlace = model.DeliveryPlace,
                        SuggBillNumber = model.SuggBillNumber,
                        ObtainedFromContractor = model.ObtainedFromContractor,
                        Mem3Date = model.Mem3DateVM.ToDate(Language),
                        Details = model.Details,
                        InsertedBy = AppUser.Id,
                        InsertedDate = DateTime.Now,
                        IsActive = true,
                    };

                    db.ReceiptReports.Add(_ReceiptReport);
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Ticket",
                        ModfiedId = _ReceiptReport.ReceiptReportID,
                        Action = "Insert",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_ReceiptReport),
                    });
                    db.SaveChanges();

                    if (model.ReceiptReportItems != null)
                    {
                        foreach (var row in model.ReceiptReportItems)
                        {
                            if (row != null)
                            {
                                row.ReceiptReportID = _ReceiptReport.ReceiptReportID;
                                row.InsertedBy = AppUser.Id;
                                row.InsertedDate = DateTime.Now;
                                row.IsActive = true;
                                db.ReceiptReportItems.Add(row);
                                db.SaveChanges();

                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "ReceiptReportItem",
                                    ModfiedId = row.ID,
                                    Action = "Insert",
                                    UserId = AppUser.Id,
                                    ModifiedTime = DateTime.Now,
                                    ModifiedData = JsonConvert.SerializeObject(row),
                                });
                                db.SaveChanges();
                            }
                        }
                    }

                    trans.Commit();
                    return Json(_ReceiptReport.ReceiptReportID);
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                }
            }
            return Json(false);
        }

        [AccessControl("Search")]
        [HttpGet]
        public ActionResult Search()
        {
            ViewBag.search = new Receipt_Report_Search();
            return View("Search");
        }

        public JsonResult ListPartial(Receipt_Report_Search search)
        {
            var DateFrom = search.DateFrom.ToDate(Language);
            var DateTo = search.DateTo.ToDate(Language);
            var _Receipts = db.ReceiptReports.Where(t => t.IsActive == true).ToList();
            var data = _Receipts.Select(r => new
            {
                r.ReceiptReportID,
                r.ReportNumber,
                r.Organization,
                ReceiptDate = r.ReceiptDate.ToDateString(Language),
                r.DeliveryPlace,
                r.SuggBillNumber,
                r.ObtainedFromContractor,
                Mem3DateVM = r.Mem3Date.ToDateString(Language),
            }).ToList();
            return Json(new
            {
                data = data.Skip(search.start).Take(search.length).ToList(),
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                draw = search.draw,
            });
        }

        [AccessControl("View")]
        public ActionResult View(int id = 0)
        {
            var _ReceptReport = db.ReceiptReports.Where(t => t.IsActive == true).SingleOrDefault(t => t.ReceiptReportID == id);
            if (_ReceptReport == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(_ReceptReport);
            return View("View", model);
        }

        [HttpPost]
        [AccessControl("Edit")]
        public JsonResult Update(Receipt_Report_Form_VM model)
        {
            if (ModelState.IsValid == false)
            {
                LogModelErrors();
                return Json(false);
            }
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    var _ReceptReport = db.ReceiptReports.Find(model.ReceiptReportID);
                    if (_ReceptReport == null) return Json(false);
                    _ReceptReport.ReportNumber = model.ReportNumber;
                    _ReceptReport.Organization = model.Organization;
                    _ReceptReport.ReceiptDate = model.ReceiptDateVM.ToDate(Language);
                    _ReceptReport.DeliveryPlace = model.DeliveryPlace;
                    _ReceptReport.SuggBillNumber = model.SuggBillNumber;
                    _ReceptReport.ObtainedFromContractor = model.ObtainedFromContractor;
                    _ReceptReport.Mem3Date = model.Mem3DateVM.ToDate(Language);
                    _ReceptReport.Details = model.Details;
                    _ReceptReport.IsActive = true;
                    _ReceptReport.LastUpdatedBy = AppUser.Id;
                    _ReceptReport.LastUpdatedDate = DateTime.Now;

                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "ReceiptReport",
                        ModfiedId = _ReceptReport.ReceiptReportID,
                        Action = "Update",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_ReceptReport),
                    });
                    db.SaveChanges();

                    db.ReceiptReportItems.Where(x => x.ReceiptReportID == _ReceptReport.ReceiptReportID).ToList().ForEach(x => x.IsActive = false);
                    db.SaveChanges();
                    if (model.ReceiptReportItems != null)
                    {
                        foreach (var row in model.ReceiptReportItems)
                        {
                            if (row != null)
                            {
                                string action = row.ID == 0 ? "Insert" : "Update";
                                if (row.ID == 0)
                                {
                                    row.ReceiptReportID = _ReceptReport.ReceiptReportID;
                                    row.InsertedBy = AppUser.Id;
                                    row.InsertedDate = DateTime.Now;
                                    row.IsActive = true;
                                    db.ReceiptReportItems.Add(row);
                                }
                                else
                                {
                                    var obj = db.ReceiptReportItems.Find(row.ID);
                                    if (obj != null)
                                    {
                                        obj.Quantity = row.Quantity;
                                        obj.UnitID = row.UnitID;
                                        obj.ItemDetails = row.ItemDetails;
                                        obj.UnitPrice = row.UnitPrice;
                                        obj.Remarks = row.Remarks;
                                        obj.LastUpdatedBy = AppUser.Id;
                                        obj.LastUpdatedDate = DateTime.Now;
                                        obj.IsActive = true;
                                    }
                                }
                                db.SaveChanges();

                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "ReceiptReportItems",
                                    ModfiedId = row.ID,
                                    Action = action,
                                    UserId = AppUser.Id,
                                    ModifiedTime = DateTime.Now,
                                    ModifiedData = JsonConvert.SerializeObject(row),
                                });
                                db.SaveChanges();
                            }
                        }
                    }
                    trans.Commit();
                    return Json(_ReceptReport.ReceiptReportID);
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                }
            }
            return Json(false);
        }

        [AccessControl("Delete")]
        public JsonResult Delete(int id = 0)
        {
            try
            {
                var obj = db.ReceiptReports.Find(id);
                if (obj != null)
                {
                    obj.IsActive = false;
                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "ReceiptReport",
                        ModfiedId = id,
                        Action = "Delete",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                    });
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Edit")]
        public ActionResult Edit(int id = 0)
        {
            CreateDropDown();
            var _ReceiptReport = db.ReceiptReports.SingleOrDefault(t => t.ReceiptReportID == id);
            if (_ReceiptReport == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(_ReceiptReport);
            return View("Form", model);
        }

    }
}