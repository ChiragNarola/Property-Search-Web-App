using PropertySearch.Business.Errors;
using System.ComponentModel.DataAnnotations;

namespace ITB.Business.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValidateRequireAttribute : ValidationAttribute
    {
        public string Message { get; set; } = null!;

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null)
                throw new BadRequestError(!string.IsNullOrEmpty(Message) ? Message : $"The field {context.MemberName} is required.");

            return ValidationResult.Success;
        }
    }
}
