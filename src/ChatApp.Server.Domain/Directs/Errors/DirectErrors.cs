using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Directs.Errors;

public static class DirectErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(Direct)}.{nameof(NotFound)}",
        "Direct not found.");

    public static readonly Error CreateError = Error.Internal(
        $"{nameof(Direct)}.{nameof(CreateError)}",
        "Could not create direct, try again later.");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(Direct)}.{nameof(RemoveError)}",
        "Could not remove direct, try again later.");
}