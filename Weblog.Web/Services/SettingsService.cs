using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Core.DataAccess.Weblog;
using Weblog.Core.Repositories;
using Weblog.Web.Models.Management;

namespace Weblog.Web.Services
{
    public class SettingsService : ISettingsService
    {

        private Guid _settingsID = Guid.Empty;
        private IWeblogRepository _repository = new WeblogRepository();
        
        public EmailSettingsModel GetEmailSettings()
        {
            return new EmailSettingsModel(_repository.GetAdministratorSettings());
        }

        public void SetEmailSettings(EmailSettingsModel settings)
        {
            AdministratorSettings adminSettings = _repository.GetAdministratorSettings();
            settings.UpdateSource(adminSettings);
            _repository.UpdateAdministratorSettings(adminSettings);
        }

        [Authorize(Roles = "Administrator")]
        public SiteSettingsModel GetSiteSettings()
        {
            return new SiteSettingsModel(_repository.GetAdministratorSettings());
        }

        public void SetSiteSettings(SiteSettingsModel settings)
        {
            AdministratorSettings adminSettings = _repository.GetAdministratorSettings();
            settings.UpdateSource(adminSettings);
            _repository.UpdateAdministratorSettings(adminSettings);
        }
    }
}