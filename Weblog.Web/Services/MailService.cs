using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Weblog.Core.DataAccess.Weblog;
using Weblog.Core.Repositories;
using Weblog.Web.Controllers.Site;

namespace Weblog.Web.Services
{
    public class MailService : IMailService
    {
        private SmtpClient _mailClient;
        private AdministratorSettings _adminSettings;

        public MailService()
        {
             IWeblogRepository _repository = new WeblogRepository();
            _adminSettings=_repository.GetAdministratorSettings();

            _mailClient = new SmtpClient();
            _mailClient.Port = 587;
            _mailClient.Host = _adminSettings.SmtpServer;
            _mailClient.EnableSsl = true;
            _mailClient.Timeout = 10000;
            _mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _mailClient.UseDefaultCredentials = false;
            if(_adminSettings.SmtpRegisterAtServerNeeded )
                _mailClient.Credentials = new System.Net.NetworkCredential(_adminSettings.SmtpUser, _adminSettings.SmtpPassword);
            
        }

        public void SendConfirmationMail(string emailAdress, string userName, string token)
        {
            string activationAction = HttpContext.Current.Request.Url.Authority.ToString() + "/Account/ConfirmUser/";
            string link = "http://" + activationAction + token;
            string body = "Hallo "+userName+",<br />"+_adminSettings.OptInMailText+"<br /> <a href=\""+link+"\">"+link+"</a>";
            SendMail(emailAdress, _adminSettings.OptInMailSubject, body);
        }

        public void SendPasswordForgottenMail(string emailAdress, string userName, string token)
        {
            string body = "Hallo " + userName + ",<br />" + _adminSettings.PasswordChangeMailText + token;
            SendMail(emailAdress, _adminSettings.PasswordChangeMailSubject, body);
        }

        private void SendMail(string toEmail, string subject, string body)
        {
            MailMessage mail = new MailMessage(_adminSettings.SmtpUser, toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
                _mailClient.Send(mail);


        }

        public void SendWelcomeMail(string emailAdress, string userName)
        {
            string body = "Hallo " + userName + ",<br />" + _adminSettings.WelcomeMailText;
            SendMail(emailAdress, _adminSettings.WelcomeMailSubject, body);
        }
    }
}
