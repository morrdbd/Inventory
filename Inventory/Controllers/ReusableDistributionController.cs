﻿using Inventory.Attributes;
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
    public class ReusableDistributionController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();
        [AccessControl("Add")]
        public ActionResult Add()
        {
            CreateDropDown();
            ViewBag.emSearch = new Employee_Search();
            ViewBag.itemSearch = new Item_Search();
            return View("Form", new ReusableDistribution_VM());
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

            var reusableValueID = AdminRepo.ValueID("Reusable");
            var ItemGroup = AdminRepo.LookupValueList(Language, "ITEMGROUP");
            ItemGroup = ItemGroup.Where(l => l.GroupValueId == reusableValueID).ToList();
            ViewBag.GroupDrp = new SelectList(ItemGroup, "ValueId", TextField);

            var departmentList = db.Departments.Where(d => d.IsActive == true).Select(d =>
                new { d.DepartmentID, DepartmentName = Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName }).ToList();
            ViewBag.DepartmentDrp = new SelectList(departmentList, "DepartmentID", "DepartmentName");
        }

        private ReusableDistribution_VM CreateModel(ReusableDistribution model)
        {
            var objectToReturn = new ReusableDistribution_VM
            {
                ReusableDistributionID = model.ReusableDistributionID,
                TicketNumber = model.TicketNumber,
                TicketIssuedDateForm = model.TicketIssuedDate.ToDateString(Language),
                Warehouse = model.Warehouse,
                RequestNumber = model.RequestNumber,
                RequestDateForm = model.RequestDate.ToDateString(Language),
                EmpID = model.EmployeeID,
                EmpName = db.Employees.Where(e=>e.EmployeeID == model.EmployeeID).Select(e=>e.Name).FirstOrDefault(),
                EmpFatherName = db.Employees.Where(e => e.EmployeeID == model.EmployeeID).Select(e => e.FatherName).FirstOrDefault(),
                EmpOccupation = db.Employees.Where(e => e.EmployeeID == model.EmployeeID).Select(e => e.Occupation).FirstOrDefault(),
                EmpDepartmentID = db.Employees.Where(e => e.EmployeeID == model.EmployeeID).Select(e => e.DepartmentID).FirstOrDefault(),
                EmpDepartmentName = db.Departments.Where(r => r.IsActive == true && r.DepartmentID == model.EmployeeID).Select(d =>
                Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                FilePath = model.FilePath,
                Details = model.Details
            };
            var itemsList = (from d in db.ReusableDistributionItems
                             join s in db.StockInItems on d.StockInItemID equals s.ID
                             where d.ReusableDistributionID == model.ReusableDistributionID
                             select new {
                                 d.ID,
                                 d.StockInItemID,
                                 d.ReusableDistributionID,
                                 s.ItemName,
                                 s.CategoryID,
                                 s.GroupID,
                                 s.UnitID,
                                 s.SizeID,
                                 s.OriginID,
                                 s.BrandID,
                                 s.Quantity,
                                 d.DealWithAccount,
                                 s.UnitPrice,
                             }).ToList();
            objectToReturn.DistributionItems = itemsList.Select(i => new ReusableDistributionItemVM
            {
                ID = i.ID,
                StockInItemID = i.StockInItemID,
                ReusableDistributionID = i.ReusableDistributionID,
                ItemCode = db.StockInItems.Where(item => item.IsActive == true && item.ID == i.StockInItemID).Select(item => item.ItemCode).FirstOrDefault(),
                ItemName = db.StockInItems.Where(item => item.IsActive == true && item.ID == i.StockInItemID).Select(item => item.ItemName).FirstOrDefault(),
                ItemCategoryName = AdminRepo.LookupName(Language, db.StockInItems.Where(item => item.IsActive == true && item.ID == i.StockInItemID).Select(item => item.CategoryID).FirstOrDefault()),
                ItemGroupName = AdminRepo.LookupName(Language, db.StockInItems.Where(item => item.IsActive == true && item.ID == i.StockInItemID).Select(item => item.GroupID).FirstOrDefault()),
                UnitName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.ID == i.StockInItemID).Select(p => p.UnitID).FirstOrDefault()),
                ItemSizeName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.ID == i.StockInItemID).Select(p => p.SizeID).FirstOrDefault()),
                ItemOriginName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.ID == i.StockInItemID).Select(p => p.OriginID).FirstOrDefault()),
                BrandName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.ID == i.StockInItemID).Select(p => p.BrandID).FirstOrDefault()),
                Quantity = i.Quantity,
                DealWithAccount = i.DealWithAccount,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.Quantity * i.UnitPrice
            }).ToList();

            return objectToReturn;
        }

        //Load search result
        [AccessControl("Search")]
        public JsonResult ItemList(Item_Search search)
        {
            var reusableLookupId = AdminRepo.ValueID("Reusable");
            var query = db.StockInItems.Where(i => i.IsActive == true && i.AvailableQuantity != 0 && 
            i.UsageTypeID == reusableLookupId).AsQueryable();
           
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
                ID = i.ID,
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
        public JsonResult Insert(ReusableDistribution_Form_VM model)
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
                    var _distribution = new ReusableDistribution
                    {
                        TicketNumber = model.TicketNumber,
                        TicketIssuedDate = model.TicketIssuedDateForm.ToDate(Language),
                        Warehouse = model.Warehouse,
                        RequestNumber = model.RequestNumber,
                        RequestDate = model.RequestDateForm.ToDate(Language),
                        EmployeeID = model.EmpID,
                        Details = model.Details,
                        InsertedBy = AppUser.Id,
                        InsertedDate = DateTime.Now,
                        IsActive = true,
                    };

                    db.ReusableDistributions.Add(_distribution);
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "ReusableDistribution",
                        ModfiedId = _distribution.ReusableDistributionID,
                        Action = "Insert",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_distribution),
                    });
                    db.SaveChanges();

                    if (model.ReusableDistributionItems != null)
                    {
                        foreach (var row in model.ReusableDistributionItems)
                        {
                            if (row != null)
                            {
                                var _item = new ReusableDistributionItem()
                                {
                                    ReusableDistributionID = _distribution.ReusableDistributionID,
                                    StockInItemID = row.StockInItemID,
                                    DealWithAccount = row.DealWithAccount
                                };

                                db.ReusableDistributionItems.Add(_item);
                                db.SaveChanges();

                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "DistributionItem",
                                    ModfiedId = row.ID,
                                    Action = "Insert",
                                    UserId = AppUser.Id,
                                    ModifiedTime = DateTime.Now,
                                    ModifiedData = JsonConvert.SerializeObject(row),
                                });
                                db.SaveChanges();
                                //minus item from item in hand
                                var _stockInHand = db.StockInItems.Where(s => s.ID == row.StockInItemID).First();
                                if(_stockInHand.AvailableQuantity < _item.Quantity)
                                {
                                    return Json(false);
                                }
                                _stockInHand.AvailableQuantity -= _item.Quantity;

                                db.SaveChanges();
                            }
                        }
                    }

                    trans.Commit();
                    return Json(_distribution.ReusableDistributionID);
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
            ViewBag.search = new TicketSearch();
            CreateDropDown();
            return View("Search");
        }

        [AccessControl("Search")]
        public JsonResult ListPartial(TicketSearch model)
        {
            var TicketIssuedDateFrom = model.IssuedDateFrom.ToDate(Language);
            var TicketIssuedDateTo = model.IssuedDateTo.ToDate(Language);
            var RequestDateFrom = model.RequestDateFrom.ToDate(Language);
            var RequestDateTo = model.RequestDateTo.ToDate(Language);

            var query = (from d in db.ReusableDistributions
                         join e in db.Employees on d.EmployeeID equals e.EmployeeID
                         join dep in db.Departments on e.DepartmentID equals dep.DepartmentID
                         where d.IsActive == true
                         select new
                         {
                             d.ReusableDistributionID,
                             d.TicketNumber,
                             d.TicketIssuedDate,
                             d.Warehouse,
                             d.RequestNumber,
                             d.RequestDate,
                             d.EmployeeID,
                             d.FilePath,
                             EmployeeName = e.Name,
                             e.DepartmentID,
                             DepartmentName = (Language == "prs"? dep.DrName:(Language == "pa" ? dep.PaName: dep.EnName))
                         });

            if (model.TicketNumber != null)
            {
                query = query.Where(c => c.TicketNumber == model.TicketNumber);
            }
            if (TicketIssuedDateFrom != null)
            {
                query = query.Where(c => c.TicketIssuedDate >= TicketIssuedDateFrom);
            }
            if (TicketIssuedDateTo != null)
            {
                query = query.Where(c => c.TicketIssuedDate <= TicketIssuedDateTo);
            }
            if (!string.IsNullOrWhiteSpace(model.Warehouse))
            {
                query = query.Where(c => c.Warehouse.Contains(model.Warehouse));
            }
            if (model.RequestNumber != null)
            {
                query = query.Where(c => c.RequestNumber == model.RequestNumber);
            }
            if (RequestDateFrom != null)
            {
                query = query.Where(c => c.RequestDate >= RequestDateFrom);
            }
            if (RequestDateTo != null)
            {
                query = query.Where(c => c.RequestDate <= RequestDateTo);
            }
            if (model.EmployeeID != null)
            {
                query = query.Where(c => c.EmployeeID == model.EmployeeID);
            }
            if (model.EmployeeID != null)
            {
                query = query.Where(c => c.EmployeeID == model.EmployeeID);
            }
            if (model.DepartmentID != null)
            {
                query = query.Where(c => c.DepartmentID == model.DepartmentID);
            }

            ViewBag.search = model;
            var records = query.OrderBy(t => t.TicketIssuedDate).ToList();
            var data = records.Select(t => new
            {
                t.ReusableDistributionID,
                t.TicketNumber,
                TicketIssuedDate = t.TicketIssuedDate.ToDateString(Language),
                t.Warehouse,
                t.RequestNumber,
                RequestDate = t.RequestDate.ToDateString(Language),
                t.EmployeeName,
                t.DepartmentName,
                t.FilePath
            }).ToList();
            return Json(new
            {
                data = data.Skip(model.start).Take(model.length).ToList(),
                recordsTotal = data.Count(),
                recordsFiltered = data.Count(),
                draw = model.draw,
            });
        }

        [AccessControl("View")]
        public ActionResult View(int id = 0)
        {
            var ticket = db.ReusableDistributions.Where(t => t.IsActive == true).SingleOrDefault(t => t.ReusableDistributionID == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(ticket);
            return View(model);
        }

        [HttpPost]
        [AccessControl("Edit")]
        public JsonResult Update(ReusableDistribution_Form_VM model)
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
                    var _distribution = db.ReusableDistributions.Where(d=>d.IsActive == true && d.ReusableDistributionID == model.DistributionID).FirstOrDefault();
                    if (_distribution == null) return Json(false);
                    _distribution.TicketNumber = model.TicketNumber;
                    _distribution.TicketIssuedDate = model.TicketIssuedDateForm.ToDate(Language);
                    _distribution.Warehouse = model.Warehouse;
                    _distribution.RequestNumber = model.RequestNumber;
                    _distribution.RequestDate = model.RequestDateForm.ToDate(Language);
                    _distribution.EmployeeID = model.EmpID;
                    _distribution.Details = model.Details;
                    _distribution.LastUpdatedBy = AppUser.Id;
                    _distribution.LastUpdatedDate = DateTime.Now;

                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Distributions",
                        ModfiedId = _distribution.ReusableDistributionID,
                        Action = "Update",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_distribution),
                    });
                    db.SaveChanges();
                    var distributedItems = db.ReusableDistributionItems.Where(d => d.ReusableDistributionID == _distribution.ReusableDistributionID).ToList();
                    foreach(var dItem in distributedItems)
                    {
                        var stockInItem = db.StockInItems.Find(dItem.StockInItemID);
                        if(stockInItem != null)
                        {
                            stockInItem.AvailableQuantity += dItem.Quantity;
                            db.ReusableDistributionItems.Remove(dItem);
                        }
                    }
                    foreach(var dItem in model.ReusableDistributionItems)
                    {
                        var stockInItem = db.StockInItems.Find(dItem.StockInItemID);
                        if(stockInItem != null && dItem.Quantity<= stockInItem.AvailableQuantity)
                        {
                            stockInItem.AvailableQuantity -= dItem.Quantity;
                            var dItemTableObj = new ReusableDistributionItem
                            {
                                ReusableDistributionID = _distribution.ReusableDistributionID,
                                Quantity = dItem.Quantity,
                                StockInItemID = dItem.StockInItemID,
                                DealWithAccount = dItem.DealWithAccount,
                            };
                            db.ReusableDistributionItems.Add(dItemTableObj);
                        }
                    }
                    db.SaveChanges();
                    trans.Commit();
                    return Json(_distribution.ReusableDistributionID);
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
                    var _distribution = db.ReusableDistributions.Where(d => d.IsActive == true && d.ReusableDistributionID == id).FirstOrDefault();
                    if (_distribution == null) return Json(false);
                    _distribution.IsActive = false;
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Distributions",
                        ModfiedId = _distribution.ReusableDistributionID,
                        Action = "Delete",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_distribution),
                    });
                    db.SaveChanges();
                    var distributedItems = db.ReusableDistributionItems.Where(d => d.ReusableDistributionID == _distribution.ReusableDistributionID).ToList();
                    foreach (var dItem in distributedItems)
                    {
                        var stockInItem = db.StockInItems.Find(dItem.StockInItemID);
                        if (stockInItem != null)
                        {
                            stockInItem.AvailableQuantity += dItem.Quantity;
                            var dItemTobeDeleted = db.ReusableDistributionItems.Find(dItem.ID);
                            db.ReusableDistributionItems.Remove(dItemTobeDeleted);
                            db.ActivityLogs.Add(new ActivityLog
                            {
                                ModifiedTable = "DistributionItems",
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

        [AccessControl("Search")]
        public JsonResult SelectItem(int id = 0)
        {
            try
            {
                var obj = db.StockInItems.Where(i => i.ID == id && i.IsActive == true && i.AvailableQuantity > 0).FirstOrDefault();
                if (obj != null)
                {
                    var item = new {
                        obj.ID,
                        obj.AvailableQuantity,
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
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
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
                var obj = db.StockInItems.Where(i => i.ID == model.StockInItemID && i.IsActive == true).FirstOrDefault();
                if (obj != null && obj.AvailableQuantity >= model.Quantity)
                {
                    var item = new {
                        obj.ID,
                        StockInItemID = model.StockInItemID,
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
                        model.DealWithAccount
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
            ViewBag.emSearch = new Employee_Search();
            ViewBag.itemSearch = new Item_Search();
            var ticket = db.ReusableDistributions.SingleOrDefault(t => t.ReusableDistributionID == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(ticket);
            return View("Form", model);
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
                string DistributionScanFolder = ConfigurationManager.AppSettings["ReusableDistributionScan"].ToString();
                var filePathForDB = DistributionScanFolder + FileName;
                var UploadedFilePath = Server.MapPath(DistributionScanFolder + FileName);

                var modelInDb = db.ReusableDistributions.Where(s => s.ReusableDistributionID == model.RecordID).FirstOrDefault();
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

        [AccessControl("Search")]
        public ActionResult ItemsInUse()
        {
            CreateDropDown();
            ViewBag.search = new ItemInUseSearch();
            return View("ItemsInUse");
        }

        [AccessControl("Search")]
        public JsonResult ListItemInUse(ItemInUseSearch model)
        {
            var TicketIssuedDateFrom = model.DateFrom.ToDate(Language);
            var TicketIssuedDateTo = model.DateTo.ToDate(Language);
            var query = (from d in db.ReusableDistributions
                                     join dItem in db.ReusableDistributionItems on d.ReusableDistributionID equals dItem.ReusableDistributionID
                                     join sItem in db.StockInItems on dItem.StockInItemID equals sItem.ID
                                     join emp in db.Employees on d.EmployeeID equals emp.EmployeeID
                                     join dep in db.Departments on emp.DepartmentID equals dep.DepartmentID
                                     where d.IsActive == true && dItem.IsReturned == false
                                     select new
                                     {
                                        d.ReusableDistributionID,
                                        distributionItemID = dItem.ID,
                                        dItem.StockInItemID,
                                        emp.DepartmentID,
                                        d.EmployeeID,
                                        EmployeeName = emp.Name,
                                        EmployeeFName = emp.FatherName,
                                        d.TicketNumber,
                                        d.TicketIssuedDate,
                                        d.RequestNumber,
                                        d.RequestDate,
                                        dItem.Quantity,
                                        sItem.UnitID,
                                        sItem.GroupID,
                                        sItem.CategoryID,
                                        sItem.ItemName,
                                        sItem.ItemCode,
                                        sItem.SizeID,
                                        sItem.OriginID,
                                        sItem.BrandID,
                                        sItem.UnitPrice,
                                        dItem.DealWithAccount
                                     });
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
            if (model.BrandID != null)
            {
                query = query.Where(r => r.BrandID == model.BrandID);
            }
            if (TicketIssuedDateFrom != null)
            {
                query = query.Where(c => c.TicketIssuedDate >= TicketIssuedDateFrom);
            }
            if (TicketIssuedDateTo != null)
            {
                query = query.Where(c => c.TicketIssuedDate <= TicketIssuedDateTo);
            }
            ViewBag.search = model;
            var queryResult = query.OrderByDescending(i => i.TicketIssuedDate).ToList();
            var data = queryResult.Select(i => new {
                i.ReusableDistributionID,
                i.distributionItemID,
                i.StockInItemID,
                DepartmentName = db.Departments.Where(d => d.DepartmentID == i.DepartmentID).Select(d => Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                i.EmployeeName,
                i.EmployeeFName,
                i.TicketNumber,
                TicketIssuedDate = i.TicketIssuedDate.ToDateString(Language),
                i.RequestNumber,
                RequestDate = i.RequestDate.ToDateString(Language),
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
                i.DealWithAccount
            });
   
            return Json(new
            {
                data = data.Skip(model.start).Take(model.length).ToList(),
                recordsTotal = data.Count(),
                recordsFiltered = data.Count(),
                draw = model.draw,
            });
        }

        [AccessControl("View")]
        public ActionResult ViewItemInUse(int id = 0)
        {
            //var itemInUse = db.DistributionItems.Where(i=>i.IsReturned == false && i.ID == id).FirstOrDefault();
            var itemInUse = (from d in db.ReusableDistributions
                         join dItem in db.ReusableDistributionItems on d.ReusableDistributionID equals dItem.ReusableDistributionID
                         join sItem in db.StockInItems on dItem.StockInItemID equals sItem.ID
                         join emp in db.Employees on d.EmployeeID equals emp.EmployeeID
                         join dep in db.Departments on emp.DepartmentID equals dep.DepartmentID
                         where d.IsActive == true && dItem.IsReturned == false
                         select new
                         {
                             distributionItemID = dItem.ID,
                             DepartmentName = (Language == "prs" ? dep.DrName : Language == "ps" ? dep.PaName : dep.EnName),
                             EmployeeName = emp.Name,
                             EmployeeFName = emp.FatherName,
                             d.TicketNumber,
                             d.TicketIssuedDate,
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
                             dItem.DealWithAccount
                         }).FirstOrDefault();
            if (itemInUse == null)
            {
                return HttpNotFound();
            }
            var model = new ReturnItemVM {
                DistributionItemID = itemInUse.distributionItemID,
                EmpDepartmentName = itemInUse.DepartmentName,
                EmpName = itemInUse.EmployeeName,
                EmpFatherName = itemInUse.EmployeeFName,
                TicketNumber = itemInUse.TicketNumber,
                TicketIssuedDateForm = itemInUse.TicketIssuedDate.ToDateString(Language),
                Quantity = itemInUse.Quantity,
                UnitName = AdminRepo.LookupName(Language, itemInUse.UnitID),
                UsageTypeName = AdminRepo.LookupName(Language,itemInUse.UsageTypeID),
                ItemGroupName = AdminRepo.LookupName(Language, itemInUse.GroupID),
                ItemCategoryName = AdminRepo.LookupName(Language, itemInUse.CategoryID),
                ItemName = itemInUse.ItemName,
                ItemCode = itemInUse.ItemCode,
                ItemSizeName = AdminRepo.LookupName(Language, itemInUse.SizeID),
                ItemOriginName = AdminRepo.LookupName(Language, itemInUse.OriginID),
                BrandName = AdminRepo.LookupName(Language, itemInUse.BrandID),
                UnitPrice =  itemInUse.UnitPrice,
                DealWithAccount = itemInUse.DealWithAccount
            };
            return View("ReturnItem", model);
        }

        [AccessControl("Delete")]
        public JsonResult ReturnItem(int id = 0)
        {
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    var _itemInUse = db.ReusableDistributionItems.Where(d => d.IsReturned == false && d.ID == id).FirstOrDefault();
                    if (_itemInUse == null) return Json(false);
                    _itemInUse.IsReturned = true;
                    _itemInUse.ReturnDate = DateTime.Now;
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "DistributionItems",
                        ModfiedId = _itemInUse.ID,
                        Action = "Returned",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_itemInUse),
                    });
                    db.SaveChanges();
                    var stockInItem = db.StockInItems.Where(d => d.IsActive == true && d.ID == _itemInUse.StockInItemID).FirstOrDefault();
             
                    if (stockInItem != null)
                    {
                        stockInItem.AvailableQuantity += _itemInUse.Quantity;
                        db.ActivityLogs.Add(new ActivityLog
                        {
                            ModifiedTable = "StockInItems",
                            ModfiedId = stockInItem.ID,
                            Action = "Return",
                            UserId = AppUser.Id,
                            ModifiedTime = DateTime.Now,
                        });
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

    }
    
}