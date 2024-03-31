using ChatApp.Server.Domain.Core.Abstractions.Errors;
using ChatApp.Server.Domain.Core.Extensions;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Domain.Users.Errors;

public static class UserErrors
{
    public static readonly Error EmailNotFound = Error.NotFound(
        $"{nameof(User)}.{nameof(EmailNotFound)}",
        "User has no email.");

    public static readonly Error PhoneNotFound = Error.NotFound(
        $"{nameof(User)}.{nameof(PhoneNotFound)}",
        "User has no phone number.");

    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(User)}.{nameof(NotFound)}",
        "User not found.");

    public static readonly Error UpdateError = Error.Internal(
        $"{nameof(User)}.{nameof(UpdateError)}",
        "Could not update user, try again later.");

    public static Error IdentityError(IEnumerable<IdentityError> errors)
    {
        return Error.Validation(
            $"{nameof(User)}.{nameof(IdentityError)}",
            errors.ConvertToString());
    }
}