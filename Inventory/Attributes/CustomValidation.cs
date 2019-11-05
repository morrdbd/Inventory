using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOMoRR.Attributes
{
    public class InValuesAttribute : ValidationAttribute
    {
        public string Values { get; set; }

        public override bool IsValid(object value)
        {
            if (!string.IsNullOrWhiteSpace(Values) && !string.IsNullOrWhiteSpace((value ?? "").ToString()))
            {
                return Values.Split('|').Contains(value);
            }
            return true;
        }
    }

    public class RequiredIfAttribute : ValidationAttribute
    {
        public string Field { get; set; }
        public object Value { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Field != null && Value != null)
            {
                var obj = validationContext.ObjectInstance;
                var propValue = obj.GetType().GetProperty(Field).GetValue(obj);

                if (Value.Equals(propValue))
                {
                    if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
                    {
                        string errorMsg = string.Format("The field {0} is required if the value of {1} is {2}",
                            validationContext.DisplayName, Field, Value);
                        return new ValidationResult(errorMsg);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }

    public class RequiredIfOtherInAttribute : ValidationAttribute
    {
        public string Field { get; set; }
        public string Values { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Field != null && Values != null)
            {
                var obj = validationContext.ObjectInstance;
                var propValue = obj.GetType().GetProperty(Field).GetValue(obj);

                if (Values.Split('|').Contains(propValue))
                {
                    if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
                    {
                        string errorMsg = string.Format("The field {0} is required if the value of {1} is in ({2})",
                            validationContext.DisplayName, Field, Values);
                        return new ValidationResult(errorMsg);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}