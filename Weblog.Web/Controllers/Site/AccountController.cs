using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Weblog.Web.Models.Account;
using WebMatrix.WebData;

namespace Weblog.Web.Controllers.Site
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Helper Methods

        private static string GetErrorString(MembershipCreateStatus createStatus)
        {
            // Vollständige Liste Fehlercodes: http://go.microsoft.com/fwlink/?LinkID=177550 

            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Der Benutzername existiert bereits";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Ein Benutzer für diese Email existiert bereits";

                case MembershipCreateStatus.InvalidPassword:
                    return "Das Passwort ist ungültig";

                case MembershipCreateStatus.InvalidEmail:
                    return "Die E-Mail-Adresse ist ungültig";

                case MembershipCreateStatus.InvalidUserName:
                    return "Der Benutzername ist ungültig";

                case MembershipCreateStatus.ProviderError:
                    return "Der Benutzer konnte wegen eines internen Fehlers nicht angemeldet werden";

                default:
                    return "Ein Fehler ist aufgetreten";
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Der eingegebene Benutzername oder das Passwort sind falsch.");

            return View(model);
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { Email = model.Email, EmailLowercase = model.Email.ToLower(), UserNameLowercase = model.UserName.ToLower() });
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", GetErrorString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }
}
