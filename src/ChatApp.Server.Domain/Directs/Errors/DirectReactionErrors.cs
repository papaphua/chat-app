using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Directs.Errors;

public static class DirectReactionErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(DirectReaction)}.{nameof(NotFound)}",
        "Reaction not found");

    public static readonly Error AlreadyExist = Error.Conflict(
        $"{nameof(DirectReaction)}.{nameof(AlreadyExist)}",
        "Reaction already exist");

    public static readonly Error AddError = Error.Internal(
        $"{nameof(DirectReaction)}.{nameof(AddError)}",
        "Could not add reaction, try again later");
    
    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(DirectReaction)}.{nameof(RemoveError)}",
        "Could not remove reaction, try again later");
}