using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Management
{
    public class EmailSettingsModel
    {

        public string WelcomeMailSubject { get; set; }
        public string WelcomeMailText { get; set; }
        public string PasswordChangeMailSubject { get; set; }
        public string PasswordChangeMailText { get; set; }
        public string OptInMailSubject { get; set; }
        public string OptInMailText { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public bool SmtpRegisterAtServerNeeded { get; set; }

        public void UpdateModel(AdministratorSettings source)
        {
            this.WelcomeMailSubject = source.WelcomeMailSubject;
            this.WelcomeMailText = source.WelcomeMailText;
            this.PasswordChangeMailSubject = source.PasswordChangeMailSubject;
            this.PasswordChangeMailText = source.PasswordChangeMailText;
            this.OptInMailSubject = source.OptInMailSubject;
            this.OptInMailText = source.OptInMailText;
            this.SmtpServer = source.SmtpServer;
            this.SmtpUser = source.SmtpUser;
            this.SmtpPassword = source.SmtpPassword;
            this.SmtpRegisterAtServerNeeded = source.SmtpRegisterAtServerNeeded;
        }

        public void UpdateSource(AdministratorSettings source)
        {
            source.WelcomeMailSubject = this.WelcomeMailSubject;
            source.WelcomeMailText = this.WelcomeMailText;
            source.PasswordChangeMailSubject = this.PasswordChangeMailSubject;
            source.PasswordChangeMailText = this.PasswordChangeMailText;
            source.OptInMailSubject = this.OptInMailSubject;
            source.OptInMailText = this.OptInMailText;
            source.SmtpServer = this.SmtpServer;
            source.SmtpUser = this.SmtpUser;
            source.SmtpPassword = this.SmtpPassword;
            source.SmtpRegisterAtServerNeeded = this.SmtpRegisterAtServerNeeded;
        }


        public EmailSettingsModel() { }

        public EmailSettingsModel(AdministratorSettings settings)
        {
            UpdateModel(settings);
        }
    }
}
