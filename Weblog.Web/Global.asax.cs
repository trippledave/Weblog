﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Weblog.Core.DataAccess.Weblog;
using Weblog.Core.Repositories;
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

            //Initialize Database
            try
            {
                WebSecurity.InitializeDatabaseConnection("SimpleMembershipConnection", "Users", "UserID", "UserName", true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Es konnte keine Verbindung zur Datenbank hergestellt werden.", ex);
            }

            if (!WebSecurity.UserExists("admin"))
            {
                WebSecurity.CreateUserAndAccount("admin", "admin", new { UserNameLowercase = "admin", Email = "admin@web.de", EmailLowercase = "admin@web.de", IsLockedByAdmin = 0 });
                WebSecurity.CreateUserAndAccount("Anonym", "Anonym", new { UserNameLowercase = "anonym", Email = "leer@web.de", EmailLowercase = "leer@web.de", IsLockedByAdmin = 1 });
                Roles.CreateRole("Administrator"); //darf eigentlich alles
                Roles.CreateRole("Benutzer");//automatisch nach Registrierung, kann Kommentare ohne Captcha eingeben
                Roles.AddUserToRole("admin", "Administrator");
            }
            initAdminSettings();

        }

        private void initAdminSettings()
        {
            IWeblogRepository _repository = new WeblogRepository();

            AdministratorSettings adminSettings = new AdministratorSettings();
            adminSettings.SiteName = "Weblog";
            adminSettings.AllowComments = true;
            adminSettings.WelcomeMailSubject = "Willkommensmail";
            adminSettings.WelcomeMailText = "Viel Spass mit unserem Blog.";
            adminSettings.PasswordChangeMailSubject = "Weblog - Passwort ändern";
            adminSettings.PasswordChangeMailText = "Der Token um ihr Passwort zu ändern: ";
            adminSettings.OptInMailSubject = "Weblog - Account aktivieren";
            adminSettings.OptInMailText = "Aktivieren Sie ihren Account mit dem folgenden Link: ";
            adminSettings.SiteFooterText = "Weblog Copyright ©2525<br/>Powered by vBulletin® Version 4.1.12 (Deutsch)";
            adminSettings.SiteKeywords = "blog, schule, mannheim";
            adminSettings.SmtpServer = "smtp.gmail.com";
            adminSettings.SmtpUser = "asp.ss2014@gmail.com";
            adminSettings.SmtpPassword = "ss2014.asp.blog";
            adminSettings.SmtpRegisterAtServerNeeded = true;
            adminSettings.EntriesPerSite = 3;
            adminSettings.FullEntriesPerSite = 1; //nur der erste Post wird standardmäßig komplett dargestellt.
            _repository.SetAdministratorSettings(adminSettings);
             
        }
    }
}