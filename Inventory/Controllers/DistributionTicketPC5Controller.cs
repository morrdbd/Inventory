using Inventory.Attributes;
using Inventory.Controllers;
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
            return View("Form");
        }
    }
}