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
    public class ConsumeController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();
        [AccessControl("Add")]
        public ActionResult Add()
        {
            CreateDropDown();
            ViewBag.itemSearch = new Item_Search();
            ViewBag.emSearch = new Employee_Search();

            return View("Form", new ConsumeVM());
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

        private ConsumeVM CreateModel(Consume model)
        {
            return new ConsumeVM
            {
                ConsumeID = model.ConsumeID,
                DepartmentID = model.DepartmentID,
                DocumentIssueNumber = model.DocumentIssueNumber,
                DocumentIssuedDate = model.DocumentIssuedDate.ToDateString(Language),
                OrderNumber = model.OrderNumber,
                OrderDate = model.OrderDate.ToDateString(Language),
                EmployeeID = model.EmployeeID,
                EmpName = db.Employees.Where(e => e.EmployeeID == model.EmployeeID).Select(e => e.Name).FirstOrDefault(),
                EmpFatherName = db.Employees.Where(e => e.EmployeeID == model.EmployeeID).Select(e => e.FatherName).FirstOrDefault(),
                EmpOccupation = db.Employees.Where(e => e.EmployeeID == model.EmployeeID).Select(e => e.Occupation).FirstOrDefault(),
                EmpDepartmentID = db.Employees.Where(e => e.EmployeeID == model.EmployeeID).Select(e => e.DepartmentID).FirstOrDefault(),
                EmpDepartmentName = db.Departments.Where(r => r.IsActive == true && r.DepartmentID == model.EmployeeID).Select(d =>
                Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                //DepartmentName = db.Departments.Where(d => d.IsActive == true && d.DepartmentID == model.DepartmentID).Select(d=> Language == "dr" ? d.DrName : Language == "pa" ? d.PaName : d.EnName).FirstOrDefault(),
                Details = model.Details,
                FilePath = model.FilePath,
                ConsumeItems = db.ConsumesItems.Where(i =>i.ConsumeID == model.ConsumeID)
                .ToList().Select(i => new ConsumeItemVM
                {
                    ConsumeItemID = i.ConsumeItemID,
                    StockInItemID = i.StockInItemID,
                    DistributionItemID = i.DistributionItemID,
                    BarCode = db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.BarCode).FirstOrDefault(),
                    ConsumeID = i.ConsumeID,
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
        [HttpPost]
        [AccessControl("Search")]
        public JsonResult DisposableDistributedItems(DistributedItemsSearch search)
        {
            var disposableValueId = AdminRepo.ValueID("Disposable");
            var query = (from d in db.Distributions
                         join di in db.DistributionItems on d.DistributionID equals di.DistributionID
                         join si in db.StockInItems on di.StockInItemID equals si.StockInItemID
                         where d.IsActive == true && d.EmployeeID == search.EmployeeID && si.UsageTypeID == disposableValueId && di.Quantity > di.QuantityConsumed
                         select new {
                             di.DistributionItemID,
                             si.BarCode,
                             si.ItemName,
                             si.ItemCode,
                             si.GroupID,
                             si.CategoryID,
                             si.UnitID,
                             si.SizeID,
                             si.OriginID,
                             si.BrandID,
                             d.InsertedDate,
                             di.Quantity,
                             di.QuantityConsumed,
                             si.UnitPrice
                         });
            var test = query.ToList();
                //db.DistributionItems.Where(i => i.IsActive == true && i.AvailableQuantity != 0 
            //&& i.UsageTypeID == disposableValueId).AsQueryable();

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
                query = query.Where(c => c.UnitID == search.UnitID);
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
            var items = query.OrderByDescending(i=>i.InsertedDate).ToList();
              var data = items.Select(i => new DistributionItemVM
            {
                BarCode = i.BarCode,
                ItemName = i.ItemName,
                UnitName = AdminRepo.LookupName(Language, i.UnitID),
                ItemGroupName = AdminRepo.LookupName(Language, i.GroupID),
                ItemCategoryName = AdminRepo.LookupName(Language, i.CategoryID),
                ItemSizeName = AdminRepo.LookupName(Language, i.SizeID),
                ItemOriginName = AdminRepo.LookupName(Language, i.OriginID),
                BrandName = AdminRepo.LookupName(Language, i.BrandID),
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                QuantityConsumed = i.QuantityConsumed,
                ItemCode = i.ItemCode,
                DistributionItemID = i.DistributionItemID
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
        public JsonResult Insert(ConsumeVM model)
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
                    var _consume = new Consume
                    {
                        DocumentIssueNumber = model.DocumentIssueNumber,
                        DocumentIssuedDate = model.DocumentIssuedDate.ToDate(Language),
                        EmployeeID = model.EmployeeID,
                        DepartmentID = model.DepartmentID,
                        OrderNumber = model.OrderNumber,
                        OrderDate = model.OrderDate.ToDate(Language),
                        Details = model.Details,
                        InsertedBy = AppUser.Id,
                        InsertedDate = DateTime.Now,
                        IsActive = true,
                    };

                    db.Consumes.Add(_consume);
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Consumes",
                        ModfiedId = _consume.ConsumeID,
                        Action = "Insert",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_consume),
                    });
                    db.SaveChanges();

                    if (model.ConsumeItems != null)
                    {
                        foreach (var row in model.ConsumeItems)
                        {
                            if (row != null)
                            {
                                var _item = new ConsumeItem()
                                {
                                    ConsumeID = _consume.ConsumeID,
                                    StockInItemID = row.StockInItemID,
                                    Quantity = row.Quantity,
                                    DistributionItemID = row.DistributionItemID
                                };

                                db.ConsumesItems.Add(_item);
                                db.SaveChanges();

                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "ConsumeItems",
                                    ModfiedId = row.ConsumeItemID,
                                    Action = "Insert",
                                    UserId = AppUser.Id,
                                    ModifiedTime = DateTime.Now,
                                    ModifiedData = JsonConvert.SerializeObject(row),
                                });
                                db.SaveChanges();
                                //minus item from item in hand
                                var _distributionItem = db.DistributionItems.Where(s => s.StockInItemID == row.StockInItemID && s.DistributionItemID == row.DistributionItemID).FirstOrDefault();
                                if (_distributionItem.Quantity < (row.Quantity + _distributionItem.QuantityConsumed))
                                {
                                    return Json(false);
                                }
                                _distributionItem.QuantityConsumed += row.Quantity;

                                db.SaveChanges();
                            }
                        }
                    }

                    trans.Commit();
                    return Json(_consume.ConsumeID);
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
            ViewBag.search = new ConsumeItemsSearch();
            CreateDropDown();
            return View("Search");
        }

        [AccessControl("View")]
        public ActionResult View(int id = 0)
        {
            var obj = db.Consumes.Where(t => t.IsActive == true).SingleOrDefault(t => t.ConsumeID == id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(obj);
            return View(model);
        }

        [HttpPost]
        [AccessControl("Edit")]
        public JsonResult Update(ConsumeVM model)
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
                    var _consume = db.Consumes.Where(c=>c.IsActive == true && c.ConsumeID == model.ConsumeID && c.EmployeeID == model.EmployeeID).FirstOrDefault();
                    if (_consume == null) return Json(false);
                    _consume.EmployeeID = model.EmployeeID;
                    _consume.DepartmentID = model.DepartmentID;
                    _consume.DocumentIssueNumber = model.DocumentIssueNumber;
                    _consume.DocumentIssuedDate = model.DocumentIssuedDate.ToDate(Language);
                    _consume.OrderNumber = model.OrderNumber;
                    _consume.OrderDate = model.OrderDate.ToDate(Language);
                    _consume.Details = model.Details;
                    _consume.LastUpdatedBy = AppUser.Id;
                    _consume.LastUpdatedDate = DateTime.Now;

                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Consume",
                        ModfiedId = _consume.ConsumeID,
                        Action = "Update",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_consume),
                    });
                    db.SaveChanges();
                    var consumedItems = db.ConsumesItems.Where(d => d.ConsumeID == _consume.ConsumeID).ToList();
                    foreach(var cItem in consumedItems)
                    {
                        var _distributedItem = db.DistributionItems.Where(di=>di.DistributionItemID == cItem.DistributionItemID && di.StockInItemID == cItem.StockInItemID).FirstOrDefault();
                        if(_distributedItem != null)
                        {
                            _distributedItem.QuantityConsumed -= cItem.Quantity;
                            db.ConsumesItems.Remove(cItem);
                        }
                    }
                    foreach (var cItem in model.ConsumeItems)
                    {
                        var distributedItem = db.DistributionItems.Where(di=>di.StockInItemID == cItem.StockInItemID && di.DistributionItemID == cItem.DistributionItemID).FirstOrDefault();
                        if (distributedItem == null && distributedItem.Quantity < (distributedItem.QuantityConsumed + cItem.Quantity))
                        {
                            return Json(false);
                        }
                        distributedItem.QuantityConsumed += cItem.Quantity;
                        var cItemTableObj = new ConsumeItem
                        {
                            ConsumeID = _consume.ConsumeID,
                            Quantity = cItem.Quantity,
                            StockInItemID = cItem.StockInItemID,
                            DistributionItemID = distributedItem.DistributionItemID
                        };
                        db.ConsumesItems.Add(cItemTableObj);
                        
                    }
                    db.SaveChanges();
                    trans.Commit();
                    return Json(_consume.ConsumeID);
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
                    var _consume = db.Consumes.Where(d => d.IsActive == true && d.ConsumeID == id).FirstOrDefault();
                    if (_consume == null) {
                        return Json(false);
                    }
                    _consume.IsActive = false;
                    db.Consumes.Remove(_consume);
                    db.SaveChanges();
       
                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Consumes",
                        ModfiedId = _consume.ConsumeID,
                        Action = "Delete",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_consume),
                    });
                    db.SaveChanges();
                    var consumeItems = db.ConsumesItems.Where(d => d.ConsumeID == _consume.ConsumeID).ToList();
                    foreach (var cItem in consumeItems)
                    {
                        var _distributedItem = db.DistributionItems.Where(di=>di.StockInItemID == cItem.StockInItemID && di.DistributionItemID == cItem.DistributionItemID).FirstOrDefault();
                        if (_distributedItem != null)
                        {
                            _distributedItem.QuantityConsumed -= cItem.Quantity;
                            db.ConsumesItems.Remove(cItem);
                            db.ActivityLogs.Add(new ActivityLog
                            {
                                ModifiedTable = "ConsumeItems",
                                ModfiedId = cItem.ConsumeItemID,
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

        public JsonResult GetItem(ItemConsumedVM model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var _item = (from d in db.Distributions
                             join dItem in db.DistributionItems on d.DistributionID equals dItem.DistributionID
                             join s in db.StockInItems on dItem.StockInItemID equals s.StockInItemID
                             where d.IsActive == true && model.DistributionItemID == dItem.DistributionItemID
                             select new
                             {
                                 d.DistributionID,
                                 s.StockInItemID,
                                 s.BarCode,
                                 s.ItemName,
                                 s.ItemCode,
                                 s.UnitPrice,
                                 s.UsageTypeID,
                                 s.UnitID,
                                 s.GroupID,
                                 s.CategoryID,
                                 s.SizeID,
                                 s.OriginID,
                                 s.BrandID,
                                 dItem.DistributionItemID,
                                 dItem.Quantity,
                                 dItem.QuantityConsumed
                             }).FirstOrDefault() ;
                if (_item != null && _item.Quantity >= (_item.QuantityConsumed + model.Quantity))
                {
                    var itemToReturn = new {
                        _item.StockInItemID,
                        _item.DistributionID,
                        //Return the quantity consumed
                         model.Quantity,
                        _item.QuantityConsumed,
                        _item.DistributionItemID,
                        _item.BarCode,
                        _item.ItemName,
                        _item.ItemCode,
                        _item.UnitPrice,
                        _item.UsageTypeID,
                        UsageTypeName = AdminRepo.LookupName(Language, _item.UsageTypeID),
                        UnitName = AdminRepo.LookupName(Language, _item.UnitID),
                        GroupName = AdminRepo.LookupName(Language, _item.GroupID),
                        CategoryName = AdminRepo.LookupName(Language, _item.CategoryID),
                        SizeName = AdminRepo.LookupName(Language, _item.SizeID),
                        OriginName = AdminRepo.LookupName(Language, _item.OriginID),
                        BrandName = AdminRepo.LookupName(Language, _item.BrandID),
                        };
                    return Json(itemToReturn, JsonRequestBehavior.AllowGet);
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
            ViewBag.emSearch = new Employee_Search();

            var consume = db.Consumes.SingleOrDefault(t => t.ConsumeID == id);
            if (consume == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(consume);
            return View("Form", model);
        }

        [AccessControl("Search")]
        public JsonResult ConsumedItemsPartialList(ConsumedSearch model)
        {
            var _dateFrom = model.DateFrom.ToDate(Language);
            var _dateTo = model.DateTo.ToDate(Language);
            var query = (from d in db.Consumes
                                     join cItem in db.ConsumesItems on d.ConsumeID equals cItem.ConsumeID
                                     join sItem in db.StockInItems on cItem.StockInItemID equals sItem.StockInItemID
                                     join dep in db.Departments on d.EmployeeID equals dep.DepartmentID
                                     where d.IsActive == true
                                     select new
                                     {
                                        sItem.BarCode,
                                        d.ConsumeID,
                                        consumedItemID = cItem.ConsumeItemID,
                                        d.DepartmentID,
                                        cItem.StockInItemID,
                                        d.EmployeeID,
                                        DepartmentName = Language == "prs" ? dep.DrName : Language == "ps" ? dep.PaName : dep.EnName,                           
                                        d.DocumentIssueNumber,
                                        d.DocumentIssuedDate,
                                        d.OrderNumber,
                                        d.OrderDate,
                                        cItem.Quantity,
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
                                         Total = cItem.Quantity * sItem.UnitPrice
                                     });
            if (!string.IsNullOrWhiteSpace(model.BarCode))
            {
                query = query.Where(c => c.BarCode == model.BarCode);
            }
            if (model.DepartmentID != null)
            {
                query = query.Where(c => c.DepartmentID == model.DepartmentID);
            }
            if (model.EmployeeID != null)
            {
                query = query.Where(c => c.EmployeeID == model.EmployeeID);
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
                i.ConsumeID,
                i.consumedItemID,
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
                string consumedScanFolder = ConfigurationManager.AppSettings["ConsumeScan"].ToString();
                var filePathForDB = consumedScanFolder + FileName;
                var UploadedFilePath = Server.MapPath(consumedScanFolder + FileName);

                var modelInDb = db.Consumes.Where(s => s.ConsumeID == model.RecordID).FirstOrDefault();
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