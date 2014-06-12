using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Web.Services;

namespace Weblog.Web.Controllers.Site
{
    public class ViewController : Controller
    {
        private ISettingsService _settingsService = new SettingsService();

        public String Keywords()
        {
            return _settingsService.GetSiteSettings().SiteKeywords;
        }

        public String Footer()
        {
            return _settingsService.GetSiteSettings().SiteFooterText;
        }

                public String Sitename()
        {
            return _settingsService.GetSiteSettings().SiteName;
        }
    }
}
