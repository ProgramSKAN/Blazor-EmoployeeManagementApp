using EmployeeManagement.Models.CustomValidators;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    /*//Model Validations from json body
     * Required
     * Range
     * MinLength
     * MaxLength
     * Compare
     * RegularExpression
     */
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="First Name must be Provided")]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [EmailDomainValidator(AllowedDomain ="GMAIL.COM")]
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; }


        //Get data from multiple table
        public Department Department { get; set; }//these are called Navigation properties.to get data from different table
                       
    }
}
