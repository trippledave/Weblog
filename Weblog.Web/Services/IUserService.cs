using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Core.DataAccess.Weblog;
using Weblog.Web.Areas.Management.Models;
using Weblog.Web.Models.Account;

namespace Weblog.Web.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        UserModel GetUser(string userName);

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        UserModel GetUserByEmail(string email);

        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        /// <returns></returns>
        List<UserModel> GetAllUsers();

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="model">The model.</param>
        void CreateUser(RegisterModel model);

        /// <summary>
        /// Checks if the name exists.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        bool DoesUserNameExist(string userName);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        void ResetPassword(UserModel model);

        /// <summary>
        /// Sets the new password.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        bool SetNewPassword(string token, string password);

        /// <summary>
        /// Updates the user settings.
        /// </summary>
        /// <param name="model">The model.</param>
        void UpdateUserSettings(UserSettingsModel model);

        /// <summary>
        /// Sets the user lock.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userLocked">if set to <c>true</c> [user locked].</param>
        void SetUserLock(UserModel user, bool userLocked);

    }
}
