using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Web.Models.Management;

namespace Weblog.Web.Services
{
    public interface ISettingsService
    {
        /// <summary>
        /// Gets the email settings.
        /// </summary>
        /// <returns></returns>
         EmailSettingsModel GetEmailSettings();

         /// <summary>
         /// Sets the email settings.
         /// </summary>
         /// <param name="settings">The settings.</param>
        void SetEmailSettings(EmailSettingsModel settings);

        /// <summary>
        /// Gets the site settings.
        /// </summary>
        /// <returns></returns>
        SiteSettingsModel GetSiteSettings();

        /// <summary>
        /// Sets the site settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void SetSiteSettings(SiteSettingsModel settings);
       
    }
}
