using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weblog.Web.Models.Account
{
    public class RegisterModel
    {
        [DisplayName("Benutzername")]
        [Required(ErrorMessage = "Der Benutzername muss angegeben werden")]
        public string UserName { get; set; }

        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "Die E-Mail muss angegeben werden")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Die E-Mail-Adresse ist keine gültige E-Mail-Adresse")]
        public string Email { get; set; }

        [DisplayName("Passwort")]
        [Required(ErrorMessage = "Das Passwort muss angegeben werden")]
        public string Password { get; set; }
    }
}