using Inventory.Models;
using System.Web.Mvc;
using Inventory.Models.ViewModels;
using Inventory.Models.Repositories;
using Inventory.Attributes;
using System.Linq;
using Inventory.Models.Procedures;
using System;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using Resources;

namespace Inventory.Controllers
{
    public class HomeController : BaseController
    {
        private InventoryDBContext db = new InventoryDBContext();
        private ProductRepocitory repo = new ProductRepocitory();
        public ActionResult Index()
        {
            ViewBag.DateFrom = Funcs.DateFrom("en");
            ViewBag.DateTo = Funcs.DateTo("en");
            return View();
        }
        public ActionResult HomeAjax(Home_SC model)
        {
            try
            {
                model._DateFrom = model.DateFrom.ToDate("en");
                model._DateTo = model.DateTo.ToDate("en");
                model.Lang = Language;
                var json = new
                {
                   
                };
                return Json(json, JsonRequestBehavior.AllowGet);
        }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return Json(false, JsonRequestBehavior.AllowGet);
    }
}

        [AccessControl]
        public ActionResult Dashboard()
        {
        
            ViewBag.DateFrom = Funcs.DateFrom(Language);
            ViewBag.DateTo = Funcs.DateTo(Language);
            return View();
        }
        public JsonResult DashboardAjax()
        {
            DashboardCharts model = new DashboardCharts();
            model.Requisition = repo.AllMobileCarRequestChart(Language);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
