﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;
using Weblog.Core.Helpers;

namespace Weblog.Web.Models.Account
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsLockedByAdmin { get; set; }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name. It may be null.
        /// </value>
        [RegularExpression(@InputFilterHelper.FilterSpecialCharsRegex, ErrorMessage = "Der Name enthält Zeichen, die nicht eingegeben werden dürfen.")]
        public string DisplayName { get; set; }

        public UserModel() { }

        public UserModel(User user)
        {
            this.Email = user.EmailLowercase;
            this.UserName = user.UserName;
            this.IsLockedByAdmin = user.IsLockedByAdmin;
            this.DisplayName = user.DisplayName;
        }
    }
}