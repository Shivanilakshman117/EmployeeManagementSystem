using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace EmployeeManagementSystem.Models
{
    [MetadataType(typeof(MetaEmployee))]
    public partial class Employee
    {
        public string ConfirmPassword { get; set; }
    }

    public class MetaEmployee
    {
        [Display(Name = "Employee ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        [RegularExpression(@"TX[0-9][0-9][0-9]", ErrorMessage = "ID does not match TechX Solutions Employee_ID ")]
        public string Employee_ID { get; set; }

        [Display(Name = "Full Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string Name { get; set; }
        
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password should be atleast 8 characters in length")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Entered Passwords don't match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Gender")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string Gender { get; set; }
        
        [Display(Name = "Date of Birth")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/YYYY}")]
        public Nullable<System.DateTime> DOB { get; set; }

        [Display(Name = "Mobile")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid Mobile Number")]
        public Nullable<long> Mobile { get; set; }

        [Display(Name = "Department")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string Department { get; set; }

        [Display(Name = "Designation")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string Designation { get; set; }

        [Display(Name = "Work Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@techxsolutions.com", ErrorMessage = "Invalid Work Email ID")]
        public string Work_Email_ID { get; set; }

        [Display(Name = "Blood Group")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string Blood_Type { get; set; }

        [Display(Name = "Date of Joining")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/YYYY}")]
        public Nullable<System.DateTime> DOJ { get; set; }
        public Nullable<System.DateTime> DOQ { get; set; }
        public string ID_Proof_Type { get; set; }
        public string ID_Proof_Number { get; set; }
        public bool IsAdmin { get; set; }
        public string Address { get; set; }
    }
}