using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagement.Models.CustomValidators
{
    public class EmailDomainValidator : ValidationAttribute
    {
        public string AllowedDomain { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null){
                string[] strings = value.ToString().Split('@');
                if (strings.Length > 1 && strings[1].ToUpper() == AllowedDomain.ToUpper())
                {
                    return null;
                }
                return new ValidationResult(
                    $"Domain must be {AllowedDomain}",//this can't be overridden
                                                      //ErrorMessage,//this can be overridden in model
                new[] { validationContext.MemberName });
            }
            return null;
        }
    }
}
