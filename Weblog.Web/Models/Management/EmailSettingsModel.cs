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
    public class EmailSettingsModel
    {
        [DisplayName("Betreff der Willkommensmail")]
        public string WelcomeMailSubject { get; set; }
        [DisplayName("Text der Willkommensmail")]
        public string WelcomeMailText { get; set; }
        [DisplayName("Betreff der Passwortänderungsmail")]
        public string PasswordChangeMailSubject { get; set; }
        [DisplayName("Text der Passwortänderungsmail")]
        public string PasswordChangeMailText { get; set; }
        [DisplayName("Betreff der Opt-In-Mail")]
        public string OptInMailSubject { get; set; }
        [DisplayName("Text der Opt-In-Mail")]
        public string OptInMailText { get; set; }
        [DisplayName("SMTP Serveradresse")]
        [Required]
        public string SmtpServer { get; set; }
        [DisplayName("SMTP Benutzername")]
        public string SmtpUser { get; set; }
        [DisplayName("SMTP Passwort")]
        public string SmtpPassword { get; set; }
        [DisplayName("Anmeldung am Server erforderlich")]
        [Required]
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
