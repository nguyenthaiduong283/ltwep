using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Services.Description;

namespace LiteCommerce.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string connectionString = ConfigurationManager
                .ConnectionStrings["LiteCommerceDB"].ConnectionString;
            HRService.Init(DatabaseTypes.SQLServer, connectionString);
            DataService.Init(DatabaseTypes.SQLServer, connectionString);
            ProductService.Init(DatabaseTypes.SQLServer, connectionString);
            ReportSercvice.Init(DatabaseTypes.SQLServer, connectionString);
            SellService.Init(DatabaseTypes.SQLServer, connectionString);
            AccountService.Init(DatabaseTypes.SQLServer, connectionString, AccountTypes.Employee);
        }
    }
}
