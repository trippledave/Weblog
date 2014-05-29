using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Weblog.Core.DataAccess.Weblog;
using Weblog.Core.Repositories;
using Weblog.Web.Models.Account;
using WebMatrix.WebData;

namespace Weblog.Web.Services
{
    public class UserService
    {
        private IWeblogRepository _repository = new WeblogRepository();
        private string _defaultRole = "User";

        public UserModel GetUser(string userName)
        {
            User user = _repository.GetUser(userName);
            return user == null ? new UserModel() : new UserModel(user);
        }


        public void CreateUser(RegisterModel model)
        {
            MailService mailService = new MailService();
            string confirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, model.Password,
                new
                {
                    UserNameLowerCase = model.UserName.ToLower(),
                    Email = model.Email,
                    EmailLowerCase = model.Email.ToLower(),
                    IsUserLocked = 1,
                    IsUserLockedByAdmin = 0
                }, true);
            mailService.SendConfirmationMail(model.Email, model.UserName, confirmationToken);
            Roles.AddUserToRole(model.UserName, _defaultRole);
            WebSecurity.Login(model.UserName, model.Password, false);
        }

        public bool UserNameExists(string userName)
        {
            return Membership.GetUser(userName) != null;
        }

        public void ResetPassword(UserModel model)
        {
            throw new NotImplementedException();

            //MailService mailService = new MailService();
            //User user = _repository.GetUserByEmail(email);
            //string resetToken = WebSecurity.GeneratePasswordResetToken(user.UserName);
            //mailService.SendPasswordResetToken(email, user.UserName, resetToken);
        }

        public bool SetNewPassword(string token, string password)
        {
            return WebSecurity.ResetPassword(token, password);
        }

        internal bool UpdatePassword(string currentPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(WebSecurity.CurrentUserName, currentPassword, newPassword);
        }

        public void UpdateEmail(string oldEmail, string newEmail)
        {
            _repository.UpdateEmail(oldEmail, newEmail);
        }
    }
}
