using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Weblog.Web.Services
{
    public class MailService
    {
        private SmtpClient mailClient;
        private string defaultMail = "asp.ss2014@gmail.com";
        private string defaultPassword = "ss2014.asp.blog";
        private string activationAction = HttpContext.Current.Request.Url.Authority.ToString()+ "/Account/ConfirmUser/";

        public MailService()
        {
            this.mailClient = new SmtpClient();
            this.mailClient.Port = 587;
            this.mailClient.Host = "smtp.gmail.com";
            this.mailClient.EnableSsl = true;
            this.mailClient.Timeout = 10000;
            this.mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.mailClient.UseDefaultCredentials = false;
            this.mailClient.Credentials = new System.Net.NetworkCredential(defaultMail, defaultPassword);
        }

        public void SendConfirmationMail(string emailAdress, string userName, string token)
        {
            string subject = "Weblog - Activate Account";
            string body = String.Format("Hello {0},<br /> activate your account with the following link.<br /> <a href=\"{1}{2}\">{1}{2}</a>", userName, activationAction, token);
            SendMail(emailAdress, subject, body);
        }

        public void SendPasswordResetToken(string emailAdress, string userName, string token)
        {
            string subject = "Zinnet - Reset Password";
            string body = String.Format("Hello {0},<br /> Token to rest you password: {1}", userName, token);
            SendMail(emailAdress, subject, body);
        }

        private void SendMail(string toEmail, string subject, string body)
        {
            MailMessage mail = new MailMessage(defaultMail, toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mailClient.Send(mail);
        }
    }
}
