using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Directs.Errors;

public static class DirectMessageErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(DirectMessage)}.{nameof(NotFound)}",
        "Direct message not found");

    public static readonly Error CreateError = Error.Internal(
        $"{nameof(DirectMessage)}.{nameof(CreateError)}",
        "Could not create direct message, try again later.");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(DirectMessage)}.{nameof(RemoveError)}",
        "Could not remove direct message, try again later.");
}