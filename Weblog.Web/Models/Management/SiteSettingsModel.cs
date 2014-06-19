﻿using System;
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