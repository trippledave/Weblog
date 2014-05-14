using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weblog.Web.Models.Account
{
    public class LoginModel
    {
        [DisplayName("Benutzername")]
        [Required(ErrorMessage = "Der Benutzername muss angegeben werden")]
        public string UserName { get; set; }

        [DisplayName("Passwort")]
        [Required(ErrorMessage = "Das Passwort muss angegeben werden")]
        public string Password { get; set; }

        [DisplayName("Angemeldet bleiben")]
        public bool RememberMe { get; set; }
    }
}