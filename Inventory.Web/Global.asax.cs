using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Inventory.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            bool isAjaxCall = string.Equals("XMLHttpRequest", Context.Request.Headers["x-requested-with"], StringComparison.OrdinalIgnoreCase);

            Exception exception = HttpContext.Current.Server.GetLastError();

            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

            logger.Trace(exception);//(exception,"Is Ajax Error: "+isAjaxCall)
            string exceptionMsg = exception.Message;
            Context.ClearError();
            if (isAjaxCall)
            {
                Response.Clear();
                Response.TrySkipIisCustomErrors = true;
                Response.ContentType = "application/json";
                //Response.StatusCode = 500;
                Response.Write(exceptionMsg);
                Response.End();
            }
            else
            {
                Server.ClearError();
                //Session["error"] = exceptionMsg;
                Session["error"] = "An error occurred while processing your request. Please contact administrator for further information.";
                Response.Redirect("/Error/Error");
            }
        }
    }
}
