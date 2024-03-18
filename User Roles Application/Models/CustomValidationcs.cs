using System.ComponentModel.DataAnnotations;

public class AllowNullAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // If the value is a string and not null or whitespace, consider it as valid
        if (value is string str && !string.IsNullOrWhiteSpace(str))
        {
            return ValidationResult.Success;
        }

        // Otherwise, return failure with the provided error message
        return new ValidationResult(ErrorMessage);
    }
}