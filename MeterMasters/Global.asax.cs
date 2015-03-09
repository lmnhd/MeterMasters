using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WePaySDK;

namespace MeterMasters
{
    public static class GlobalVars
    {
        // i know...this is lazy
        public static string hostUrl;
    }
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var context = new MMUserContext.Concrete.UserContext();
            
            context.Database.Initialize(false);
            

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
           // roleManager.Create(new IdentityRole("Admin"));
            
            

            WePayConfig.accessToken = ConfigurationManager.AppSettings["WepayAccessToken"];
            WePayConfig.accountId = Convert.ToInt32(ConfigurationManager.AppSettings["WepayAccountId"]);
            WePayConfig.clientId = Convert.ToInt32(ConfigurationManager.AppSettings["WepayClientId"]);
            WePayConfig.clientSecret = ConfigurationManager.AppSettings["WepayClientSecret"];
            WePayConfig.productionMode = Convert.ToBoolean(ConfigurationManager.AppSettings["ProductionMode"]);
        }
    }
}
