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
            if (WebSecurity.ConfirmAccount(id))
            {
                return RedirectToAction("ConfirmationSuccess");
            }
            return RedirectToAction("ConfirmationFailure");
        }

        public ActionResult ConfirmationSuccess()
        {
            UserModel currentUser = _userService.GetUser(WebSecurity.CurrentUserName);
            IMailService mailService = new MailService();
            mailService.SendWelcomeMail(currentUser.Email, currentUser.UserName);
            return View();
        }

        [AllowAnonymous]
        public ActionResult ConfirmationFailure()
        {
            return View();
        }

           public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(UserModel model)
        {
            return View();

            //if (ModelState.IsValid && _userService.EmailExists(model.Email))
            //{
            //    _userService.ResetPassword(model.Email);
            //    return View("NewPassword", new NewPasswordModel());
            //}
            //else
            //{
            //    return View(model);
            //}
        }

        [HttpPost]
        public ActionResult NewPassword(UserModel model)
        {
            return View();


            //if (ModelState.IsValid && userService.SetNewPassword(model.Token, model.Password))
            //{
            //    return View("ResetPasswordSuccess");
            //}
            //else
            //{
            //    return View(model);
            //}
        }

    }
}
