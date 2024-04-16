using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupReactionErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(GroupReaction)}.{nameof(NotFound)}",
        "Group reaction not found.");

    public static readonly Error AlreadyExist = Error.Conflict(
        $"{nameof(GroupReaction)}.{nameof(AlreadyExist)}",
        "Group reaction already exist.");

    public static readonly Error CreateError = Error.Internal(
        $"{nameof(GroupReaction)}.{nameof(CreateError)}",
        "Could not create group reaction, try again later.");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(GroupReaction)}.{nameof(RemoveError)}",
        "Could not remove group reaction, try again later.");
}