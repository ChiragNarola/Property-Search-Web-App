using PropertySearch.Business.Errors;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PropertySearch.Business.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValidateListRequireAttribute : ValidationAttribute
    {
        public string Message { get; set; } = null!;

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null || !(value is IEnumerable seq && seq.GetEnumerator().MoveNext()))
                throw new BadRequestError(!string.IsNullOrEmpty(Message) ? Message : $"The field {context.MemberName} is required.");

            return ValidationResult.Success;
        }
    }
}
