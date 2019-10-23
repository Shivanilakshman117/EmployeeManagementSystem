using EmployeeManagementSystem.Helper;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.ClassModels;
using System;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;

namespace EmployeeManagementSystem.Utilities
{
    public class DBOperations
    {
        public static string ReturnLoginAttemptresult(UserLogin logger, out Employee user)
        {
            using (TechX_SolutionsDBEntities TE = new TechX_SolutionsDBEntities())
            {
                user = TE.Employees.Where(u => u.Employee_ID == logger.EmployeeID).FirstOrDefault();

                if (user != null)
                {
                    if (string.Compare(PasswordHash.Hasher(logger.Password), user.Password) == 0)
                    {
                        return "Successful";
                    }
                    else
                    {
                        return "Incorrect password!";
                    }
                }
                else
                {
                    return "Invalid Credentials!";
                }
            }
        }
        public static string ReturnSignupAttemptresult(Employee NewUser)
        {
            using (TechX_SolutionsDBEntities TE = new TechX_SolutionsDBEntities())
            {
                try
                {
                    TE.Employees.Add(NewUser);
                    TE.Configuration.ValidateOnSaveEnabled = true;
                    TE.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    ExceptionLog.Logger(dbEx);
                }
            }
            return "Account created successfully. Please login to continue.";
        }

        public static string ReturnForgotPasswordAttemptResult(string EmailID, string EmployeeId)
        {
            using (TechX_SolutionsDBEntities TE = new TechX_SolutionsDBEntities())
            {
                var account = TE.Employees.Where(a => a.Work_Email_ID == EmailID && a.Employee_ID == EmployeeId).FirstOrDefault();
                if (account != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    
                    account.ResetPasswordLink = resetCode;
                    TE.Configuration.ValidateOnSaveEnabled = false;
                    TE.SaveChanges();
                    return "Reset password link has been sent to your email id.";
                }
                else
                {
                    return "Account not found";
                }
            }
        }

        public static string ReturnResetPasswordAttemptResult(string id, HttpCookie aCookie)
        {
            using (TechX_SolutionsDBEntities dc = new TechX_SolutionsDBEntities())
            {
                var user = dc.Employees.Where(a => a.ResetPasswordLink == id).FirstOrDefault();
              
                if (user != null && aCookie.Expires < DateTime.Now)
                {
                    PasswordReset model = new PasswordReset
                    {
                        ResetCode = id
                    };
                    return "Successful";
                }
                else
                {
                    return "Invalid attempt";
                }
            }
        }

        public static string ReturnResetPasswordAttemptResult(PasswordReset model)
        {
            using (TechX_SolutionsDBEntities TE = new TechX_SolutionsDBEntities())
            {
                var user = TE.Employees.Where(u => u.ResetPasswordLink == model.ResetCode).FirstOrDefault();
                try
                {
                    if (user != null)
                    {
                        user.Password = PasswordHash.Hasher(model.Password);
                        user.ResetPasswordLink = "";
                        TE.Configuration.ValidateOnSaveEnabled = false;
                        TE.SaveChanges();
                        return "Password has been updated!";
                    }
                }
                catch (Exception Ex)
                {
                    ExceptionLog.Logger(Ex);
                }
            }
            return "Something went wrong while updating password!";
        }

    }
}