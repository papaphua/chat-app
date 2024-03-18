using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Directs.Errors;

public static class DirectErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(Direct)}.{nameof(NotFound)}",
        "Chat not found");

    public static readonly Error CreateError = Error.Internal(
        $"{nameof(Direct)}.{nameof(CreateError)}",
        "Could not create chat, try again later");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(Direct)}.{nameof(RemoveError)}",
        "Could not remove chat, try again later");
}