using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public static class ValidationHelpers
    {
        public static  void ModelValidations(object? obj)
        {
            //Model Validations
            ValidationContext validationContext = new ValidationContext(obj);

            //To store list of validation results we create a validation List
            List<ValidationResult> results = new List<ValidationResult>();

            // atleast one validation error is found it returns false
            bool isValid = Validator.TryValidateObject(obj, validationContext, results, true);

            if (isValid == false)
                throw new ArgumentException(results.FirstOrDefault()?.ErrorMessage);
        }
    }
}
