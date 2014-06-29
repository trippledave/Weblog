using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weblog.Core.Helpers;

namespace Weblog.Web.Models.Account
{
    public class LoginModel
    {
        [DisplayName("Benutzername")]
        [Required(ErrorMessage = "Der Benutzername muss angegeben werden")]
        [RegularExpression(@InputFilterHelper.FilterSpecialCharsRegex, ErrorMessage = "Der Benutzername enthält Zeichen, die nicht eingegeben werden dürfen.")]
        public string UserName { get; set; }

        [DisplayName("Passwort")]
        [Required(ErrorMessage = "Das Passwort muss angegeben werden")]
        [RegularExpression(@InputFilterHelper.FilterHtmlTagsRegex, ErrorMessage = "Das Passwort enthält Zeichen, die nicht eingegeben werden dürfen.")]
        public string Password { get; set; }

        [DisplayName("Angemeldet bleiben")]
        public bool RememberMe { get; set; }
    }
}