using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Web.Models.Account;

namespace Weblog.Web.Areas.Management.Models
{
    public class UserSettingsModel
    {
        [DisplayName("Anzeigename")]
        public string DisplayName { get; set; }

        [DisplayName("E-Mail")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Die E-Mail-Adresse ist keine gültige E-Mail-Adresse")]
        public string Email { get; set; }


        [DisplayName("Altes Passwort")]
        public string OldPassword { get; set; }

        [DisplayName("Neues Passwort")]
        public string Password { get; set; }

        [DisplayName("Neues Passwort wiederholen")]
        public string RepeatPassword { get; set; }

        public UserSettingsModel(UserModel model){
            this.DisplayName = model.DisplayName;
            this.Email = model.Email;
        }

        public UserSettingsModel()
        {
        }

    }
}
