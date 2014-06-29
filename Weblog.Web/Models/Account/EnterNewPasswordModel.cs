using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Core.Helpers;

namespace Weblog.Web.Models.Account
{
    public class EnterNewPasswordModel
    {
        [DisplayName("Neues Passwort")]
        [Required(ErrorMessage = "Das Passwort muss angegeben werden")]
        [RegularExpression(@InputFilterHelper.FilterHtmlTagsRegex, ErrorMessage = "Das Passwort enthält Zeichen, die nicht eingegeben werden dürfen.")]
        public string Password { get; set; }

        [DisplayName("Neues Passwort wiederholen")]
        [Required(ErrorMessage = "Das Passwort muss angegeben werden")]
        [RegularExpression(@InputFilterHelper.FilterHtmlTagsRegex, ErrorMessage = "Das Passwort enthält Zeichen, die nicht eingegeben werden dürfen.")]
        public string RepeatPassword { get; set; }

        [DisplayName("Token")]
        [Required(ErrorMessage = "Der Token muss eingegeben werden")]
        [RegularExpression(@InputFilterHelper.FilterHtmlTagsRegex, ErrorMessage = "Dert Token enthält Zeichen, die nicht eingegeben werden dürfen.")]
        public string Token { get; set; }
    }
}
