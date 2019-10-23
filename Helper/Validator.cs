using EmployeeManagementSystem.Models;
using System.Linq;

namespace EmployeeManagementSystem.Helper
{
    public class Validator
    {
        public static bool DoesEmailExist(string emailID)
        {
            using (TechX_SolutionsDBEntities TE = new TechX_SolutionsDBEntities())
            {
                var email = TE.Employees.Where(e => e.Work_Email_ID == emailID).FirstOrDefault();
                return email == null ? false : true;
            };
        }
        public static bool DoesEmployeeIDExist(string EmployeeID)
        {
            using (TechX_SolutionsDBEntities TE = new TechX_SolutionsDBEntities())
            {
                var EmpID = TE.Employees.Where(e => e.Employee_ID == EmployeeID).FirstOrDefault();
                return EmpID == null ? false : true;
            };
        }

    }
}