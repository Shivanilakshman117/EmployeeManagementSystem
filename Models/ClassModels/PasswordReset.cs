using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models.ClassModels
{
    public class PasswordReset
    {

        [Display(Name = "Reset Code")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string ResetCode { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password should be atleast 8 characters in length")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Entered Passwords don't match")]

        public string ConfirmPassword { get; set; }

      
    }


}