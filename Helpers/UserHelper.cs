using Hikaya.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hikaya.Helpers
{
    public class UserHelper
    {
        public static string GetCurrentUser()
        {
            return System.Web.HttpContext.Current.User.Identity.GetUserId();
        }
        public static string GetUserName(string id)
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
            return user.UserName;
        }
    }
}