using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Directs.Errors;

public static class DirectReactionErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(DirectReaction)}.{nameof(NotFound)}",
        "Direct reaction not found.");

    public static readonly Error AlreadyExist = Error.Conflict(
        $"{nameof(DirectReaction)}.{nameof(AlreadyExist)}",
        "Direct reaction already exist.");

    public static readonly Error CreateError = Error.Internal(
        $"{nameof(DirectReaction)}.{nameof(CreateError)}",
        "Could not create direct reaction, try again later.");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(DirectReaction)}.{nameof(RemoveError)}",
        "Could not remove direct reaction, try again later.");
}