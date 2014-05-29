using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace Weblog.Web
{
    // Hinweis: Anweisungen zum Aktivieren des klassischen Modus von IIS6 oder IIS7 
    // finden Sie unter "http://go.microsoft.com/?LinkId=9394801".

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            try
            {
                WebSecurity.InitializeDatabaseConnection("SimpleMembershipConnection", "Users", "UserID", "UserName", true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Es konnte keine Verbindung zur Datenbank hergestellt werden.", ex);
            }

            if (!WebSecurity.UserExists("Admin"))
            {
                WebSecurity.CreateUserAndAccount("Admin", "Admin", new { UserNameLowercase = "admin", Email = "de@epp.de", EmailLowercase = "de@epp.de", IsUserLocked = 0 });
                Roles.CreateRole("Administrator");
                Roles.CreateRole("Autor");
                Roles.CreateRole("Benutzer");
                Roles.AddUserToRole("Admin", "Administrator");
            }

        }
    }
}