using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Weblog.Core.DataAccess.Weblog;
using Weblog.Core.Repositories;
using Weblog.Web.Areas.Management.Models;
using Weblog.Web.Models.Account;
using WebMatrix.WebData;

namespace Weblog.Web.Services
{
    public class UserService : IUserService
    {
        private IWeblogRepository _repository = new WeblogRepository();
        private string _defaultRole = "Benutzer";

        public UserModel GetUser(string userName)
        {
            User user = _repository.GetUser(userName);
            return user == null ? null : new UserModel(user);
        }

        public UserModel GetUserByEmail(string email)
        {
            User user = _repository.GetUserByEmail(email);
            return user == null ? null : new UserModel(user);
        }

        public List<UserModel> GetAllUsers()
        {
            List<User> userList = _repository.GetAllUsers();
            List<UserModel> returnList = new List<UserModel>();
            foreach (User item in userList)
            {
                UserModel modelToAdd = new UserModel(item);
                returnList.Add(modelToAdd);
            }
            return returnList;
        }

        public void CreateUser(RegisterModel model)
        {
            if (_repository.GetUser(model.UserName) == null)
            {
                string confirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, model.Password,
                    new
                    {
                        UserNameLowerCase = model.UserName.ToLower(),
                        Email = model.Email,
                        EmailLowerCase = model.Email.ToLower(),
                        IsLockedByAdmin = 0
                    }, true);
                Roles.AddUserToRole(model.UserName, _defaultRole);

                IMailService mailService = new MailService();
                mailService.SendConfirmationMail(model.Email, model.UserName, confirmationToken);
            }
            else
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateEmail);

            }
        }

        public bool DoesUserNameExist(string userName)
        {
            return Membership.GetUser(userName) != null;
        }

        public bool DoesDisplayNameExist(string displayName)
        {
            return _repository.DoesDisplayNameExist(displayName);
        }

        public void ForgotPassword(UserModel model)
        {
            IMailService mailService = new MailService();
            User user = _repository.GetUser(model.UserName);
            string resetToken = WebSecurity.GeneratePasswordResetToken(user.UserName);
            mailService.SendPasswordForgottenMail(user.Email, user.UserName, resetToken);
        }

        public void UpdateUserSettings(UserSettingsModel model)
        {
            User currentUser = _repository.GetUser(WebSecurity.CurrentUserName);
            currentUser.Email = model.Email;
            currentUser.EmailLowercase = model.Email.ToLower();
            currentUser.DisplayName = model.DisplayName;
            _repository.UpdateUserSettings(currentUser);
        }

        public void SetUserLock(UserModel user, bool userLocked)
        {
            User currentUser = _repository.GetUser(user.UserName);
            currentUser.IsLockedByAdmin = userLocked;
            _repository.UpdateUserSettings(currentUser);
        }
    }
}
