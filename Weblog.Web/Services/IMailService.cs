using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Weblog.Web.Services
{
    public interface IMailService
    {
        /// <summary>
        /// Sends the confirmation mail.
        /// </summary>
        /// <param name="emailAdress">The email adress.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="token">The token.</param>
       void SendConfirmationMail(string emailAdress, string userName, string token);
       /// <summary>
       /// Sends the password reset token.
       /// </summary>
       /// <param name="emailAdress">The email adress.</param>
       /// <param name="userName">Name of the user.</param>
       /// <param name="token">The token.</param>
       void SendPasswordResetToken(string emailAdress, string userName, string token);
       /// <summary>
       /// Sends the welcome mail.
       /// </summary>
       /// <param name="emailAdress">The email adress.</param>
       /// <param name="userName">Name of the user.</param>
       void SendWelcomeMail(string emailAdress, string userName);

    }
}
