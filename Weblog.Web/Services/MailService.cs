using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Weblog.Web.Controllers.Site;

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
            mailClient = new SmtpClient();
            mailClient.Port = 587;
            mailClient.Host = "smtp.gmail.com";
            mailClient.EnableSsl = true;
            mailClient.Timeout = 10000;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            //mailClient.Credentials = new System.Net.NetworkCredential(defaultMail, defaultPassword);
            
        }

        public void SendConfirmationMail(string emailAdress, string userName, string token)
        {
            string subject = "Weblog - Account aktivieren";
            string link = "http://" + activationAction + token;
            string body = "Hello "+userName+",<br /> aktivieren Sie ihren Account mit dem folgenden Link:<br /> <a href=\""+link+"\">"+link+"</a>";
            SendMail(emailAdress, subject, body);
        }

        public void SendPasswordResetToken(string emailAdress, string userName, string token)
        {
            string subject = "Weblog - Passwortreset";
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
