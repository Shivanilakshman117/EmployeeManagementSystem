using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace EmployeeManagementSystem.Helper
{
    public class ComposeEmail
    {
        public static MailMessage PasswordResetMail(string emailID, string code,out SmtpClient messenger)
        {
            var fromEmail = new MailAddress("techxems@gmail.com", "EMS Portal");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Psiog@123";
            string subject = "";
            string body = "";
            subject = "Reset Password";
            body = "Hey,<br/><br/>\nWe heard that you lost your TechX EMS Portal password. Sorry about that! But don’t worry! You can use the following link to reset your password: " +
                    "<br/><br/><a href=";
            
          messenger= new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
          };

            var mail = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            return mail;
        }
    }
}