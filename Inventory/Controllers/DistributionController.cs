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
using System.Data.Entity;
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

            var sizeList = AdminRepo.LookupValueList(Language, "PRODUCTSIZE");
            ViewBag.SizeDrp = new SelectList(sizeList, "ValueId", TextField);

            var originList = AdminRepo.LookupValueList(Language, "PRODUCTORIGIN");
            ViewBag.OriginDrp = new SelectList(originList, "ValueId", TextField);

            var brandList = AdminRepo.LookupValueList(Language, "PRODUCTBRAND");
            ViewBag.BrandDrp = new SelectList(brandList, "ValueId", TextField);


            var ProductGroup = AdminRepo.LookupValueList(Language, "PRODUCTGROUP");
            ViewBag.ProductGroupDrp = new SelectList(ProductGroup, "ValueId", TextField);

            var departmentList = db.Departments.Where(d => d.IsActive == true).Select(d =>
                new { d.DepartmentID, DepartmentName = Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName }).ToList();
            ViewBag.DepartmentDrp = new SelectList(departmentList, "DepartmentID", "DepartmentName");
        }

        private Distribution_VM CreateModel(Distribution model)
        {
            return new Distribution_VM
            {
                DistributionID = model.DistributionID,
                TicketNumber = model.TicketNumber,
                TicketIssuedDateForm = model.TicketIssuedDate.ToDateString(Language),
                Warehouse = model.Warehouse,
                RequestNumber = model.RequestNumber,
                RequestDateForm = model.RequestDate.ToDateString(Language),
                EmpID = model.EmployeeID,
                EmpDepartmentName = db.Departments.Where(r => r.IsActive == true && r.DepartmentID == model.EmployeeID).Select(d =>
                Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                Details = model.Details,
                DistributionItems = db.DistributionItems.Where(i => i.IsActive == true && i.DistributionID == model.DistributionID)
                .ToList().Select(i => new DistributionItemVM
                {
                    ID = i.ID,
                    DistributionID = i.DistributionID,
                    UsageTypeID = db.Products.Where(p => p.IsActive == true && p.ProductCode == i.ProductCode).Select(p => p.UsageTypeID).FirstOrDefault(),
                    ProductCode = i.ProductCode,
                    ProductName = db.Products.Where(p => p.IsActive == true && p.ProductCode == i.ProductCode).Select(p => p.ProductName).FirstOrDefault(),
                    UnitName = AdminRepo.LookupName(Language, db.Products.Where(p => p.IsActive == true && p.ProductCode == i.ProductCode).Select(p => p.UnitID).FirstOrDefault()),
                    Quantity = i.Quantity,
                    DealWithAccount = i.DealWithAccount,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.Quantity * i.UnitPrice
                }
                ).ToList(),
            };
        }

        //Load search result
        [AccessControl("Search")]
        public JsonResult EmployeeList(Employee_Search model)
        {
            var query = db.Employees.Select(c => new
            {
                c.EmployeeID,
                c.Name,
                c.FatherName,
                c.Occupation,
                c.DepartmentID,
                c.IsActive
            }).Where(c => c.IsActive == true);
            if (!string.IsNullOrWhiteSpace(model.sName))
            {
                query = query.Where(c => c.Name.Contains(model.sName));
            }
            if (!string.IsNullOrWhiteSpace(model.sFatherName))
            {
                query = query.Where(c => c.FatherName.Contains(model.sFatherName));
            }
            if (model.sDepartmentID != null)
            {
                query = query.Where(c => c.DepartmentID == model.sDepartmentID);
            }
            ViewBag.search = model;
            var tes1 = query.ToList();

            var data = query.Select(c =>
            new
            {
                c.EmployeeID,
                c.Name,
                c.FatherName,
                c.Occupation,
                DepartmentName = db.Departments.Where(d => d.DepartmentID == d.DepartmentID).Select(d => Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault()
            }
            ).ToList();
            var tes = data.ToList();
            return Json(data);
        }
        //Load search result
        [AccessControl("Search")]
        public JsonResult ItemList(Item_Search search)
        {
            var query = db.StockInItems.Where(i => i.IsActive == true).AsQueryable();
           
            if (!string.IsNullOrWhiteSpace(search.ProductName))
            {
                query = query.Where(c => c.ProductName.Contains(search.ProductName));
            }
            if (!string.IsNullOrWhiteSpace(search.ProductCode))
            {
                query = query.Where(c => c.ProductCode.Contains(search.ProductCode));
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
                AvailableQuantity = i.AvailableQuantity,
                UsageTypeID = i.UsageTypeID,
                ProductCode = i.ProductCode
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
                        RequestDate = model.RequestDateVM.ToDate(Language),
                        EmployeeID = model.EmpID,
                        Details = model.Details,
                        InsertedBy = AppUser.Id,
                        InsertedDate = DateTime.Now,
                        IsActive = true,
                    };

                    db.Distributions.Add(_distribution);
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Distribution",
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
                                    DistributionID = row.DistributionID,
                                    ProductCode = row.ProductCode,
                                    Quantity = row.Quantity,
                                    UnitPrice = row.UnitPrice,
                                    DealWithAccount = row.DealWithAccount,
                                    InsertedBy = AppUser.Id,
                                    InsertedDate = DateTime.Now,
                                    IsActive = true
                                };

                                db.DistributionItems.Add(_item);
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
                                var _stockInHand = db.InHandStocks.Where(s => s.ProductCode == _item.ProductCode).First();

                                _stockInHand.Quantity -= _item.Quantity;

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
            var TicketIssuedDateFrom = model.DateFrom.ToDate(Language);
            var TicketIssuedDateTo = model.DateTo.ToDate(Language);

            var query = db.Distributions.Where(t => t.IsActive == true);

            if (model.RequestNumber != null && model.RequestNumber != 0)
            {
                query = query.Where(c => c.RequestNumber == model.RequestNumber);
            }
            if (model.TicketNumber != null && model.TicketNumber != 0)
            {
                query = query.Where(c => c.TicketNumber == model.TicketNumber);
            }
            if (model.EmployeeID != 0)
            {
                query = query.Where(c => c.EmployeeID == model.EmployeeID);
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
            var records = query.OrderBy(t => t.TicketIssuedDate).ToList();
            var data = records.Select(t => new
            {
                t.DistributionID,
                t.TicketNumber,
                TicketIssuedDate = t.TicketIssuedDate.ToDateString(Language),
                t.Warehouse,
                t.RequestNumber,
                RequestDate = t.RequestDate.ToDateString(Language),
                EmployeeName = db.Employees.Where(r => r.IsActive == true && r.EmployeeID == t.EmployeeID).Select(r => r.Name).FirstOrDefault()
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
            if (ModelState.IsValid == false)
            {
                LogModelErrors();
                return Json(false);
            }
            //            using (var trans = db.Database.BeginTransaction())
            //            {
            //                try
            //                {
            //                    var _distribution = db.Distributions.Find(model.TicketID);
            //                    if (_distribution == null) return Json(false);
            //                    _distribution.TicketNumber = model.TicketNumber;
            //                    _distribution.TicketIssuedDate = model.TicketIssuedDateVM.ToDate(Language);
            //                    _distribution.Warehouse = model.Warehouse;
            //                    _distribution.RequestNumber = model.RequestNumber;
            //                    _distribution.RequestDate = model.RequestDateVM.ToDate(Language);
            //                    _distribution.EmployeeID = model.EmployeeID;
            //                    _distribution.Details = model.Details;
            //                    _distribution.IsActive = true;
            //                    _distribution.LastUpdatedBy = AppUser.Id;
            //                    _distribution.LastUpdatedDate = DateTime.Now;

            //                    db.SaveChanges();

            //                    db.ActivityLogs.Add(new ActivityLog
            //                    {
            //                        ModifiedTable = "Ticket",
            //                        ModfiedId = _distribution.DistributionID,
            //                        Action = "Update",
            //                        UserId = AppUser.Id,
            //                        ModifiedTime = DateTime.Now,
            //                        ModifiedData = JsonConvert.SerializeObject(_distribution),
            //                    });
            //                    db.SaveChanges();

            //                    db.DistributionItems.Where(x => x.DistributionID == _distribution.DistributionID).ToList().ForEach(x => x.IsActive = false);
            //                    db.SaveChanges();
            //                    if (model.TicketItems != null)
            //                    {
            //                        foreach (var row in model.TicketItems)
            //                        {
            //                            if (row != null)
            //                            {
            //                                string action = row.ID == 0 ? "Insert" : "Update";
            //                                if (row.ID == 0)
            //                                {
            //                                    row.DistributionID = _distribution.DistributionID;
            //                                    row.InsertedBy = AppUser.Id;
            //                                    row.InsertedDate = DateTime.Now;
            //                                    row.IsActive = true;
            //                                    db.DistributionItems.Add(row);
            //                                }
            //                                else
            //                                {
            //                                    var obj = db.DistributionItems.Find(row.ID);
            //                                    if (obj != null)
            //                                    {
            //                                        obj.Quantity = row.Quantity;
            //                                        obj.ProductCode = row.ProductCode;
            //                                        obj.UnitPrice = row.UnitPrice;
            //                                        obj.DealWithAccount = row.DealWithAccount;
            //                                        obj.LastUpdatedBy = AppUser.Id;
            //                                        obj.LastUpdatedDate = DateTime.Now;
            //                                        obj.IsActive = true;
            //                                    }
            //                                }
            //                                db.SaveChanges();

            //                                db.ActivityLogs.Add(new ActivityLog
            //                                {
            //                                    ModifiedTable = "TicketItem",
            //                                    ModfiedId = row.ID,
            //                                    Action = action,
            //                                    UserId = AppUser.Id,
            //                                    ModifiedTime = DateTime.Now,
            //                                    ModifiedData = JsonConvert.SerializeObject(row),
            //                                });
            //                                db.SaveChanges();
            //                            }
            //                        }
            //                    }
            //            trans.Commit();
            //            return Json(_distribution.DistributionID);
            //        }
            //                catch (Exception e)
            //                {
            //                    trans.Rollback();
            //                    Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            //    }
            //}
            return Json(false);
        }
        [AccessControl("Delete")]
        public JsonResult Delete(int id = 0)
        {
            try
            {
                var obj = db.Distributions.Find(id);
                if (obj != null)
                {
                    obj.IsActive = false;
                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Ticket",
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
                        obj.ProductName,
                        obj.ProductCode,
                        obj.UnitPrice,
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
                var obj = db.StockInItems.Where(i => i.ID == model.ItemID && i.IsActive == true && i.AvailableQuantity > 0).FirstOrDefault();
                if (obj != null && obj.AvailableQuantity >= model.Quantity)
                {
                    var item = new {
                        obj.ID,
                       // obj.
                        Quantity = model.Quantity,
                        DealWithAccount = model.DealWithAccount,
                        obj.ProductName,
                        obj.ProductCode,
                        obj.UnitPrice,
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
            var ticket = db.Distributions.SingleOrDefault(t => t.DistributionID == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(ticket);
            return View("Form", model);
        }

        [AccessControl("Search")]
        public ActionResult ItemInUse()
        {
            CreateDropDown();
            ViewBag.search = new ItemInUseSearch();
            return View("ItemInUse");
        }

        //[AccessControl("Search")]
        //public JsonResult ListItemInUse(ItemInUseSearch model)
        //{
        //    var TicketIssuedDateFrom = model.DateFrom.ToDate(Language);
        //    var TicketIssuedDateTo = model.DateTo.ToDate(Language);

        //    var query = db.Distributions.Where(t => t.IsActive == true);

        //    if (model.TicketNumber != null && model.TicketNumber != 0)
        //    {
        //        query = query.Where(c => c.TicketNumber == model.TicketNumber);
        //    }
        //    if (model.BranchID != 0)
        //    {
        //        query = query.Where(c => c.BranchID == model.BranchID);
        //    }
        //    if (TicketIssuedDateFrom != null)
        //    {
        //        query = query.Where(c => c.TicketIssuedDate >= TicketIssuedDateFrom);
        //    }
        //    if (TicketIssuedDateTo != null)
        //    {
        //        query = query.Where(c => c.TicketIssuedDate <= TicketIssuedDateTo);
        //    }
        //    ViewBag.search = model;
        //    var records = (from t in query
        //                   join i in db.Distributions
        //                   on t.DistributionID equals i.DistributionID
        //                   select new {
        //                       t.DistributionID,
        //                       t.TicketNumber,
        //                       t.TicketIssuedDate,
        //                       t.BranchID,
        //                       t.RequestDate,
        //                       BranchName = db.Branches.Where(r => r.IsActive == true && r.BranchID == t.BranchID).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault(),
        //                       i.Details,
        //                       UnitName = db.LookupValues.Where(l=>l.IsActive == true && l.ValueId == i.UnitID).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault(),
        //                       i.UnitPrice
        //                   }).OrderBy(t => t.TicketIssuedDate).ToList();
        //    //query.OrderBy(t => t.TicketIssuedDate).ToList();
        //    var data = records.Select(t => new
        //    {
        //        t.TicketID,
        //        t.TicketNumber,
        //        TicketIssuedDate = t.TicketIssuedDate.ToDateString(Language),
        //        RequestDate = t.RequestDate.ToDateString(Language),
        //        BranchName = db.Branches.Where(r => r.IsActive == true && r.BranchID == t.BranchID).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault()
        //    }).ToList();
        //    return Json(new
        //    {
        //        data = data.Skip(model.start).Take(model.length).ToList(),
        //        recordsTotal = data.Count,
        //        recordsFiltered = data.Count,
        //        draw = model.draw,
        //    });
        //}

    }
}