using DOMoRR.Models;
using System.Web.Mvc;
using DOMoRR.Models.ViewModels;
using DOMoRR.Models.Repositories;
using DOMoRR.Attributes;
using System.Linq;
using DOMoRR.Models.Procedures;
using System;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using Resources;

namespace DOMoRR.Controllers
{
    public class HomeController : BaseController
    {
        private DOMoRRDBContext db = new DOMoRRDBContext();
        private ClientsRepocitory repo = new ClientsRepocitory();
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

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
