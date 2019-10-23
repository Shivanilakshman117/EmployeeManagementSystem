using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Net;
using EmployeeManagementSystem.Helper;
using EmployeeManagementSystem.Utilities;
using EmployeeManagementSystem.Models.ClassModels;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{   
    public class AccountController : Controller
    {
        [HttpGet]
        //Sign Up
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost] //Post Sign Up
        [ValidateAntiForgeryToken]
        public ActionResult Signup(Employee NewUser)
        {
            string message = " ";
            bool status = false;

            if (ModelState.IsValid)
            {
                if (Validator.DoesEmployeeIDExist(NewUser.Employee_ID))
                {
                    ModelState.AddModelError("Employee ID Exists", "The Employee Id seems to be registered to a user. Did you Forget your password?");
                    return View(NewUser);
                }
                if (Validator.DoesEmailExist(NewUser.Work_Email_ID))
                {
                    ModelState.AddModelError("Email Exists", "The Email Id seems to be registered to a user. Did you Forget your password?");
                    return View(NewUser);
                }
                NewUser.Password = PasswordHash.Hasher(NewUser.Password);
                NewUser.ConfirmPassword = PasswordHash.Hasher(NewUser.ConfirmPassword);
                message= DBOperations.ReturnSignupAttemptresult(NewUser);

                if (string.Compare(message, "Account created successfully. Please login to continue.") == 0)
                {   status = true;
                    ViewBag.Status = status;
                    ViewBag.Message = message;
                }
            }
            else
            {
                status = false;
                message = null;
                ViewBag.Status = status;
                ViewBag.Message = message;
            }
            return View();
        }
     

        [HttpGet] //login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]//post login
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login,string returnurl)
        {
            string message = " ";
            message = DBOperations.ReturnLoginAttemptresult(login, out Employee user);
            if (string.Compare(message, "Successful") == 0)
            {
                Response.Cookies.Add(CookieGenerator.LoginCookie(user,login));
                if (Url.IsLocalUrl(""))
                {
                    ViewBag.Message = "Something went wrong!";
                    return Redirect("");
                }
                else
                {
                    return RedirectToAction("Welcome", "Feed");
                }
            }
            else
            {
                ViewBag.Message = message;
            }
            return View();
        }

        [Authorize]
        [HttpPost] //logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
       
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID, string employeeid)
        {
            string message = "";
            using (TechX_SolutionsDBEntities TE = new TechX_SolutionsDBEntities())
            {
                var account = TE.Employees.Where(a => a.Work_Email_ID == EmailID && a.Employee_ID == employeeid).FirstOrDefault();
                if (account != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail("techxems@gmail.com", resetCode, "ResetPassword");
                    account.ResetPasswordLink = resetCode;
                    TE.Configuration.ValidateOnSaveEnabled = false;
                    TE.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        public ActionResult ResetPassword(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }
            HttpCookie aCookie = Request.Cookies["PasswordResetCookie"];
            string result = DBOperations.ReturnResetPasswordAttemptResult(id,aCookie);

            if (result.Equals("successful", StringComparison.InvariantCultureIgnoreCase))
            {
                    PasswordReset model = new PasswordReset
                    {
                        ResetCode = id
                    };
                    return View(model);
            }
           else
           {
           return HttpNotFound();
           }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(PasswordReset model)
        {
            var message = "";
            string result;
            if (ModelState.IsValid)
            {
                result = DBOperations.ReturnResetPasswordAttemptResult(model);
                if (result.Equals("Password has been updated!", StringComparison.InvariantCultureIgnoreCase))
                    message = result;
            }
            else
            {
                message = "Something went wrong! Unable to process your request";
            }
            ViewBag.Message = message;
            return View(model);
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string code, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Account/" + emailFor + "/" + code;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            HttpCookie passwordcookie = new HttpCookie("PasswordResetCookie", link)
            {
                Expires = DateTime.Now.AddMinutes(1),
                HttpOnly = true
            };
            var mail=ComposeEmail.PasswordResetMail(emailID,code,out SmtpClient messenger);
            Response.Cookies.Add(passwordcookie);
            mail.Body+= link + ">Reset Password link</a>";
            messenger.Send(mail);
            messenger.Dispose();
          
        }
    }
}
