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
    public class StockInController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();
        [AccessControl("Add")]

        public ActionResult Add()
        {
            CreateDropDown();
            return View("Form", new StockInVM());
        }

        private void CreateDropDown()
        {
            //var unitList = db.LookupValues.Where(l => l.LookupCode == "UNIT" && l.IsActive == true).Select(l =>
            // new { l.ValueId, UnitName = Language == "prs" ? l.DrName : Language == "ps" ? l.PaName : l.EnName }).ToList();
            //ViewBag.UnitDrp = new SelectList(unitList, "ValueId", "UnitName");

            var UsageType = AdminRepo.LookupValueList(Language, "UTYPE");
            ViewBag.UsageTypeDrp = new SelectList(UsageType, "ValueId", TextField);
        }

        private StockInVM CreateModel(StockIn model)
        {
            return new StockInVM()
            {
                StockInID = model.StockInID,
                M7Number = model.M7Number,
                StockInDateForm = model.StockInDate.ToDateString(Language),
                DeliveryPlace = model.DeliveryPlace,
                OrderNumber = model.OrderNumber,
                ContractorName = model.ContractorName,
                OrderDateForm = model.OrderDate.ToDateString(Language),
                Details = model.Details,
                StockInItems = db.StockInItems.Where(i => i.IsActive == true && i.StockInID == model.StockInID)
                .ToList().Select(i => new StockInItemVM
                {
                    ID=  i.ID,
                    ProductName = db.Products.Where(p => p.IsActive == true && p.ProductCode == i.ProductCode).Select(p => p.ProductName).FirstOrDefault(),
                    UnitName = AdminRepo.LookupName(Language, db.Products.Where(p=>p.IsActive == true && p.ProductCode == i.ProductCode).Select(p=>p.UnitID).FirstOrDefault()),
                    Quantity = i.Quantity,
                    Remarks = i.Remarks,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.Quantity * i.UnitPrice
                }
                ).ToList()
            };
        }

        [HttpPost]
        [AccessControl("Add")]
        public JsonResult Insert(StockIn_Form_VM model)
        {
            //var _StockinDate = model.StockInDateForm.ToDate(Language);
            //var _OrderDate = model.OrderDateForm.ToDate(Language);
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid == false)
            {
                LogModelErrors();
                return Json(false);
            }
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    var _stockIn = new StockIn
                    {
                        M7Number = model.M7Number,
                        OrderNumber = model.OrderNumber,
                        StockInDate = model.StockInDateForm.ToDate(Language),
                        OrderDate = model.OrderDateForm.ToDate(Language),
                        DeliveryPlace = model.DeliveryPlace,
                        ContractorName = model.ContractorName,
                        Details = model.Details,                       
                        InsertedBy = AppUser.Id,
                        InsertedDate = DateTime.Now,
                        IsActive = true,
                    };

                    db.StockIns.Add(_stockIn);
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "StockIns",
                        ModfiedId = _stockIn.StockInID,
                        Action = "Insert",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_stockIn),
                    });
                    db.SaveChanges();

                    if (model.StockInItems != null)
                    {
                        foreach (var row in model.StockInItems)
                        {
                            if (row != null)
                            {
                                var _item = new StockInItem()
                                {
                                    StockInID = _stockIn.StockInID,
                                    ProductCode = row.ProductCode,
                                    Quantity = row.Quantity,
                                    UnitPrice = row.UnitPrice,
                                    Remarks = row.Remarks,
                                    InsertedBy = AppUser.Id,
                                    InsertedDate = DateTime.Now,
                                    IsActive = true
                                };
                                db.StockInItems.Add(_item);
                                db.SaveChanges();
                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "StockInItems",
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
                    return Json(_stockIn.StockInID);
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

        //[AccessControl("Search")]
        //public JsonResult ListPartial(Receipt_Report_Search search)
        //{
        //    var DateFrom = search.DateFrom.ToDate(Language);
        //    var DateTo = search.DateTo.ToDate(Language);
        //    var _Receipts = db.StockIns.Where(t => t.IsActive == true).ToList();
        //    var data = _Receipts.Select(r => new
        //    {
        //        r.ReceiptReportID,
        //        r.ReportNumber,
        //        r.Organization,
        //        ReceiptDate = r.ReceiptDate.ToDateString(Language),
        //        r.DeliveryPlace,
        //        r.SuggBillNumber,
        //        r.ObtainedFromContractor,
        //        Mem3DateVM = r.Mem3Date.ToDateString(Language),
        //    }).ToList();
        //    return Json(new
        //    {
        //        data = data.Skip(search.start).Take(search.length).ToList(),
        //        recordsTotal = data.Count,
        //        recordsFiltered = data.Count,
        //        draw = search.draw,
        //    });
        //}

        [AccessControl("Add")]
        public JsonResult ProductsByUsageType(int usageTypeID = 0)
        {
            var _Product = db.Products.Where(t => t.IsActive == true && t.UsageTypeID == usageTypeID).ToList();
            var customList = _Product.Select(p => new
            {
                p.ProductCode,
                p.ProductName 
                //ProductName = p.ProductName +" "+ AdminRepo.LookupName(Language, p.SizeID) + " " + AdminRepo.LookupName(Language, p.CategoryID) + " "+ AdminRepo.LookupName(Language, p.GroupID) 
            }).ToList();
            var _ProductsList = new SelectList(customList, "ProductCode", "ProductName");
            return Json(_ProductsList, JsonRequestBehavior.AllowGet);
        }
        [AccessControl("Add")]
        public JsonResult LoadProductInfo(int productCode = 0)
        {
            var _Product = db.Products.Where(t => t.IsActive == true && t.ProductCode == productCode).ToList();
            var productInfo = _Product.Select(p => new
            {
                p.ProductCode,
                ProductName = p.ProductName ,
                ProductSize = AdminRepo.LookupName(Language, p.SizeID) ,
                ProductCategory = AdminRepo.LookupName(Language, p.CategoryID) ,
                ProductGroup = AdminRepo.LookupName(Language, p.GroupID) ,
                ProductOrigin = AdminRepo.LookupName(Language, p.OriginID),
                UnitName = AdminRepo.LookupName(Language,p.UnitID)
            }).FirstOrDefault();
            return Json(productInfo, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("View")]
        public ActionResult View(int id = 0)
        {
            var _record = db.StockIns.Where(t => t.IsActive == true && t.StockInID == id).FirstOrDefault();
            if (_record == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(_record);
            return View("View", model);
        }

        //[HttpPost]
        //[AccessControl("Edit")]
        //public JsonResult Update(Receipt_Report_Form_VM model)
        //{
        //    if (ModelState.IsValid == false)
        //    {
        //        LogModelErrors();
        //        return Json(false);
        //    }
        //    using (var trans = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var _ReceptReport = db.ReceiptReports.Find(model.ReceiptReportID);
        //            if (_ReceptReport == null) return Json(false);
        //            _ReceptReport.ReportNumber = model.ReportNumber;
        //            _ReceptReport.Organization = model.Organization;
        //            _ReceptReport.ReceiptDate = model.ReceiptDateVM.ToDate(Language);
        //            _ReceptReport.DeliveryPlace = model.DeliveryPlace;
        //            _ReceptReport.SuggBillNumber = model.SuggBillNumber;
        //            _ReceptReport.ObtainedFromContractor = model.ObtainedFromContractor;
        //            _ReceptReport.Mem3Date = model.Mem3DateVM.ToDate(Language);
        //            _ReceptReport.Details = model.Details;
        //            _ReceptReport.IsActive = true;
        //            _ReceptReport.LastUpdatedBy = AppUser.Id;
        //            _ReceptReport.LastUpdatedDate = DateTime.Now;

        //            db.SaveChanges();

        //            db.ActivityLogs.Add(new ActivityLog
        //            {
        //                ModifiedTable = "ReceiptReport",
        //                ModfiedId = _ReceptReport.ReceiptReportID,
        //                Action = "Update",
        //                UserId = AppUser.Id,
        //                ModifiedTime = DateTime.Now,
        //                ModifiedData = JsonConvert.SerializeObject(_ReceptReport),
        //            });
        //            db.SaveChanges();

        //            db.ReceiptReportItems.Where(x => x.ReceiptReportID == _ReceptReport.ReceiptReportID).ToList().ForEach(x => x.IsActive = false);
        //            db.SaveChanges();
        //            if (model.ReceiptReportItems != null)
        //            {
        //                foreach (var row in model.ReceiptReportItems)
        //                {
        //                    if (row != null)
        //                    {
        //                        string action = row.ID == 0 ? "Insert" : "Update";
        //                        if (row.ID == 0)
        //                        {
        //                            row.ReceiptReportID = _ReceptReport.ReceiptReportID;
        //                            row.InsertedBy = AppUser.Id;
        //                            row.InsertedDate = DateTime.Now;
        //                            row.IsActive = true;
        //                            db.ReceiptReportItems.Add(row);
        //                        }
        //                        else
        //                        {
        //                            var obj = db.ReceiptReportItems.Find(row.ID);
        //                            if (obj != null)
        //                            {
        //                                obj.Quantity = row.Quantity;
        //                                obj.UnitID = row.UnitID;
        //                                obj.ItemDetails = row.ItemDetails;
        //                                obj.UnitPrice = row.UnitPrice;
        //                                obj.Remarks = row.Remarks;
        //                                obj.LastUpdatedBy = AppUser.Id;
        //                                obj.LastUpdatedDate = DateTime.Now;
        //                                obj.IsActive = true;
        //                            }
        //                        }
        //                        db.SaveChanges();

        //                        db.ActivityLogs.Add(new ActivityLog
        //                        {
        //                            ModifiedTable = "ReceiptReportItems",
        //                            ModfiedId = row.ID,
        //                            Action = action,
        //                            UserId = AppUser.Id,
        //                            ModifiedTime = DateTime.Now,
        //                            ModifiedData = JsonConvert.SerializeObject(row),
        //                        });
        //                        db.SaveChanges();
        //                    }
        //                }
        //            }
        //            trans.Commit();
        //            return Json(_ReceptReport.ReceiptReportID);
        //        }
        //        catch (Exception e)
        //        {
        //            trans.Rollback();
        //            Elmah.ErrorSignal.FromCurrentContext().Raise(e);
        //        }
        //    }
        //    return Json(false);
        //}

        //[AccessControl("Delete")]
        //public JsonResult Delete(int id = 0)
        //{
        //    try
        //    {
        //        var obj = db.ReceiptReports.Find(id);
        //        if (obj != null)
        //        {
        //            obj.IsActive = false;
        //            db.ActivityLogs.Add(new ActivityLog
        //            {
        //                ModifiedTable = "ReceiptReport",
        //                ModfiedId = id,
        //                Action = "Delete",
        //                UserId = AppUser.Id,
        //                ModifiedTime = DateTime.Now,
        //            });
        //            db.SaveChanges();
        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(e);
        //    }
        //    return Json(false, JsonRequestBehavior.AllowGet);
        //}

        //[AccessControl("Edit")]
        //public ActionResult Edit(int id = 0)
        //{
        //    CreateDropDown();
        //    var _ReceiptReport = db.ReceiptReports.SingleOrDefault(t => t.ReceiptReportID == id);
        //    if (_ReceiptReport == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var model = CreateModel(_ReceiptReport);
        //    return View("Form", model);
        //}

        //[AccessControl("Search")]
        //public ActionResult ItemInWarehouse()
        //{
        //    ViewBag.search = new Item_In_Warehouse_Search();
        //    return View("ItemInWarehouse");
        //}

        //[AccessControl("Search")]
        //public JsonResult ListItemInWarehouse(Item_In_Warehouse_Search search)
        //{
        //    var DateFrom = search.DateFrom.ToDate(Language);
        //    var DateTo = search.DateTo.ToDate(Language);
        //    var _Receipts = db.ReceiptReports.Where(t => t.IsActive == true).ToList();
        //    var data = _Receipts.Select(r => new
        //    {
        //        r.ReceiptReportID,
        //        r.ReportNumber,
        //        r.Organization,
        //        ReceiptDate = r.ReceiptDate.ToDateString(Language),
        //        r.DeliveryPlace,
        //        r.SuggBillNumber,
        //        r.ObtainedFromContractor,
        //        Mem3DateVM = r.Mem3Date.ToDateString(Language),
        //    }).ToList();
        //    return Json(new
        //    {
        //        data = data.Skip(search.start).Take(search.length).ToList(),
        //        recordsTotal = data.Count,
        //        recordsFiltered = data.Count,
        //        draw = search.draw,
        //    });
        //}

    }
}