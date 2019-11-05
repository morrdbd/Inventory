using DOMoRR.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DOMoRR
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            MvcHandler.DisableMvcResponseHeader = true;
        }

        private void Application_AcquireRequestState()
        {
            var httpContext = Request.RequestContext.HttpContext;
            if(httpContext == null) return;

            var language = (RouteTable.Routes.GetRouteData(httpContext).Values["language"]??"en").ToString();

            CultureInfo culture = null;

            switch(language){
                case "dr":
                    culture = new CultureInfo("prs-AF");
                    break;
                case "pa":
                    culture = new CultureInfo("ps-AF");
                    break;
                default:
                    culture = new CultureInfo("en-US");
                    break;
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            var EnCulture = new CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat = EnCulture.DateTimeFormat;
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat = EnCulture.NumberFormat;
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Set("Server", "Apache Tomcat");
            HttpContext.Current.Response.Headers.Set("X-Powered-By", "PHP/5.6.31");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
        }
    }
}