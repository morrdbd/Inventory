using Inventory.Attributes;
using Inventory.Controllers;
using Inventory.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOMoRR.Controllers
{
    public class DistributionTicketPC5Controller : BaseController
    {
        //[AccessControl("Add")]
        public ActionResult Add()
        {
            var model = new DistTicketPC5ViewModels();
            return View("Form",model);
        }
        public ActionResult ViewPC5()
        {
            return View();
        }
       
    }
}