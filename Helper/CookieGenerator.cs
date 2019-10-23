
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.ClassModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

namespace EmployeeManagementSystem.Helper
{
    public class CookieGenerator
    {
        public static HttpCookie LoginCookie(Employee user, UserLogin login)
        {
            var timeout = login.RememberMe ? DateTime.Now.AddMinutes(1) : DateTime.Now.AddMinutes(1);
            string username = user.Name;
            string Isadmin = user.IsAdmin.ToString();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, // Ticket version  
            username, DateTime.Now, timeout, false, Isadmin, FormsAuthentication.FormsCookiePath);
            string encryptedticket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedticket);
            user.Token = encryptedticket;
            cookie.Expires = timeout;
            cookie.HttpOnly = true;
            return cookie;

        }

    }
}