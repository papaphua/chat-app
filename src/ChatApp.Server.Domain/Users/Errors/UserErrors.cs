using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Users.Errors;

public static class UserErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(User)}.{nameof(NotFound)}",
        "User not found");

    public static readonly Error UpdateError = Error.Internal(
        $"{nameof(User)}.{nameof(UpdateError)}",
        "Could not update user, try again later");
}