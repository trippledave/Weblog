using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Management
{
    public class SiteSettingsModel
    {
        public string SiteName { get; set; }
        public bool AllowComments { get; set; }
        public string SiteFooterText { get; set; }
        public string SiteKeywords { get; set; }
        public int EntrysPerSite { get; set; }

        public void UpdateModel(AdministratorSettings source)
        {
            this.SiteName = source.SiteName;
            this.AllowComments = source.AllowComments;
            this.SiteFooterText = source.SiteFooterText;
            this.SiteKeywords = source.SiteKeywords;
            this.EntrysPerSite = source.EntrysPerSite;
        }

        public void UpdateSource(AdministratorSettings source)
        {
            source.SiteName = this.SiteName;
            source.AllowComments = this.AllowComments;
            source.SiteFooterText = this.SiteFooterText;
            source.SiteKeywords = this.SiteKeywords;
            source.EntrysPerSite = this.EntrysPerSite;
        }

        public SiteSettingsModel() { }
        public SiteSettingsModel(AdministratorSettings settings)
        {
            UpdateModel(settings);
        }

    }
}
