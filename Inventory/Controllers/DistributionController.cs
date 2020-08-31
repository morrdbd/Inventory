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
    public class DistributionController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();
        [AccessControl("Add")]
        public ActionResult Add()
        {
            CreateDropDown();
            ViewBag.emSearch = new Employee_Search();
            ViewBag.itemSearch = new Item_Search();
            return View("Form", new Distribution_VM());
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

        private Distribution_VM CreateModel(Distribution model)
        {
            var objectToReturn = new Distribution_VM
            {
                DistributionID = model.DistributionID,
                TicketNumber = model.TicketNumber,
                TicketIssuedDateForm = model.TicketIssuedDate.ToDateString(Language),
                Warehouse = model.Warehouse,
                RequestNumber = model.RequestNumber,
                RequestDateForm = model.RequestDate.ToDateString(Language),
                EmployeeID = model.EmployeeID,
                EmpName = db.Employees.Where(e=>e.EmployeeID == model.EmployeeID).Select(e=>e.Name).FirstOrDefault(),
                EmpFatherName = db.Employees.Where(e => e.EmployeeID == model.EmployeeID).Select(e => e.FatherName).FirstOrDefault(),
                EmpOccupation = db.Employees.Where(e => e.EmployeeID == model.EmployeeID).Select(e => e.Occupation).FirstOrDefault(),
                EmpDepartmentID = db.Employees.Where(e => e.EmployeeID == model.EmployeeID).Select(e => e.DepartmentID).FirstOrDefault(),
                EmpDepartmentName = db.Departments.Where(r => r.IsActive == true && r.DepartmentID == model.EmployeeID).Select(d =>
                Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                FilePath = model.FilePath,
                Details = model.Details
            };
            var itemsList = (from d in db.DistributionItems
                             join s in db.StockInItems on d.StockInItemID equals s.StockInItemID
                             where d.DistributionID == model.DistributionID
                             select new {
                                 d.DistributionItemID,
                                 s.BarCode,
                                 d.StockInItemID,
                                 d.DistributionID,
                                 s.ItemName,
                                 s.UsageTypeID,
                                 s.CategoryID,
                                 s.GroupID,
                                 s.UnitID,
                                 s.SizeID,
                                 s.OriginID,
                                 s.BrandID,
                                 d.Quantity,
                                 d.DealWithAccount,
                                 s.UnitPrice,
                                 s.IsSecondHand,
                                 s.SecondHandPrice
                             }).ToList();
            objectToReturn.DistributionItems = itemsList.Select(i => new DistributionItemVM
            {
                DistributionItemID = i.DistributionItemID,
                BarCode = i.BarCode,
                StockInItemID = i.StockInItemID,
                DistributionID = i.DistributionID,
                ItemCode = db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.ItemCode).FirstOrDefault(),
                ItemName = db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.ItemName).FirstOrDefault(),
                ItemCategoryName = AdminRepo.LookupName(Language, db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.CategoryID).FirstOrDefault()),
                UsageTypeName = AdminRepo.LookupName(Language, db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.UsageTypeID).FirstOrDefault()),
                ItemGroupName = AdminRepo.LookupName(Language, db.StockInItems.Where(item => item.IsActive == true && item.StockInItemID == i.StockInItemID).Select(item => item.GroupID).FirstOrDefault()),
                UnitName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.StockInItemID == i.StockInItemID).Select(p => p.UnitID).FirstOrDefault()),
                ItemSizeName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.StockInItemID == i.StockInItemID).Select(p => p.SizeID).FirstOrDefault()),
                ItemOriginName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.StockInItemID == i.StockInItemID).Select(p => p.OriginID).FirstOrDefault()),
                BrandName = AdminRepo.LookupName(Language, db.StockInItems.Where(p => p.IsActive == true && p.StockInItemID == i.StockInItemID).Select(p => p.BrandID).FirstOrDefault()),
                Quantity = i.Quantity,
                DealWithAccount = i.DealWithAccount,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.Quantity * i.UnitPrice,
                IsSecondHand = i.IsSecondHand,
                SecondHandPrice = i.SecondHandPrice
            }).ToList();

            return objectToReturn;
        }

        //Load search result
        [AccessControl("Search")]
        public JsonResult ItemList(Item_Search search)
        {
            var query = db.StockInItems.Where(i => i.IsActive == true && i.AvailableQuantity != 0).AsQueryable();
           
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
            if (!string.IsNullOrWhiteSpace(search.BarCode))
            {
                query = query.Where(c => c.BarCode == search.BarCode);
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
            var items = query.ToList();
              var data = items.Select(i => new StockInItemVM
            {
                StockInItemID = i.StockInItemID,
                BarCode = i.BarCode,
                ItemName = i.ItemName,
                UnitName = AdminRepo.LookupName(Language, i.UnitID),
                //UnitID = i.UnitID,
                //GroupID = i.GroupID,
                GroupName = AdminRepo.LookupName(Language, i.GroupID),
                //CategoryID = i.CategoryID,
                CategoryName = AdminRepo.LookupName(Language, i.CategoryID),
                //SizeID = i.SizeID,
                SizeName = AdminRepo.LookupName(Language, i.SizeID),
                //OriginID = i.OriginID,
                OriginName = AdminRepo.LookupName(Language, i.OriginID),
                //BrandID = i.BrandID,
                BrandName = AdminRepo.LookupName(Language, i.BrandID),
                Remarks = i.Remarks,
                UnitPrice = i.UnitPrice,
                AvailableQuantity = i.AvailableQuantity,
                UsageTypeID = i.UsageTypeID,
                UsageTypeName = AdminRepo.LookupName(Language, i.UsageTypeID),
                ItemCode = i.ItemCode,
                IsSecondHand = i.IsSecondHand,
                SecondHandPrice = i.SecondHandPrice
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
        public JsonResult Insert(Distribution_Form_VM model)
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
                    var _distribution = new Distribution
                    {
                        TicketNumber = model.TicketNumber,
                        TicketIssuedDate = model.TicketIssuedDateForm.ToDate(Language),
                        Warehouse = model.Warehouse,
                        RequestNumber = model.RequestNumber,
                        RequestDate = model.RequestDateForm.ToDate(Language),
                        EmployeeID = model.EmployeeID,
                        Details = model.Details,
                        InsertedBy = AppUser.Id,
                        InsertedDate = DateTime.Now,
                        IsActive = true,
                    };

                    db.Distributions.Add(_distribution);
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Distributions",
                        ModfiedId = _distribution.DistributionID,
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
                                var _item = new DistributionItem()
                                {
                                    DistributionID = _distribution.DistributionID,
                                    StockInItemID = row.StockInItemID,
                                    DealWithAccount = row.DealWithAccount,
                                    Quantity = row.Quantity
                                };

                                db.DistributionItems.Add(_item);
                                db.SaveChanges();

                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "DistributionItems",
                                    ModfiedId = row.DistributionItemID,
                                    Action = "Insert",
                                    UserId = AppUser.Id,
                                    ModifiedTime = DateTime.Now,
                                    ModifiedData = JsonConvert.SerializeObject(row),
                                });
                                db.SaveChanges();
                                //minus item from item in hand
                                var _stockInHand = db.StockInItems.Where(s => s.StockInItemID == row.StockInItemID).First();
                                if(_stockInHand.AvailableQuantity <= 0 || _stockInHand.Quantity < row.Quantity)
                                {
                                    return Json(false);
                                }
                                if(_stockInHand.IsSecondHand == true)
                                {
                                    _stockInHand.SecondHandPrice = row.UnitPrice;
                                }
                                _stockInHand.AvailableQuantity -= row.Quantity;

                                db.SaveChanges();
                            }
                        }
                    }

                    trans.Commit();
                    return Json(_distribution.DistributionID);
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

            var query = (from d in db.Distributions
                         join e in db.Employees on d.EmployeeID equals e.EmployeeID
                         join dep in db.Departments on e.DepartmentID equals dep.DepartmentID
                         where d.IsActive == true
                         select new
                         {
                             d.DistributionID,
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

            var test = query.ToList();
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
            var records = query.OrderByDescending(t => t.TicketIssuedDate).ToList();
            var data = records.Select(t => new
            {
                t.DistributionID,
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
            var ticket = db.Distributions.Where(t => t.IsActive == true).SingleOrDefault(t => t.DistributionID == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(ticket);
            return View(model);
        }

        [HttpPost]
        [AccessControl("Edit")]
        public JsonResult Update(Distribution_Form_VM model)
        {
            if (ModelState.IsValid == false && model.DistributionItems.Length == 0)
            {
                LogModelErrors();
                return Json(false);
            }
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    var _distribution = db.Distributions.Where(d=>d.IsActive == true && d.DistributionID == model.DistributionID).FirstOrDefault();
                    if (_distribution == null) return Json(false);
                    _distribution.TicketNumber = model.TicketNumber;
                    _distribution.TicketIssuedDate = model.TicketIssuedDateForm.ToDate(Language);
                    _distribution.Warehouse = model.Warehouse;
                    _distribution.RequestNumber = model.RequestNumber;
                    _distribution.RequestDate = model.RequestDateForm.ToDate(Language);
                    _distribution.EmployeeID = model.EmployeeID;
                    _distribution.Details = model.Details;
                    _distribution.LastUpdatedBy = AppUser.Id;
                    _distribution.LastUpdatedDate = DateTime.Now;

                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Distributions",
                        ModfiedId = _distribution.DistributionID,
                        Action = "Update",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_distribution),
                    });
                    db.SaveChanges();
                    var distributedItems = db.DistributionItems.Where(d => d.DistributionID == _distribution.DistributionID).ToList();
                    foreach(var dItem in distributedItems)
                    {
                        var _stockInItem = db.StockInItems.Find(dItem.StockInItemID);
                        if(_stockInItem != null)
                        {
                            _stockInItem.AvailableQuantity += dItem.Quantity;
                            _stockInItem.SecondHandPrice = null;
                            db.DistributionItems.Remove(dItem);
                        }
                    }
                    db.SaveChanges();
                    foreach(var dItem in model.DistributionItems)
                    {
                        var _stockInItem = db.StockInItems.Find(dItem.StockInItemID);
                        if(_stockInItem != null && dItem.Quantity<= _stockInItem.AvailableQuantity)
                        {
                            _stockInItem.AvailableQuantity -= dItem.Quantity;
                            var dItemTableObj = new DistributionItem
                            {
                                DistributionID = _distribution.DistributionID,
                                StockInItemID = dItem.StockInItemID,
                                Quantity = dItem.Quantity,
                                DealWithAccount = dItem.DealWithAccount,
                            };
                            db.DistributionItems.Add(dItemTableObj);
                            if(_stockInItem.IsSecondHand == true)
                            {
                                _stockInItem.SecondHandPrice = dItem.UnitPrice;
                            }
                        }
                        else
                        {
                            return Json(false);
                        }
                    }
                    db.SaveChanges();
                    trans.Commit();
                    return Json(_distribution.DistributionID);
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
                    var _distribution = db.Distributions.Where(d => d.IsActive == true && d.DistributionID == id).FirstOrDefault();
                    if (_distribution == null) return Json(false);
                    _distribution.IsActive = false;
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Distributions",
                        ModfiedId = _distribution.DistributionID,
                        Action = "Delete",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_distribution),
                    });
                    db.SaveChanges();
                    var distributedItems = db.DistributionItems.Where(d => d.DistributionID == _distribution.DistributionID).ToList();
                    foreach (var dItem in distributedItems)
                    {
                        var _stockInItem = db.StockInItems.Find(dItem.StockInItemID);
                        if (_stockInItem != null)
                        {
                            _stockInItem.AvailableQuantity += dItem.Quantity;
                            _stockInItem.SecondHandPrice = null;

                            var dItemTobeDeleted = db.DistributionItems.Find(dItem.DistributionItemID);
                            db.DistributionItems.Remove(dItemTobeDeleted);
                            db.ActivityLogs.Add(new ActivityLog
                            {
                                ModifiedTable = "DistributionItems",
                                ModfiedId = dItem.DistributionItemID,
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
                var obj = db.StockInItems.Where(i => i.StockInItemID == id && i.IsActive == true && i.AvailableQuantity > 0).FirstOrDefault();
                if (obj != null)
                {
                    var item = new {
                        obj.StockInItemID,
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
                        model.DealWithAccount,
                        obj.IsSecondHand,
                        obj.SecondHandPrice
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
            var ticket = db.Distributions.SingleOrDefault(t => t.DistributionID == id);
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
                string DistributionScanFolder = ConfigurationManager.AppSettings["DistributionScan"].ToString();
                var filePathForDB = DistributionScanFolder + FileName;
                var UploadedFilePath = Server.MapPath(DistributionScanFolder + FileName);

                var modelInDb = db.Distributions.Where(s => s.DistributionID == model.RecordID).FirstOrDefault();
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
        public ActionResult DistributedItems()
        {
            CreateDropDown();
            ViewBag.search = new DistributedItemsSearch();
            return View("DistributedItems");
        }

        [AccessControl("Search")]
        public JsonResult DistributedItemsPartialList(DistributedItemsSearch model)
        {
            var TicketIssuedDateFrom = model.DateFrom.ToDate(Language);
            var TicketIssuedDateTo = model.DateTo.ToDate(Language);
            var query = (from d in db.Distributions
                                     join dItem in db.DistributionItems on d.DistributionID equals dItem.DistributionID
                                     join sItem in db.StockInItems on dItem.StockInItemID equals sItem.StockInItemID
                                     join emp in db.Employees on d.EmployeeID equals emp.EmployeeID
                                     join dep in db.Departments on emp.DepartmentID equals dep.DepartmentID
                                     where d.IsActive == true && dItem.IsReturned == false
                                     select new
                                     {
                                        sItem.BarCode,
                                        sItem.UsageTypeID,
                                        d.DistributionID,
                                        distributionItemID = dItem.DistributionItemID,
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
                                        dItem.QuantityConsumed,
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
            if (model.BarCode != null)
            {
                query = query.Where(r => r.BarCode.Contains(model.BarCode));
            }
            if (model.UsageTypeID != null)
            {
                query = query.Where(r => r.UsageTypeID == model.UsageTypeID);
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
                i.BarCode,
                i.DistributionID,
                i.distributionItemID,
                i.StockInItemID,
                DepartmentName = db.Departments.Where(d => d.DepartmentID == i.DepartmentID).Select(d => Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                i.EmployeeName,
                i.EmployeeFName,
                i.TicketNumber,
                TicketIssuedDate = i.TicketIssuedDate.ToDateString(Language),
                i.RequestNumber,
                RequestDate = i.RequestDate.ToDateString(Language),
                UnitName = AdminRepo.LookupName(Language, i.UnitID),
                GroupName = AdminRepo.LookupName(Language, i.GroupID),
                UsageType = AdminRepo.LookupName(Language, i.UsageTypeID),
                CategoryName = AdminRepo.LookupName(Language, i.CategoryID),
                i.ItemName,
                i.ItemCode,
                SizeName = AdminRepo.LookupName(Language, i.SizeID),
                OriginName = AdminRepo.LookupName(Language, i.OriginID),
                BrandName = AdminRepo.LookupName(Language, i.BrandID),
                i.UnitPrice,
                i.DealWithAccount,
                i.Quantity,
                i.QuantityConsumed
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
            var itemInUse = (from d in db.Distributions
                         join dItem in db.DistributionItems on d.DistributionID equals dItem.DistributionID
                         join sItem in db.StockInItems on dItem.StockInItemID equals sItem.StockInItemID
                         join emp in db.Employees on d.EmployeeID equals emp.EmployeeID
                         join dep in db.Departments on emp.DepartmentID equals dep.DepartmentID
                         where d.IsActive == true && dItem.IsReturned == false
                         select new
                         {
                             distributionItemID = dItem.DistributionItemID,
                             DepartmentName = (Language == "prs" ? dep.DrName : Language == "ps" ? dep.PaName : dep.EnName),
                             EmployeeName = emp.Name,
                             EmployeeFName = emp.FatherName,
                             d.TicketNumber,
                             d.TicketIssuedDate,
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
            var model = new DistributedItemVM {
                DistributionItemID = itemInUse.distributionItemID,
                EmpDepartmentName = itemInUse.DepartmentName,
                EmpName = itemInUse.EmployeeName,
                EmpFatherName = itemInUse.EmployeeFName,
                TicketNumber = itemInUse.TicketNumber,
                TicketIssuedDateForm = itemInUse.TicketIssuedDate.ToDateString(Language),
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

    }
    
}