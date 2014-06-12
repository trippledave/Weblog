using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Weblog.Web.Areas.Management.Models;
using Weblog.Web.Models.Account;
using Weblog.Web.Services;
using WebMatrix.WebData;

namespace Weblog.Web.Areas.Management.Controllers
{
    [Authorize]
    public class ManagementController : Controller
    {
        private IUserService _userService = new UserService();

        public ActionResult Index()
        {
            if (Roles.GetRolesForUser().Contains("Administrator"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserSettings");
            }
        }

        public ActionResult ChangeUserSettings()
        {
            UserSettingsModel model = new UserSettingsModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeUserSettings(UserSettingsModel model)
        {
            if (ModelState.IsValid)
            {
               //todo gibts email schon in DB
                if (model.Password != model.RepeatPassword)
                {
                    ModelState.AddModelError("PasswordsDidNotMatch", "Die Passwörter müssen übereinstimmen.");
                }
                return View(model);
            }
            return View(model);
        }

        public ActionResult ChangeUserPassword()
        {
            UserPasswordModel model = new UserPasswordModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeUserPassword(UserPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                //todo gibts email schon in DB
                if (model.Password != model.RepeatPassword)
                {
                    ModelState.AddModelError("PasswordsDidNotMatch", "Die Passwörter müssen übereinstimmen.");
                }
                return View(model);
            }
            return View(model);
        }
    }
}
