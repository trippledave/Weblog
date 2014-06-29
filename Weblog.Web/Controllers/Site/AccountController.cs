using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Weblog.Web.Models.Account;
using Weblog.Web.Services;
using WebMatrix.WebData;

namespace Weblog.Web.Controllers.Site
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserService _userService = new UserService();
        private IMailService _mailService = new MailService();
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
                return RedirectToAction("Index", "Blog");
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
        [ValidateAntiForgeryToken()]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid &&
                _userService.GetUser(model.UserName) != null && (!_userService.GetUser(model.UserName).IsLockedByAdmin) &&
                WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Sie wurden gesperrt oder der eingegebene Benutzername oder das Passwort sind falsch.");

            return View(model);
        }

        public ActionResult LogOut()
        {
            if (WebSecurity.IsAuthenticated)
            {
                WebSecurity.Logout();
            }
            return RedirectToAction("Index", "Blog");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken()]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    _userService.CreateUser(model);
                    return RedirectToAction("Index", "Blog");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", GetErrorString(e.StatusCode));
                }
                catch (System.Net.Mail.SmtpException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ConfirmUser(string id)
        {
            if (WebSecurity.IsAuthenticated)
            {
                WebSecurity.Logout();
            }
            if (WebSecurity.ConfirmAccount(id))
            {
                return RedirectToAction("ConfirmationSuccess");
            }
            return RedirectToAction("Index", "Errors", null);
        }

        public ActionResult ConfirmationSuccess()
        {
            UserModel currentUser = _userService.GetUser(WebSecurity.CurrentUserName);
            _mailService.SendWelcomeMail(currentUser.Email, currentUser.UserName);
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            ForgotPasswordModel model = new ForgotPasswordModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken()]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = _userService.GetUserByEmail(model.Email);
                if (user != null)
                {
                    String token = WebSecurity.GeneratePasswordResetToken(user.UserName);
                    _mailService.SendPasswordForgottenMail(user.Email, user.UserName, token);
                }
                return RedirectToAction("EnterNewPassword");

            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult EnterNewPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken()]
        public ActionResult EnterNewPassword(EnterNewPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.RepeatPassword)
                {
                    ModelState.AddModelError("", "Die Passwörter müssen übereinstimmen.");
                }
                else if (WebSecurity.ResetPassword(model.Token, model.Password))
                {
                    return View("NewPasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "Ihr Passwort konnte leider nicht geändert werden.");
                }
            }
            return View(model);
        }

    }
}
