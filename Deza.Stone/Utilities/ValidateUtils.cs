using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Deza.Stone.Utilities
{
    public class ValidateUtils
    {
        public static bool TryValidateObject(object obj)
        {
            var validationContext = new ValidationContext(obj, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }

        public static bool TryValidateObject(object obj, List<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(obj, serviceProvider: null, items: null);

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}
