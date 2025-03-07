using System.ComponentModel.DataAnnotations;

namespace simple_recip_application.Data.ApplicationCore.ValidationAttributes;

public class RequiredImageAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is byte[] byteArray && byteArray.Length > 0)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessageString, [validationContext.MemberName!]);
    }
}
