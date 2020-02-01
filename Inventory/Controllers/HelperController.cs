using Inventory.Attributes;
using Inventory.Models;
using Inventory.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    [AccessControl]
    public class HelperController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();

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


        public JsonResult LoadProductInfo(int productCode = 0)
        {
            var _Product = db.Products.Where(t => t.IsActive == true && t.ProductCode == productCode).ToList();
            var productInfo = _Product.Select(p => new
            {
                p.ProductCode,
                ProductName = p.ProductName,
                ProductSize = AdminRepo.LookupName(Language, p.SizeID),
                ProductCategory = AdminRepo.LookupName(Language, p.CategoryID),
                ProductGroup = AdminRepo.LookupName(Language, p.GroupID),
                ProductOrigin = AdminRepo.LookupName(Language, p.OriginID),
                UnitName = AdminRepo.LookupName(Language, p.UnitID)
            }).FirstOrDefault();
            return Json(productInfo, JsonRequestBehavior.AllowGet);
        }
    }
}