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
        /// Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        bool UpdatePassword(User user, string currentPassword, string newPassword);

        /// <summary>
        /// Updates the user settings.
        /// </summary>
        /// <param name="model">The model.</param>
        void UpdateUserSettings(UserSettingsModel model);

    }
}
