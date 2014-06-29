using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Core.Helpers;
using Weblog.Web.Models.Account;

namespace Weblog.Web.Areas.Management.Models
{
    public class UserPasswordModel
    {
        [DisplayName("Altes Passwort")]
        [Required(ErrorMessage = "Das Passwort muss angegeben werden")]
        [RegularExpression(@InputFilterHelper.FilterHtmlTagsRegex, ErrorMessage = "Der Text enthält Zeichen, die nicht eingegeben werden dürfen.")]
        public string OldPassword { get; set; }

        [DisplayName("Neues Passwort")]
        [Required(ErrorMessage = "Das Passwort muss angegeben werden")]
        [RegularExpression(@InputFilterHelper.FilterHtmlTagsRegex, ErrorMessage = "Der Text enthält Zeichen, die nicht eingegeben werden dürfen.")]
        public string Password { get; set; }

        [DisplayName("Neues Passwort wiederholen")]
        [Required(ErrorMessage = "Das Passwort muss angegeben werden")]
        [RegularExpression(@InputFilterHelper.FilterHtmlTagsRegex, ErrorMessage = "Der Text enthält Zeichen, die nicht eingegeben werden dürfen.")]
        public string RepeatPassword { get; set; }
    }
}
