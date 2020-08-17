using Inventory.Attributes;
using Inventory.Models;
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
    public class RestoreController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();
        [AccessControl("Add")]
        public ActionResult Add()
        {
            CreateDropDown();
            ViewBag.emSearch = new Employee_Search();
            ViewBag.itemSearch = new Item_Search();
            return View("Form", new RestoreVM());
        }
        [AccessControl("Edit")]
        public ActionResult Edit(int id = 0)
        {
            CreateDropDown();
            ViewBag.emSearch = new Employee_Search();
            ViewBag.itemSearch = new Item_Search();
            var restore = db.Restores.Where(r => r.RestoreID == id).FirstOrDefault();
            if(restore == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(restore);
            return View("Form", model);
        }

        [AccessControl("Search")]
        [HttpGet]
        public ActionResult Search()
        {
            ViewBag.search = new ItemsRestoredSearch();
            CreateDropDown();
            return View("Search");
        }

        private void CreateDropDown()
        {
            var ItemInCondition = AdminRepo.LookupValueList(Language, "ItemInCondition");
            ViewBag.ItemInConditionDrp = new SelectList(ItemInCondition, "ValueCode", TextField);

            var groupList = AdminRepo.LookupValueList(Language, "ITEMGROUP");
            ViewBag.GroupDrp = new SelectList(groupList, "ValueId", TextField);

            var unitList = AdminRepo.LookupValueList(Language, "UNIT");
            ViewBag.UnitDrp = new SelectList(unitList, "ValueId", TextField);

            var sizeList = AdminRepo.LookupValueList(Language, "ITEMSIZE");
            ViewBag.SizeDrp = new SelectList(sizeList, "ValueId", TextField);

            var originList = AdminRepo.LookupValueList(Language, "ITEMORIGIN");
            ViewBag.OriginDrp = new SelectList(originList, "ValueId", TextField);

            var brandList = AdminRepo.LookupValueList(Language, "ITEMBRAND");
            ViewBag.BrandDrp = new SelectList(brandList, "ValueId", TextField);


            var ItemGroup = AdminRepo.LookupValueList(Language, "ITEMGROUP");
            ViewBag.ItemGroupDrp = new SelectList(ItemGroup, "ValueId", TextField);

            var departmentList = db.Departments.Where(d => d.IsActive == true).Select(d =>
                new { d.DepartmentID, DepartmentName = Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName }).ToList();
            ViewBag.DepartmentDrp = new SelectList(departmentList, "DepartmentID", "DepartmentName");
        }

        //Load search result
        [AccessControl("Search")]
        public JsonResult EmployeeItemsList(RestoreItemSearch search)
        {
            var query = (from d in db.Distributions
                         join di in db.DistributionItems on d.DistributionID equals di.DistributionID
                         join s in db.StockInItems on di.StockInItemID equals s.StockInItemID
                         where di.IsReturned == false && d.IsActive == true && s.IsExpired == false
                         && d.EmployeeID == search.EmpID
                         select new
                         {
                             s.BarCode,
                             di.DistributionItemID,
                             di.StockInItemID,
                             d.EmployeeID,
                             s.ItemName,
                             s.UnitID,
                             s.Quantity,
                             s.GroupID,
                             s.CategoryID,
                             s.SizeID,
                             s.OriginID,
                             s.BrandID,
                             s.Remarks,
                             s.UnitPrice,
                             s.IsSecondHand,
                             s.SecondHandPrice,
                             s.ItemCode,
                             di.DealWithAccount
                         });
                //db.StockInItems.Where(i => i.IsActive == true && i.IsExpired == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.BarCode))
            {
                query = query.Where(c => c.BarCode.Contains(search.BarCode));
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
            var data = items.Select(i => new RestoreItemVM
            {
                RestoreItemID = i.DistributionItemID,
                StockInItemID = i.StockInItemID,
                ItemName = i.ItemName,
                BarCode = i.BarCode,
                Quantity = i.Quantity,
                UnitName = AdminRepo.LookupName(Language, i.UnitID),
                GroupName = AdminRepo.LookupName(Language, i.GroupID),
                CategoryName = AdminRepo.LookupName(Language, i.CategoryID),
                SizeName = AdminRepo.LookupName(Language, i.SizeID),
                OriginName = AdminRepo.LookupName(Language, i.OriginID),
                BrandName = AdminRepo.LookupName(Language, i.BrandID),
                UnitPrice = i.UnitPrice,
                TotalPrice = i.Quantity * i.UnitPrice,
                ItemCode = i.ItemCode,
                DealWithAccount = i.DealWithAccount
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

        [AccessControl("Search")]
        public JsonResult SelectItem(int id = 0)
        {
            try
            {
                var obj = (from d in db.Distributions
                           join di in db.DistributionItems on d.DistributionID equals di.DistributionID
                           join si in db.StockInItems on di.StockInItemID equals si.StockInItemID
                           where di.DistributionItemID == id && d.IsActive == true && di.IsReturned == false &&
                           si.IsActive == true && si.IsExpired == false
                           select new
                           {
                               di.DistributionItemID,
                               si.Quantity,
                               si.StockInItemID,
                               si.BarCode,
                               si.ItemName,
                               si.ItemCode,
                               si.UnitPrice,
                               di.DealWithAccount,
                               si.UnitID,
                               si.GroupID,
                               si.CategoryID,
                               si.SizeID,
                               si.OriginID,
                               si.BrandID,
                           }).FirstOrDefault();
                if (obj != null)
                {
                    var item = new
                    {
                        obj.DistributionItemID,
                        obj.Quantity,
                        obj.StockInItemID,
                        obj.BarCode,
                        obj.ItemName,
                        obj.ItemCode,
                        obj.UnitPrice,
                        obj.DealWithAccount,
                        UnitName = AdminRepo.LookupName(Language, obj.UnitID),
                        GroupName = AdminRepo.LookupName(Language, obj.GroupID),
                        CategoryName = AdminRepo.LookupName(Language, obj.CategoryID),
                        SizeName = AdminRepo.LookupName(Language, obj.SizeID),
                        OriginName = AdminRepo.LookupName(Language, obj.OriginID),
                        BrandName = AdminRepo.LookupName(Language, obj.BrandID),
                    };
                    return Json(item, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AccessControl("Add")]
        public JsonResult Insert(RestoreVM model)
        {
            var _docIssucedDate = model.DocumentIssuedDateForm.ToDate(Language);
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
                    var _restore = new Restore
                    {
                        EmployeeID = model.EmployeeID,
                        DocumentIssuedNo = model.DocumentIssuedNo,
                        DocumentIssuedDate = _docIssucedDate,
                        ItemInCondition = model.ItemInCondition,
                        Details = model.Details,
                        InsertedBy = AppUser.Id,
                        InsertedDate = DateTime.Now,
                        IsActive = true,
                    };

                    db.Restores.Add(_restore);
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Restores",
                        ModfiedId = _restore.RestoreID,
                        Action = "Insert",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_restore),
                    });
                    db.SaveChanges();

                    if (model.RestoreItems != null)
                    {
                        foreach (var row in model.RestoreItems)
                        {
                            var distributionObj = (from d in db.Distributions
                                                   join dItem in db.DistributionItems on d.DistributionID equals dItem.DistributionID
                                                   where d.EmployeeID == _restore.EmployeeID && row.StockInItemID == dItem.StockInItemID select new {distributoinItemID = dItem.DistributionItemID, dItem.StockInItemID }).FirstOrDefault();

                            if (row != null && distributionObj != null)
                            {
                                var distributionItem = db.DistributionItems.Find(distributionObj.distributoinItemID);
                                distributionItem.IsReturned = true;
                                distributionItem.ReturnDate = DateTime.Now;
                                var _item = new RestoreItem()
                                {
                                    StockInItemID = row.StockInItemID,
                                    RestoreID = _restore.RestoreID
                                };
                                db.RestoreItems.Add(_item);
                                var stockinItem = db.StockInItems.Where(i => i.StockInItemID == row.StockInItemID).FirstOrDefault();
                                if (model.ItemInCondition == "Usable")
                                {
                                    stockinItem.IsExpired = false;
                                    stockinItem.IsSecondHand = true;
                                    stockinItem.AvailableQuantity += 1;
                                }
                                else if (model.ItemInCondition == "Expired")
                                {
                                    stockinItem.IsExpired = true;
                                    stockinItem.DateExpired = DateTime.Now;
                                    stockinItem.IsSecondHand = true;
                                }
                                else
                                {
                                    return Json(false);
                                }
                                db.SaveChanges();
                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "RestoreItems",
                                    ModfiedId = row.RestoreItemID,
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
                    return Json(_restore.RestoreID);
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                }
            }
            return Json(false);
        }

        [HttpPost]
        [AccessControl("Edit")]
        public JsonResult Update(RestoreVM model)
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
                    var _Restore = db.Restores.Find(model.RestoreID);
                    if (_Restore == null) return Json(false);
                    _Restore.EmployeeID = model.EmployeeID;
                    _Restore.DocumentIssuedDate = model.DocumentIssuedDateForm.ToDate(Language);
                    _Restore.ItemInCondition = model.ItemInCondition;
                    _Restore.Details = model.Details;
                    _Restore.IsActive = true;
                    _Restore.LastUpdatedBy = AppUser.Id;
                    _Restore.LastUpdatedDate = DateTime.Now;
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Restores",
                        ModfiedId = _Restore.RestoreID,
                        Action = "Update",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_Restore),
                    });
                    db.SaveChanges();
                    db.RestoreItems.Where(x => x.RestoreID == _Restore.RestoreID).ToList().ForEach(x =>
                    {
                        var distributionItem = db.DistributionItems.Where(i => i.StockInItemID == x.StockInItemID).FirstOrDefault();
                        if (distributionItem != null)
                        {
                            distributionItem.IsReturned = false;
                            distributionItem.ReturnDate = null;
                        }
                        var stockinItem = db.StockInItems.Where(i => i.StockInItemID == x.StockInItemID).FirstOrDefault();
                        stockinItem.IsExpired = false;
                        db.RestoreItems.Remove(x);
                    }
                    );

                    db.SaveChanges();
                    if(model.RestoreItems != null)
                    {
                        model.RestoreItems.ForEach(x=>
                        {
                            var distributionItem = db.DistributionItems.Where(i => i.StockInItemID == x.StockInItemID).FirstOrDefault();
                            if (distributionItem != null)
                            {
                                distributionItem.IsReturned = true;
                                distributionItem.ReturnDate = DateTime.Now;
                            }
                            var stockinItem = db.StockInItems.Where(i => i.StockInItemID == x.StockInItemID).FirstOrDefault();
                            if (model.ItemInCondition == "Usable")
                            {
                                stockinItem.IsExpired = false;
                                stockinItem.IsSecondHand = true;
                                stockinItem.AvailableQuantity += 1;
                            }else if (model.ItemInCondition == "Expired") {
                                stockinItem.IsExpired = true;
                                stockinItem.DateExpired = DateTime.Now;
                                stockinItem.IsSecondHand = true;
                            }
                            var restoreItem = new RestoreItem() {
                                RestoreID = _Restore.RestoreID,
                                StockInItemID = x.StockInItemID
                            };

                            db.RestoreItems.Add(restoreItem);

                            string action = _Restore.RestoreID == 0 ? "Insert" : "Update";

                            db.ActivityLogs.Add(new ActivityLog
                            {
                                ModifiedTable = "RestoreItems",
                                ModfiedId = restoreItem.RestoreItemID,
                                Action = action,
                                UserId = AppUser.Id,
                                ModifiedTime = DateTime.Now,
                                ModifiedData = JsonConvert.SerializeObject(restoreItem),
                            });
                        });
                    }
                    db.SaveChanges();
                    trans.Commit();
                    return Json(_Restore.RestoreID);
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                }
            }
            return Json(false);
        }

        [AccessControl("View")]
        public ActionResult View(int id = 0)
        {
            var _record = db.Restores.Where(t => t.IsActive == true && t.RestoreID == id).FirstOrDefault();
            if (_record == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(_record);

            return View("View", model);
        }

        private RestoreVM CreateModel(Restore model)
        {
            var employee = db.Employees.Where(i => i.IsActive == true && i.EmployeeID == model.EmployeeID).FirstOrDefault();
            var restore = new RestoreVM()
            {
                RestoreID = model.RestoreID,
                DocumentIssuedDateForm = model.DocumentIssuedDate.ToDateString(Language),
                DocumentIssuedNo = model.DocumentIssuedNo,
                EmployeeID = employee.EmployeeID,
                EmpName = employee.Name,
                EmpFatherName = employee.FatherName,
                EmpOccupation = employee.Occupation,
                EmpDepartmentName = db.Departments.Where(d=>d.DepartmentID == employee.DepartmentID)
                .Select(d=> Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                ItemInCondition = AdminRepo.LookupNameByVlueCode(Language, model.ItemInCondition),
                FilePath = model.FilePath,
                Details = model.Details
            };
          
              var restoreItems = db.RestoreItems.Where(i => i.RestoreID == model.RestoreID)
                .ToList();
            foreach(var item in restoreItems)
            {
                var itemInStockIn = db.StockInItems.Where(i => i.StockInItemID == item.StockInItemID).FirstOrDefault();
                if(itemInStockIn != null)
                {
                    var restoreItem = new RestoreItemVM
                    {
                        RestoreItemID = item.RestoreItemID,
                        StockInItemID = item.StockInItemID,
                        BarCode = itemInStockIn.BarCode,
                        RestoreID = restore.RestoreID,
                        ItemName = itemInStockIn.ItemName,
                        UnitName = AdminRepo.LookupName(Language, itemInStockIn.UnitID),
                        GroupName = AdminRepo.LookupName(Language, itemInStockIn.GroupID),
                        CategoryName = AdminRepo.LookupName(Language, itemInStockIn.CategoryID),
                        SizeName = AdminRepo.LookupName(Language, itemInStockIn.SizeID),
                        OriginName = AdminRepo.LookupName(Language, itemInStockIn.OriginID),
                        BrandName = AdminRepo.LookupName(Language, itemInStockIn.BrandID),
                        UnitPrice = itemInStockIn.UnitPrice,
                        Quantity = itemInStockIn.Quantity,
                        ItemCode = itemInStockIn.ItemCode,
                        DealWithAccount = db.DistributionItems.Where(i=>i.StockInItemID == item.StockInItemID).Select(i=>i.DealWithAccount).FirstOrDefault()
                    };
                    restore.RestoreItems.Add(restoreItem);
                }
            }
                
            return restore;
        }

        [AccessControl("Search")]
        public JsonResult ItemsRestoredPartialList(ItemsRestoredSearch model)
        {
            var restoredDateFrom = model.DateFrom.ToDate(Language);
            var restoredDateTo = model.DateTo.ToDate(Language);
            var test = db.Restores.ToList();
            var query = (from r in db.Restores
                         join rItem in db.RestoreItems on r.RestoreID equals rItem.RestoreID
                         join sItem in db.StockInItems on rItem.StockInItemID equals sItem.StockInItemID
                         join emp in db.Employees on r.EmployeeID equals emp.EmployeeID
                         join dep in db.Departments on emp.DepartmentID equals dep.DepartmentID
                         join disItem in db.DistributionItems on rItem.StockInItemID equals disItem.StockInItemID
                         where r.IsActive == true && disItem.IsReturned == true
                         select new
                         {
                             sItem.BarCode,
                             r.RestoreID,
                             RestoreDate = r.DocumentIssuedDate,
                             r.DocumentIssuedNo,
                             r.ItemInCondition,
                             RestoreItemID = rItem.RestoreItemID,
                             sItem.StockInItemID,
                             emp.DepartmentID,
                             r.EmployeeID,
                             EmployeeName = emp.Name,
                             EmployeeFName = emp.FatherName,
                             emp.Occupation,
                             sItem.UnitID,
                             sItem.Quantity,
                             sItem.GroupID,
                             sItem.CategoryID,
                             sItem.ItemName,
                             sItem.ItemCode,
                             sItem.SizeID,
                             sItem.OriginID,
                             sItem.BrandID,
                             sItem.UnitPrice,
                             disItem.DealWithAccount
                         });
            var test2 = query.ToList();

            if (model.DepartmentID != null)
            {
                query = query.Where(c => c.DepartmentID == model.DepartmentID);
            }
            if (model.DepartmentID != null)
            {
                query = query.Where(c => c.DepartmentID == model.DepartmentID);
            }
            if (model.EmployeeID != null)
            {
                query = query.Where(c => c.EmployeeID == model.EmployeeID);
            }
            if (!string.IsNullOrWhiteSpace(model.BarCode))
            {
                query = query.Where(r => r.BarCode.Contains(model.BarCode));
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
            if (model.OriginID != null)
            {
                query = query.Where(r => r.OriginID == model.OriginID);
            }
            if (restoredDateFrom != null)
            {
                query = query.Where(c => c.RestoreDate >= restoredDateFrom);
            }
            if (restoredDateTo != null)
            {
                query = query.Where(c => c.RestoreDate <= restoredDateTo);
            }
            if (model.DocumentIssuedNo != null)
            {
                query = query.Where(c => c.DocumentIssuedNo == model.DocumentIssuedNo);
            }
            if (!string.IsNullOrWhiteSpace(model.ItemInCondition))
            {
                query = query.Where(c => c.ItemInCondition == model.ItemInCondition);
            }
            ViewBag.search = model;
            var queryResult = query.OrderByDescending(i => i.RestoreDate).ToList();
            var data = queryResult.Select(i => new
            {
                i.BarCode,
                i.RestoreID,
                RestoreDate = i.RestoreDate.ToDateString(Language),
                i.DocumentIssuedNo,
                i.ItemInCondition,
                DepartmentName = db.Departments.Where(d => d.DepartmentID == i.DepartmentID).Select(d => Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                i.EmployeeName,
                i.EmployeeFName,
                i.Occupation,
                i.RestoreItemID,
                i.StockInItemID,
                UnitName = AdminRepo.LookupName(Language, i.UnitID),
                i.Quantity,
                GroupName = AdminRepo.LookupName(Language, i.GroupID),
                CategoryName = AdminRepo.LookupName(Language, i.CategoryID),
                i.ItemName,
                i.ItemCode,
                SizeName = AdminRepo.LookupName(Language, i.SizeID),
                OriginName = AdminRepo.LookupName(Language, i.OriginID),
                BrandName = AdminRepo.LookupName(Language, i.BrandID),
                i.UnitPrice,
                Total = i.UnitPrice * i.Quantity,
                i.DealWithAccount
            });
            data = data.OrderBy(i=>i.RestoreDate).ToList();
            return Json(new
            {
                data = data.Skip(model.start).Take(model.length).ToList(),
                recordsTotal = data.Count(),
                recordsFiltered = data.Count(),
                draw = model.draw,
            });
        }

        [AccessControl("Delete")]
        public JsonResult Delete(int id = 0)
        {
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    var _restore = db.Restores.Find(id);
                    if (_restore != null)
                    {
                        _restore.IsActive = false;
                        db.ActivityLogs.Add(new ActivityLog
                        {
                            ModifiedTable = "Restores",
                            ModfiedId = id,
                            Action = "Delete",
                            UserId = AppUser.Id,
                            ModifiedTime = DateTime.Now,
                        });
                        db.SaveChanges();
                        var _restoreItems = db.RestoreItems.Where(s => s.RestoreID == _restore.RestoreID ).ToList();
                        _restoreItems.ForEach(item =>
                        {
                            var distributionItem = db.DistributionItems.Where(i=>i.StockInItemID == item.StockInItemID).FirstOrDefault();
                            if(distributionItem != null)
                            {
                                distributionItem.IsReturned = false;
                                distributionItem.ReturnDate = null;
                            }
                            var stockInItem = db.StockInItems.Where(i=>i.StockInItemID == item.StockInItemID).FirstOrDefault();
                            if(stockInItem != null)
                            {
                                stockInItem.IsExpired = false;
                                stockInItem.DateExpired = null;
                            }
                            db.SaveChanges();
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
                string RestoreScanFolder = ConfigurationManager.AppSettings["RestoreScan"].ToString();
                var filePathForDB = RestoreScanFolder + FileName;
                var UploadedFilePath = Server.MapPath(RestoreScanFolder + FileName);

                var modelInDb = db.Restores.Where(s => s.RestoreID == model.RecordID).FirstOrDefault();
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