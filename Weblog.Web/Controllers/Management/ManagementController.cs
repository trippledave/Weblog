using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Weblog.Web.Areas.Management.Models;
using Weblog.Web.Models.Account;
using Weblog.Web.Models.Management;
using Weblog.Web.Services;
using WebMatrix.WebData;

namespace Weblog.Web.Areas.Management.Controllers
{
    [Authorize]
    public class ManagementController : Controller
    {
        private IUserService _userService = new UserService();
        private ISettingsService _settingsService = new SettingsService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeUserSettings()
        {
            UserModel currentUser = _userService.GetUser(WebSecurity.CurrentUserName);
            UserSettingsModel model = new UserSettingsModel(currentUser);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ChangeUserSettings(UserSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel currentUser = _userService.GetUser(WebSecurity.CurrentUserName);
                UserModel userFromEmail = _userService.GetUserByEmail(model.Email);
                //checks if the email does not belong to anyone else, and if the email matches the current users password.
                if (userFromEmail == null || userFromEmail.UserName == currentUser.UserName)
                {
                    _userService.UpdateUserSettings(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Die Email ist im System schon vorhanden.");
                }
            }
            return View(model);
        }

        public ActionResult ChangeUserPassword()
        {
            UserPasswordModel model = new UserPasswordModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ChangeUserPassword(UserPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.RepeatPassword)
                {
                    ModelState.AddModelError("", "Die Passwörter müssen übereinstimmen.");
                }
                if (WebSecurity.ChangePassword(WebSecurity.CurrentUserName, model.OldPassword, model.Password))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Ihr altes Passwort ist falsch.");
                    return View(model);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ChangeSiteSettings()
        {
            SiteSettingsModel model = _settingsService.GetSiteSettings();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ValidateInput(false)] //admin darf das.
        [Authorize(Roles = "Administrator")]
        public ActionResult ChangeSiteSettings(SiteSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                _settingsService.SetSiteSettings(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ChangeEmailSettings()
        {
            EmailSettingsModel model = _settingsService.GetEmailSettings();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ValidateInput(false)] //admin darf das.
        [Authorize(Roles = "Administrator")]
        public ActionResult ChangeEmailSettings(EmailSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SmtpRegisterAtServerNeeded && (model.SmtpPassword == null || model.SmtpUser == null))
                {
                    ModelState.AddModelError("", "Es muss ein Passwort und Benutzername vorhanden sein.");
                }
                else
                {
                    _settingsService.SetEmailSettings(model);
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}
