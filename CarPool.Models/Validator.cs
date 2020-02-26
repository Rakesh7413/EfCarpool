using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CarPool.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class NotNullAttribute : ValidationAttribute
    {
        // Checks whether string is Null
        public override bool IsValid(object value)
        {
            return string.IsNullOrEmpty(value.ToString()) ? false : true;
        }
    }

    public class PhoneNumberAttribute : NotNullAttribute
    {
        // Returns true if it is a valid phone number
        public override bool IsValid(object value)
        {
            return new Regex(@"^(\+[1-9][0-9])?[6-9](0-9){9}").IsMatch(value.ToString());
        }
    }

    public class PresentOrFutureDateAttribute : NotNullAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToDateTime(value) >= DateTime.Today)
            {
                return true;
            }
            return false;
        }
    }

    public class GenericValidator
    {
        public static bool Validate(object model,out List<string> errors)
        {
            errors = new List<string>();
            foreach (var propertyInfo in model.GetType().GetProperties())
            {
                foreach (var attribute in propertyInfo.GetCustomAttributes(true))
                {
                    if (attribute is NotNullAttribute notNullAttribute)
                    {
                        if (!notNullAttribute.IsValid(propertyInfo.GetValue(model)))
                        {
                            errors.Add(notNullAttribute.ErrorMessage);
                            break;
                        }
                    }
                }
            }            
            return errors.Count==0;
        }
    }
}
