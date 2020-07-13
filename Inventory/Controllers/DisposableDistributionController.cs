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
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    [AccessControl]
    public class DisposableDistributionController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();
        [AccessControl("Add")]
        public ActionResult Add()
        {
            CreateDropDown();
            ViewBag.itemSearch = new Item_Search();
            return View("Form", new DisposableDistributionVM());
        }

        private void CreateDropDown()
        {
            var usageTypeList = AdminRepo.LookupValueList(Language, "UTYPE");
            ViewBag.UsageTypeDrp = new SelectList(usageTypeList, "ValueId", TextField);

            var unitList = AdminRepo.LookupValueList(Language, "UNIT");
            ViewBag.UnitDrp = new SelectList(unitList, "ValueId", TextField);

            var sizeList = AdminRepo.LookupValueList(Language, "ITEMSIZE");
            ViewBag.SizeDrp = new SelectList(sizeList, "ValueId", TextField);

            var originList = AdminRepo.LookupValueList(Language, "ITEMORIGIN");
            ViewBag.OriginDrp = new SelectList(originList, "ValueId", TextField);

            var brandList = AdminRepo.LookupValueList(Language, "ITEMBRAND");
            ViewBag.BrandDrp = new SelectList(brandList, "ValueId", TextField);


            var disposableValueID = AdminRepo.ValueID("Disposable");
            var ItemGroup = AdminRepo.LookupValueList(Language, "ITEMGROUP");
            ItemGroup = ItemGroup.Where(l=>l.GroupValueId == disposableValueID).ToList();
            ViewBag.GroupDrp = new SelectList(ItemGroup, "ValueId", TextField);

            var departmentList = db.Departments.Where(d => d.IsActive == true).Select(d =>
                new { d.DepartmentID, DepartmentName = Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName }).ToList();
            ViewBag.DepartmentDrp = new SelectList(departmentList, "DepartmentID", "DepartmentName");
        }

        private DisposableDistributionVM CreateModel(DisposableDistribution model)
        {
            return new DisposableDistributionVM
            {
                DisposableDistributionID = model.DisposableDistributionID,
                DocumentIssueNumber = model.DocumentIssueNumber,
                DocumentIssuedDate = model.DocumentIssuedDate.ToDateString(Language),
                OrderNumber = model.OrderNumber,
                OrderDate = model.OrderDate.ToDateString(Language),
                DepartmentID = model.DepartmentID,
                DepartmentName = db.Departments.Where(d => d.IsActive == true && d.DepartmentID == model.DepartmentID).Select(d=> Language == "dr" ? d.DrName : Language == "pa" ? d.PaName : d.EnName).FirstOrDefault(),
                Details = model.Details,
                FilePath = model.FilePath,
                DistributionItems = db.DisposableDistributionItems.Where(i =>i.DisposableDistributionID == model.DisposableDistributionID)
                .ToList().Select(i => new DisposableDistributionItemVM
                {
                    ID = i.ID,
                    StockInItemID = i.StockInItemID,
                    BarCode = db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.BarCode).FirstOrDefault(),
                    DisposableDistributionID = i.DisposableDistributionID,
                    ItemCode = db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.ItemCode).FirstOrDefault(),
                    ItemName = db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.ItemName).FirstOrDefault(),
                    ItemCategoryName = AdminRepo.LookupName(Language,db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.CategoryID).FirstOrDefault()),
                    ItemGroupName = AdminRepo.LookupName(Language, db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.GroupID).FirstOrDefault()),
                    UnitName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.StockInItemID == i.StockInItemID).Select(p => p.UnitID).FirstOrDefault()),
                    ItemSizeName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.StockInItemID == i.StockInItemID).Select(p => p.SizeID).FirstOrDefault()),
                    ItemOriginName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.StockInItemID == i.StockInItemID).Select(p => p.OriginID).FirstOrDefault()),
                    BrandName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.StockInItemID == i.StockInItemID).Select(p => p.BrandID).FirstOrDefault()),
                    Quantity = i.Quantity,
                    UnitPrice = db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.UnitPrice).FirstOrDefault(),
                }
                ).ToList(),
            };
        }

        //Load search result
        [AccessControl("Search")]
        public JsonResult DisposableItemPartialList(Item_Search search)
        {
            var disposableValueId = AdminRepo.ValueID("Disposable");
            var query = db.StockInItems.Where(i => i.IsActive == true && i.AvailableQuantity != 0 
            && i.UsageTypeID == disposableValueId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.BarCode))
            {
                query = query.Where(c => c.BarCode == search.BarCode);
            }
            if (!string.IsNullOrWhiteSpace(search.ItemName))
            {
                query = query.Where(c => c.ItemName.Contains(search.ItemName));
            }
            if (!string.IsNullOrWhiteSpace(search.ItemCode))
            {
                query = query.Where(c => c.ItemCode.Contains(search.ItemCode));
            }
            if (search.UsageTypeID != null)
            {
                query = query.Where(c => c.UsageTypeID == search.UsageTypeID);
            }
            if (search.GroupID != null)
            {
                query = query.Where(c => c.GroupID == search.GroupID);
            }
            if (search.CategoryID != null)
            {
                query = query.Where(c => c.CategoryID == search.CategoryID);
            }
            if (search.UnitID != null)
            {
                query = query.Where(c => c.UnitID == search.UnitID);
            }
            if (search.UnitID != null)
            {
                query = query.Where(c => c.CategoryID == search.UnitID);
            }
            if (search.SizeID != null)
            {
                query = query.Where(c => c.SizeID == search.SizeID);
            }
            if (search.OriginID != null)
            {
                query = query.Where(c => c.OriginID == search.OriginID);
            }
            if (search.BrandID != null)
            {
                query = query.Where(c => c.BrandID == search.BrandID);
            }
            ViewBag.search = search;
            var items = query.ToList();
              var data = items.Select(i => new StockInItemVM
            {
                BarCode = i.BarCode,
                StockInItemID = i.StockInItemID,
                ItemName = i.ItemName,
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
                AvailableQuantity = i.AvailableQuantity,
                UsageTypeID = i.UsageTypeID,
                ItemCode = i.ItemCode
            }
            ).ToList();
            var tes = data.ToList();
            return Json(new
            {
                data = data.Skip(search.start).Take(search.length).ToList(),
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                draw = search.draw,
            });
        }

        [HttpPost]
        [AccessControl("Add")]
        public JsonResult Insert(DisposableDistributionVM model)
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
                    var _distribution = new DisposableDistribution
                    {
                        DocumentIssueNumber = model.DocumentIssueNumber,
                        DocumentIssuedDate = model.DocumentIssuedDate.ToDate(Language),
                        DepartmentID = model.DepartmentID,
                        OrderNumber = model.OrderNumber,
                        OrderDate = model.OrderDate.ToDate(Language),
                        Details = model.Details,
                        InsertedBy = AppUser.Id,
                        InsertedDate = DateTime.Now,
                        IsActive = true,
                    };

                    db.DisposableDistributions.Add(_distribution);
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "DisposableDistribution",
                        ModfiedId = _distribution.DisposableDistributionID,
                        Action = "Insert",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_distribution),
                    });
                    db.SaveChanges();

                    if (model.DistributionItems != null)
                    {
                        foreach (var row in model.DistributionItems)
                        {
                            if (row != null)
                            {
                                var _item = new DisposableDistributionItem()
                                {
                                    DisposableDistributionID = _distribution.DisposableDistributionID,
                                    StockInItemID = row.StockInItemID,
                                    Quantity = row.Quantity,
                                };

                                db.DisposableDistributionItems.Add(_item);
                                db.SaveChanges();

                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "DisposableDistributionItem",
                                    ModfiedId = row.ID,
                                    Action = "Insert",
                                    UserId = AppUser.Id,
                                    ModifiedTime = DateTime.Now,
                                    ModifiedData = JsonConvert.SerializeObject(row),
                                });
                                db.SaveChanges();
                                //minus item from item in hand
                                var _stockInHand = db.StockInItems.Where(s => s.StockInItemID == row.StockInItemID).First();
                                if (_stockInHand.AvailableQuantity < _item.Quantity)
                                {
                                    return Json(false);
                                }
                                _stockInHand.AvailableQuantity -= _item.Quantity;

                                db.SaveChanges();
                            }
                        }
                    }

                    trans.Commit();
                    return Json(_distribution.DisposableDistributionID);
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
            ViewBag.search = new DisposableDistributionItemsSearch();
            CreateDropDown();
            return View("Search");
        }

        [AccessControl("View")]
        public ActionResult View(int id = 0)
        {
            var obj = db.DisposableDistributions.Where(t => t.IsActive == true).SingleOrDefault(t => t.DisposableDistributionID == id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(obj);
            return View(model);
        }

        [HttpPost]
        [AccessControl("Edit")]
        public JsonResult Update(DisposableDistributionVM model)
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
                    var _distribution = db.DisposableDistributions.Where(d=>d.IsActive == true && d.DisposableDistributionID == model.DisposableDistributionID).FirstOrDefault();
                    if (_distribution == null) return Json(false);
                    _distribution.DepartmentID = model.DepartmentID;
                    _distribution.DocumentIssueNumber = model.DocumentIssueNumber;
                    _distribution.DocumentIssuedDate = model.DocumentIssuedDate.ToDate(Language);
                    _distribution.OrderNumber = model.OrderNumber;
                    _distribution.OrderDate = model.OrderDate.ToDate(Language);
                    _distribution.Details = model.Details;
                    _distribution.LastUpdatedBy = AppUser.Id;
                    _distribution.LastUpdatedDate = DateTime.Now;

                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "DisposableDistributions",
                        ModfiedId = _distribution.DisposableDistributionID,
                        Action = "Update",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_distribution),
                    });
                    db.SaveChanges();
                    var distributedItems = db.DisposableDistributionItems.Where(d => d.DisposableDistributionID == _distribution.DisposableDistributionID).ToList();
                    foreach(var dItem in distributedItems)
                    {
                        var stockInItem = db.StockInItems.Find(dItem.StockInItemID);
                        if(stockInItem != null)
                        {
                            stockInItem.AvailableQuantity += dItem.Quantity;
                            db.DisposableDistributionItems.Remove(dItem);
                        }
                    }
                    foreach (var dItem in model.DistributionItems)
                    {
                        var stockInItem = db.StockInItems.Find(dItem.StockInItemID);
                        if (stockInItem != null && dItem.Quantity <= stockInItem.AvailableQuantity)
                        {
                            stockInItem.AvailableQuantity -= dItem.Quantity;
                            var dItemTableObj = new DisposableDistributionItem
                            {
                                DisposableDistributionID = _distribution.DisposableDistributionID,
                                Quantity = dItem.Quantity,
                                StockInItemID = dItem.StockInItemID,
                            };
                            db.DisposableDistributionItems.Add(dItemTableObj);
                        }
                    }
                    db.SaveChanges();
                    trans.Commit();
                    return Json(_distribution.DisposableDistributionID);
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
                    var _distribution = db.DisposableDistributions.Where(d => d.IsActive == true && d.DisposableDistributionID == id).FirstOrDefault();
                    if (_distribution == null) return Json(false);
                    _distribution.IsActive = false;
                    db.DisposableDistributions.Remove(_distribution);
                    db.SaveChanges();
       
                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "DisposableDistributions",
                        ModfiedId = _distribution.DisposableDistributionID,
                        Action = "Delete",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_distribution),
                    });
                    db.SaveChanges();
                    var distributedItems = db.DisposableDistributionItems.Where(d => d.DisposableDistributionID == _distribution.DisposableDistributionID).ToList();
                    foreach (var dItem in distributedItems)
                    {
                        var stockInItem = db.StockInItems.Find(dItem.StockInItemID);
                        if (stockInItem != null)
                        {
                            stockInItem.AvailableQuantity += dItem.Quantity;
                            var dItemTobeDeleted = db.DisposableDistributionItems.Find(dItem.ID);
                            db.DisposableDistributionItems.Remove(dItemTobeDeleted);
                            db.ActivityLogs.Add(new ActivityLog
                            {
                                ModifiedTable = "DisposableDistributionItems",
                                ModfiedId = dItem.ID,
                                Action = "Delete",
                                UserId = AppUser.Id,
                                ModifiedTime = DateTime.Now,
                            });
                        }
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

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItem(GetItemVM model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var obj = db.StockInItems.Where(i => i.StockInItemID == model.StockInItemID && i.IsActive == true).FirstOrDefault();
                if (obj != null && obj.AvailableQuantity >= model.Quantity)
                {
                    var item = new {
                        obj.BarCode,
                        obj.StockInItemID,
                        model.Quantity,
                        obj.ItemName,
                        obj.ItemCode,
                        obj.UnitPrice,
                        obj.UsageTypeID,
                        UsageTypeName = AdminRepo.LookupName(Language, obj.UsageTypeID),
                        UnitName = AdminRepo.LookupName(Language, obj.UnitID),
                        GroupName = AdminRepo.LookupName(Language, obj.GroupID),
                        CategoryName = AdminRepo.LookupName(Language, obj.CategoryID),
                        SizeName = AdminRepo.LookupName(Language, obj.SizeID),
                        OriginName = AdminRepo.LookupName(Language, obj.OriginID),
                        BrandName = AdminRepo.LookupName(Language, obj.BrandID),
                        };
                    return Json(item, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
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
            ViewBag.itemSearch = new Item_Search();
            var distribution = db.DisposableDistributions.SingleOrDefault(t => t.DisposableDistributionID == id);
            if (distribution == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(distribution);
            return View("Form", model);
        }

        [AccessControl("Search")]
        public JsonResult DistributedItemsListPartial(DisposableDistributedSearch model)
        {
            var _dateFrom = model.DateFrom.ToDate(Language);
            var _dateTo = model.DateTo.ToDate(Language);
            var query = (from d in db.DisposableDistributions
                                     join dItem in db.DisposableDistributionItems on d.DisposableDistributionID equals dItem.DisposableDistributionID
                                     join sItem in db.StockInItems on dItem.StockInItemID equals sItem.StockInItemID
                                     join dep in db.Departments on d.DepartmentID equals dep.DepartmentID
                                     where d.IsActive == true
                                     select new
                                     {
                                        sItem.BarCode,
                                        d.DisposableDistributionID,
                                        distributionItemID = dItem.ID,
                                        dItem.StockInItemID,
                                        d.DepartmentID,
                                        DepartmentName = Language == "prs" ? dep.DrName : Language == "ps" ? dep.PaName : dep.EnName,                           
                                        d.DocumentIssueNumber,
                                        d.DocumentIssuedDate,
                                        d.OrderNumber,
                                        d.OrderDate,
                                        dItem.Quantity,
                                        sItem.UnitID,
                                         sItem.UsageTypeID,
                                         sItem.GroupID,
                                         sItem.CategoryID,
                                         sItem.ItemName,
                                         sItem.ItemCode,
                                         sItem.SizeID,
                                         sItem.OriginID,
                                         sItem.BrandID,
                                         sItem.UnitPrice,
                                         Total = dItem.Quantity * sItem.UnitPrice
                                     });
            if (!string.IsNullOrWhiteSpace(model.BarCode))
            {
                query = query.Where(c => c.BarCode == model.BarCode);
            }
            if (model.DepartmentID != null)
            {
                query = query.Where(c => c.DepartmentID == model.DepartmentID);
            }
            if (model.GroupID != null)
            {
                query = query.Where(r => r.GroupID == model.GroupID);
            }
            if (model.CategoryID != null)
            {
                query = query.Where(r => r.CategoryID == model.CategoryID);
            }
            if (model.ItemName != null)
            {
                query = query.Where(r => r.ItemName.Contains(model.ItemName));
            }
            if (model.ItemCode != null)
            {
                query = query.Where(r => r.ItemCode.Contains(model.ItemCode));
            }
            if (model.SizeID != null)
            {
                query = query.Where(r => r.SizeID == model.SizeID);
            }
            if (model.OriginID != null)
            {
                query = query.Where(r => r.OriginID == model.OriginID);
            }
            if (model.BrandID != null)
            {
                query = query.Where(r => r.BrandID == model.BrandID);
            }
            if (_dateFrom != null)
            {
                query = query.Where(c => c.DocumentIssuedDate >= _dateFrom);
            }
            if (_dateTo != null)
            {
                query = query.Where(c => c.DocumentIssuedDate <= _dateTo);
            }
            ViewBag.search = model;
            var queryResult = query.OrderByDescending(i => i.DocumentIssuedDate).ToList();
            var data = queryResult.Select(i => new {
                i.BarCode,
                i.DisposableDistributionID,
                i.distributionItemID,
                i.StockInItemID,
                DepartmentName = db.Departments.Where(d => d.DepartmentID == i.DepartmentID).Select(d => Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                i.DocumentIssueNumber,
                DocumentIssuedDate = i.DocumentIssuedDate.ToDateString(Language),
                i.OrderNumber,
                OrderDate = i.OrderDate.ToDateString(Language),
                i.Quantity,
                UnitName = AdminRepo.LookupName(Language, i.UnitID),
                GroupName = AdminRepo.LookupName(Language, i.GroupID),
                CategoryName = AdminRepo.LookupName(Language, i.CategoryID),
                i.ItemName,
                i.ItemCode,
                SizeName = AdminRepo.LookupName(Language, i.SizeID),
                OriginName = AdminRepo.LookupName(Language, i.OriginID),
                BrandName = AdminRepo.LookupName(Language, i.BrandID),
                i.UnitPrice,
                Total = i.Quantity * i.UnitPrice
            });
   
            return Json(new
            {
                data = data.Skip(model.start).Take(model.length).ToList(),
                recordsTotal = data.Count(),
                recordsFiltered = data.Count(),
                draw = model.draw,
            });
        }

        public JsonResult SaveScanImage(FileUpload model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }
            try
            {
                string FileName = Path.GetFileNameWithoutExtension(model.FileContent.FileName);

                string FileExtension = Path.GetExtension(model.FileContent.FileName);

                FileName = model.RecordID + FileExtension;
                string DistributionScanFolder = ConfigurationManager.AppSettings["DisposableDistributionScan"].ToString();
                var filePathForDB = DistributionScanFolder + FileName;
                var UploadedFilePath = Server.MapPath(DistributionScanFolder + FileName);

                var modelInDb = db.DisposableDistributions.Where(s => s.DisposableDistributionID == model.RecordID).FirstOrDefault();
                modelInDb.FilePath = filePathForDB;
                db.SaveChanges();
                model.FileContent.SaveAs(UploadedFilePath);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

    }

}