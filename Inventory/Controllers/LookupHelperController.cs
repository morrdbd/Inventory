using Inventory.Attributes;
using Inventory.Controllers;
using Inventory.Models;
using Inventory.Models.Repositories;
using Inventory.Models.Tables;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    [AccessControl]
    public class LookupHelperController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();


        public JsonResult CategoryAjax(string id = "0")
        {
            var categories = AdminRepo.LookupValueList(Language, "PRODUCTCATEGORY");
            var _categories = new SelectList(categories, "ValueId", TextField);
            return Json(_categories, JsonRequestBehavior.AllowGet);
        }

    }
}
