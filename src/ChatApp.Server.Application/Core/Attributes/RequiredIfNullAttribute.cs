using System.ComponentModel.DataAnnotations;

namespace ChatApp.Server.Application.Core.Attributes;

public class RequiredIfNullAttribute(string nullPropertyName) : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var otherPropertyInfo = validationContext.ObjectType.GetProperty(nullPropertyName);

        if (otherPropertyInfo == null) return new ValidationResult($"Property {nullPropertyName} not found");

        var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

        if (otherPropertyValue == null && value == null)
            return new ValidationResult(
                $"Either {validationContext.MemberName} or {nullPropertyName} must have a value.");

        return ValidationResult.Success!;
    }
}