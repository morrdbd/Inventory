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
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
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
            var usageTypeList = AdminRepo.LookupValueList(Language, "UTYPE");
            ViewBag.UsageTypeDrp = new SelectList(usageTypeList, "ValueId", TextField);

            var unitList = AdminRepo.LookupValueList(Language, "UNIT");
            ViewBag.UnitDrp = new SelectList(unitList, "ValueId", TextField);

            var sizeList = AdminRepo.LookupValueList(Language, "PRODUCTSIZE");
            ViewBag.SizeDrp = new SelectList(sizeList, "ValueId", TextField);

            var originList = AdminRepo.LookupValueList(Language, "PRODUCTORIGIN");
            ViewBag.OriginDrp = new SelectList(originList, "ValueId", TextField);

            var brandList = AdminRepo.LookupValueList(Language, "PRODUCTBRAND");
            ViewBag.BrandDrp = new SelectList(brandList, "ValueId", TextField);


            //var unitList = db.LookupValues.Where(l => l.LookupCode == "UNIT" && l.IsActive == true).Select(l =>
            // new { l.ValueId, UnitName = Language == "prs" ? l.DrName : Language == "ps" ? l.PaName : l.EnName }).ToList();
            //ViewBag.UnitDrp = new SelectList(unitList, "ValueId", "UnitName");

            var ProductGroup = AdminRepo.LookupValueList(Language, "PRODUCTGROUP");
            ViewBag.ProductGroupDrp = new SelectList(ProductGroup, "ValueId", TextField);
        }

        private StockInVM CreateModel(StockIn model)
        {
            var _items = db.StockInItems.Where(i => i.IsActive == true && i.StockInID == model.StockInID).ToList();
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
                    ID = i.ID,
                    StockInID = i.StockInID,
                    ProductName = i.ProductName,
                    UnitName = AdminRepo.LookupName(Language, i.UnitID),
                    UnitID = i.UnitID,
                    GroupID = i.GroupID,
                    GroupName = AdminRepo.LookupName(Language, i.GroupID),
                    CategoryID = i.CategoryID,
                    CategoryName = AdminRepo.LookupName(Language, i.CategoryID),
                    SizeID = i.SizeID,
                    SizeName = AdminRepo.LookupName(Language, i.SizeID),
                    OriginID = i.OriginID,
                    OriginName = AdminRepo.LookupName(Language, i.OriginID),
                    BrandID = i.BrandID,
                    BrandName = AdminRepo.LookupName(Language, i.BrandID),
                    Remarks = i.Remarks,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    UsageTypeID = i.UsageTypeID,
                    ProductCode = i.ProductCode
                }
                ).ToList()
            };
        }

        [HttpPost]
        [AccessControl("Add")]
        public JsonResult Insert(StockIn_Form_VM model)
        {
            var _StockinDate = model.StockInDateForm.ToDate(Language);
            var _OrderDate = model.OrderDateForm.ToDate(Language);
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
                        StockInDate = _StockinDate,
                        OrderDate = _OrderDate,
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
                                    UnitID = row.UnitID,
                                    ProductName = row.ProductName,
                                    GroupID = row.GroupID,
                                    CategoryID = row.CategoryID,
                                    BrandID = row.BrandID,
                                    SizeID = row.SizeID,
                                    OriginID = row.OriginID,
                                    UnitPrice = row.UnitPrice,
                                    Quantity = row.Quantity,
                                    AvailableQuantity = row.Quantity,
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
            ViewBag.search = new Durable_StockIn_Search();
            return View("Search");
        }

        [AccessControl("Search")]
        public JsonResult ListPartial(Durable_StockIn_Search search)
        {
            var StockInDateFrom = search.StockInDateFrom.ToDate(Language);
            var StockInDateTo = search.StockInDateTo.ToDate(Language);
            var RequestDateFrom = search.RequestDateFrom.ToDate(Language);
            var RequestDateTo = search.RequestDateTo.ToDate(Language);
            var _Receipts = db.StockIns.Where(t => t.IsActive == true).OrderByDescending(s => s.StockInDate).ToList();
            if(StockInDateFrom != null)
            {
                _Receipts = _Receipts.Where(r => r.StockInDate >= StockInDateFrom).ToList();
            }
            if (StockInDateTo != null)
            {
                _Receipts = _Receipts.Where(r => r.StockInDate <= StockInDateTo).ToList();
            }
            if (RequestDateFrom != null)
            {
                _Receipts = _Receipts.Where(r => r.OrderDate >= RequestDateFrom).ToList();
            }
            if (RequestDateTo != null)
            {
                _Receipts = _Receipts.Where(r => r.OrderDate <= RequestDateTo).ToList();
            }
            if (!string.IsNullOrWhiteSpace(search.ReportNumber))
            {
                _Receipts = _Receipts.Where(r => r.M7Number == search.ReportNumber).ToList();
            }
            if (!string.IsNullOrWhiteSpace(search.RequestNumber))
            {
                _Receipts = _Receipts.Where(r => r.OrderNumber == search.RequestNumber).ToList();
            }
            var data = _Receipts.Select(r => new
            {
                r.StockInID,
                r.M7Number,
                StockInDate = r.StockInDate.ToDateString(Language),
                r.ContractorName,
                r.DeliveryPlace,
                r.OrderNumber,
                OrderDate = r.OrderDate.ToDateString(Language),
                r.Details
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
            var _record = db.StockIns.Where(t => t.IsActive == true && t.StockInID == id).FirstOrDefault();
            if (_record == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(_record);
            return View("View", model);
        }

        [HttpPost]
        [AccessControl("Edit")]
        public JsonResult Update(StockIn_Form_VM model)
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
                    var _StockIn = db.StockIns.Find(model.StockInID);
                    if (_StockIn == null) return Json(false);
                    _StockIn.M7Number = model.M7Number;
                    _StockIn.StockInDate = model.StockInDateForm.ToDate(Language);
                    _StockIn.ContractorName = model.ContractorName;
                    _StockIn.DeliveryPlace = model.DeliveryPlace;
                    _StockIn.OrderNumber = model.OrderNumber;
                    _StockIn.OrderDate = model.OrderDateForm.ToDate(Language);
                    _StockIn.Details = model.Details;
                    _StockIn.IsActive = true;
                    _StockIn.LastUpdatedBy = AppUser.Id;
                    _StockIn.LastUpdatedDate = DateTime.Now;
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "StockIns",
                        ModfiedId = _StockIn.StockInID,
                        Action = "Update",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_StockIn),
                    });
                    db.SaveChanges();

                    db.StockInItems.Where(x => x.StockInID == _StockIn.StockInID).ToList().ForEach(x => x.IsActive = false);

                    db.SaveChanges();
                    if (model.StockInItems != null)
                    {
                        foreach (var row in model.StockInItems)
                        {
                            if (row != null)
                            {
                                string action = row.ID == 0 ? "Insert" : "Update";
                                if (row.ID == 0)
                                {
                                    var _newItem = new StockInItem() {
                                        StockInID = _StockIn.StockInID,
                                        ProductCode = row.ProductCode,
                                        UnitPrice = row.UnitPrice,
                                        InsertedBy = AppUser.Id,
                                        InsertedDate = DateTime.Now,
                                        IsActive = true
                                    };
                                   
                                    db.StockInItems.Add(_newItem);
                                }
                                else
                                {
                                    var obj = db.StockInItems.Find(row.ID);
                                    if (obj != null)
                                    {
                                        obj.ProductCode = row.ProductCode;
                                        obj.ProductName = row.ProductName;
                                        obj.UnitID = row.UnitID;
                                        obj.OriginID = row.OriginID;
                                        obj.GroupID = row.GroupID;
                                        obj.CategoryID = row.CategoryID;
                                        obj.BrandID = row.BrandID;
                                        obj.SizeID = row.SizeID;
                                        obj.UnitPrice = row.UnitPrice;
                                        obj.Quantity = row.Quantity;
                                        obj.Remarks = row.Remarks;
                                        obj.LastUpdatedBy = AppUser.Id;
                                        obj.LastUpdatedDate = DateTime.Now;
                                        obj.IsActive = true;
                                    }
                                }
                                db.SaveChanges();

                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "StockInItems",
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
                    return Json(_StockIn.StockInID);
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
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    var _stockIn = db.StockIns.Find(id);
                    if (_stockIn != null)
                    {
                        _stockIn.IsActive = false;
                        db.ActivityLogs.Add(new ActivityLog
                        {
                            ModifiedTable = "StockIns",
                            ModfiedId = id,
                            Action = "Delete",
                            UserId = AppUser.Id,
                            ModifiedTime = DateTime.Now,
                        });
                        db.SaveChanges();
                        var _stockInItems = db.StockInItems.Where(s => s.StockInID == _stockIn.StockInID && s.IsActive == true).ToList();
                        db.StockInItems.Where(s => s.StockInID == _stockIn.StockInID).ToList().ForEach(i => i.IsActive = false);
                    }
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
            return Json(false,JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Edit")]
        public ActionResult Edit(int id = 0)
        {
            CreateDropDown();
            var _stockIn = db.StockIns.SingleOrDefault(t => t.StockInID == id);
            if (_stockIn == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(_stockIn);
            return View("Form", model);
        }

        [AccessControl("Search")]
        public ActionResult ItemInWarehouse()
        {
            ViewBag.search = new Item_In_Warehouse_Search();
            return View("ItemInWarehouse");
        }

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
        public JsonResult SaveScanImage(FileUpload model)
        {
            if(!ModelState.IsValid)
            {
                return Json(false);
            }
            try
            {
                string FileName = Path.GetFileNameWithoutExtension(model.FileContent.FileName);

                string FileExtension = Path.GetExtension(model.FileContent.FileName);

                FileName = model.RecordID + FileExtension;
                string StockInScanFolder = ConfigurationManager.AppSettings["StockInScan"].ToString();
                var filePathForDB = StockInScanFolder + FileName;
                var UploadedFilePath = Server.MapPath(StockInScanFolder + FileName);

                var modelInDb = db.StockIns.Where(s => s.StockInID == model.RecordID).FirstOrDefault();
                modelInDb.FilePath = filePathForDB;
                db.SaveChanges();
                model.FileContent.SaveAs(UploadedFilePath);
                return Json(true);
            }
            catch(Exception e)
            {
                return Json(false);
            }
        }


    }
}