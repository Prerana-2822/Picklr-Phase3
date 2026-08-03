using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Picklr.Models
{
    public class AtLeastOneDayAttribute : ValidationAttribute, IClientModelValidator
    {

        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            PicklProgram program =
                (PicklProgram)validationContext.ObjectInstance;

            if (program.Monday ||
                program.Tuesday ||
                program.Wednesday ||
                program.Thursday ||
                program.Friday ||
                program.Saturday ||
                program.Sunday)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(
                "Please select at least one training day.");
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(
                context.Attributes,
                "data-val",
                "true");

            MergeAttribute(
                context.Attributes,
                "data-val-atleastoneday",
                ErrorMessage ??
                "Please select at least one training day.");
        }

        private bool MergeAttribute(
            IDictionary<string, string> attributes,
            string key,
            string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);

            return true;
        }
    }
}