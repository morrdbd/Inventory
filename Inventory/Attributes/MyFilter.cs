using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Attributes
{
    public class MyFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            var action = filterContext.ActionDescriptor.ActionName.ToLower();
            var isAjax = filterContext.HttpContext.Request.IsAjaxRequest();

            if (!(controller == "home" && action == "index") && !isAjax)
            {
                var browser = filterContext.HttpContext.Request.Browser;
                var name = browser.Browser;
                var version = Convert.ToDouble(browser.Version);

                var allowed = System.Configuration.ConfigurationManager.AppSettings["AllowedBrowsers"];
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<browser[]>(allowed);
                foreach (var obj in json)
                {
                    if (name == obj.name && version >= obj.version) return;
                }
                filterContext.Result = new ViewResult { ViewName = "BrowserError" };
            }
        }

        private class browser
        {
            public string name { get; set; }
            public float version { get; set; }
        }
    }
}