using Inventory.Attributes;
using Inventory.Models;
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
    public class LookupsController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();
        //******************************************************************************
        //***************************Product***********************************************
        //******************************************************************************

        //------------------------
        [HttpGet]
        [AccessControl("Add")]
        public ActionResult ProductAdd()
        {
            CreateDropDown();
            return View(new Product());
        }

        private void CreateDropDown()
        {
            var usageType = AdminRepo.LookupValueList(Language, "UTYPE");
            ViewBag.UsageTypeDrp = new SelectList(usageType, "ValueId", TextField);

            var groupList = AdminRepo.LookupValueList(Language, "PRODUCTGROUP");
            ViewBag.GroupDrp = new SelectList(groupList, "ValueId", TextField);

            var unitList = AdminRepo.LookupValueList(Language, "UNIT"); 
            ViewBag.UnitDrp = new SelectList(unitList, "ValueId", TextField);

            var packingList = AdminRepo.LookupValueList(Language, "PRODUCTPACKAGE"); 
            ViewBag.PackingDrp = new SelectList(packingList, "ValueId", TextField);

            var sizeList = AdminRepo.LookupValueList(Language, "PRODUCTSIZE");
            ViewBag.SizeDrp = new SelectList(sizeList, "ValueId", TextField);

            var originList = AdminRepo.LookupValueList(Language, "PRODUCTORIGIN");  
            ViewBag.OriginDrp = new SelectList(originList, "ValueId", TextField);

            var brandList = AdminRepo.LookupValueList(Language, "PRODUCTBRAND"); 
            ViewBag.BrandDrp = new SelectList(brandList, "ValueId", TextField);


        }
        [AccessControl("Add")]
        public JsonResult SaveProduct(Product model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string _action = "";
            try
            {
                if (ModelState.IsValid == true)
                {
                    if (model.ProductID == 0)
                    {
                        var Any = db.Products.Any(i => i.GroupID == model.GroupID && i.ProductName == model.ProductName);
                        if (Any)
                        {
                            return Json("duplicate");
                        }
                        _action = "Insert";
                        var _ProductCode = db.Products.OrderByDescending(p => p.ProductCode).Select(p => p.ProductCode).FirstOrDefault() + 1;
                        model.ProductCode = _ProductCode;
                        model.IsActive = true;
                        model.InsertedBy = AppUser.Id;
                        model.InsertedDate = DateTime.Now;
                        db.Products.Add(model);
                    }
                    else
                    {
                        _action = "Update";
                        model.LastUpdatedBy = AppUser.Id;
                        model.LastUpdatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                        db.Entry(model).Property(x => x.ProductCode).IsModified = false;
                        db.Entry(model).Property(x => x.ImagePath).IsModified = false;
                        db.Entry(model).Property(x => x.IsActive).IsModified = false;
                        db.Entry(model).Property(x => x.InsertedBy).IsModified = false;
                        db.Entry(model).Property(x => x.InsertedDate).IsModified = false;

                    }
                    db.SaveChanges();
                    //Should remove file to store log
                    var _logObj = new
                    {
                        model.ProductID,
                        model.UsageTypeID,
                        model.ProductName,
                        model.ProductCode,
                        model.UnitID,
                        model.GroupID,
                        model.CategoryID,
                        model.PackingID,
                        model.SizeID,
                        model.OriginID,
                        model.BrandID,
                        model.Description
                    };
                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Products",
                        ModfiedId = model.ProductID,
                        Action = _action,
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_logObj),
                    });
                    db.SaveChanges();
                    SaveProductImage(model);
                    return Json(true, JsonRequestBehavior.AllowGet);
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

        [AccessControl("Search")]
        [HttpGet]
        public ActionResult ProductSearch()
        {
            ViewBag.search = new ProductSearch();
            CreateDropDown();
            return View();
        }

        [AccessControl("Search")]
        public JsonResult ListProduct(ProductSearch model)
        {
            var query = db.Products.Where(t => t.IsActive == true);

            ViewBag.search = model;
           
            if (model.UsageTypeID != null && model.UsageTypeID != 0)
            {
                query = query.Where(c => c.UsageTypeID == model.UsageTypeID);
            }
            if (model.GroupID != null && model.GroupID != 0)
            {
                query = query.Where(c => c.GroupID == model.GroupID);
            }
            if (model.CategoryID != null && model.CategoryID != 0)
            {
                query = query.Where(c => c.CategoryID == model.CategoryID);
            }
            if (!string.IsNullOrWhiteSpace(model.ProductName))
            {
                query = query.Where(c => c.ProductName.Contains(model.ProductName));
            }
            var records = query.OrderBy(t => t.GroupID).ToList();
            var data = records.Select(i => new
            {
                i.ProductID,
                UsageType = db.LookupValues.Where(l => l.IsActive == true && l.ValueId == i.UsageTypeID).Select(l => Language == "prs" ? l.DrName : Language == "ps" ? l.PaName : l.EnName).FirstOrDefault(),
                i.ProductName,
                i.ProductCode,
                Group = AdminRepo.LookupName(Language,i.GroupID),
                Category = AdminRepo.LookupName(Language, i.CategoryID),
                Unit = AdminRepo.LookupName(Language, i.UnitID),
                Packing = AdminRepo.LookupName(Language, i.PackingID),
                Size = AdminRepo.LookupName(Language, i.SizeID),
                Origin = AdminRepo.LookupName(Language, i.OriginID),
                Brand = AdminRepo.LookupName(Language, i.BrandID),
                i.ImagePath
            }).ToList();
            return Json(new
            {
                data = data.Skip(model.start).Take(model.length).ToList(),
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                draw = model.draw,
            });
        }

        [AccessControl("View")]
        public JsonResult ProductGet(int id = 0)
        {
            var query = db.Products.Where(p => p.ProductID == id).ToList();
            var  obj = query.Select(p=> new{
                UsageTypeName = AdminRepo.LookupName(Language, p.UsageTypeID),
                p.ProductName,
                p.ProductCode,
                GroupName = AdminRepo.LookupName(Language, p.GroupID),
                CategoryName = AdminRepo.LookupName(Language, p.CategoryID),
                UnitName = AdminRepo.LookupName(Language, p.UnitID),
                PackingName = AdminRepo.LookupName(Language, p.PackingID),
                SizeName = AdminRepo.LookupName(Language, p.SizeID),
                OriginName = AdminRepo.LookupName(Language, p.OriginID),
                BrandName = AdminRepo.LookupName(Language, p.BrandID),
                p.SerialCodeNumber,
                p.Description,
                p.ImagePath
            }).FirstOrDefault();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Delete")]
        public JsonResult ProductDelete(int id = 0)
        {
            try
            {
                var obj = db.Products.Find(id);
                if (obj != null)
                {
                    obj.IsActive = false;
                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Products",
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
        public ActionResult ProductEdit(int id = 0)
        {
            CreateDropDown();
            var product = db.Products.SingleOrDefault(t => t.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("ProductAdd", product);
        }

        public void SaveProductImage(Product model)
        {
            if (model.FileContent != null)
            {
                string FileName = Path.GetFileNameWithoutExtension(model.FileContent.FileName);

                string FileExtension = Path.GetExtension(model.FileContent.FileName);

                FileName = model.ProductID + FileExtension;
                string productIamgesFolder = ConfigurationManager.AppSettings["ProductsImages"].ToString();
                var filePathForDB = productIamgesFolder + FileName;
                var UploadedFilePath = Server.MapPath(productIamgesFolder + FileName);

                var modelInDb = db.Products.Where(s => s.ProductID == model.ProductID).FirstOrDefault();
                modelInDb.ImagePath = filePathForDB;
                db.SaveChanges();
                model.FileContent.SaveAs(UploadedFilePath);
            }
        }
    }
}