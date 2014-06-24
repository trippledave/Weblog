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
    public class LockUserModel
    {
        public List<UserModel> UserList { get; set; }

        
        public LockUserModel(List<UserModel> userList){
            this.UserList = userList;
        }

        public LockUserModel()
        {
        }

    }
}
