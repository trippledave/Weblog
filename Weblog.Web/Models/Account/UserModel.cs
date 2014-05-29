using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Account
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public UserModel() { }

        public UserModel(User user)
        {
            this.Email = user.EmailLowercase;
            this.UserName = user.UserName;
        }
    }
}