using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Management
{
    public class SiteSettingsModel
    {
        [DisplayName("Name der Seite")]
        [Required]
        public string SiteName { get; set; }
        [DisplayName("Kommentare erlauben")]
        [Required]
        public bool AllowComments { get; set; }
        [DisplayName("Footertext")]
        [Required]
        public string SiteFooterText { get; set; }
        [DisplayName("Meta Keywords")]
        [Required]
        public string SiteKeywords { get; set; }
        [DisplayName("Blogeinträge pro Seite")]
        [Required]
        [RegularExpression(@"([^0]+)", ErrorMessage = "0 darf nicht gewählt werden.")]
        public int EntriesPerSite { get; set; }
        [DisplayName("Vollangezeigte Blogeinträge pro Seite")]
        [Required]
        [RegularExpression(@"([^0]+)", ErrorMessage = "0 darf nicht gewählt werden.")]
        public int FullEntriesPerSite { get; set; }

        public void UpdateModel(AdministratorSettings source)
        {
            this.SiteName = source.SiteName;
            this.AllowComments = source.AllowComments;
            this.SiteFooterText = source.SiteFooterText;
            this.SiteKeywords = source.SiteKeywords;
            this.EntriesPerSite = source.EntriesPerSite;
            this.FullEntriesPerSite = source.FullEntriesPerSite;
        }

        public void UpdateSource(AdministratorSettings source)
        {
            source.SiteName = this.SiteName;
            source.AllowComments = this.AllowComments;
            source.SiteFooterText = this.SiteFooterText;
            source.SiteKeywords = this.SiteKeywords;
            source.EntriesPerSite = this.EntriesPerSite;
            source.FullEntriesPerSite = this.FullEntriesPerSite;
        }

        public SiteSettingsModel() { }
        public SiteSettingsModel(AdministratorSettings settings)
        {
            UpdateModel(settings);
        }

    }
}
