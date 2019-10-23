using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models.ClassModels
{

    public class UserLogin
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "*Please enter Employee ID")]
        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "*Please enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }



}