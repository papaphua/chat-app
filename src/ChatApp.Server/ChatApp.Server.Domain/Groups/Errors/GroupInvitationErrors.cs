using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupInvitationErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(GroupInvitation)}.{nameof(NotFound)}",
        "Invitation not found.");
    
    public static readonly Error CreateError = Error.Internal(
        $"{nameof(GroupInvitation)}.{nameof(CreateError)}",
        "Could not create invitation, try again later.");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(GroupInvitation)}.{nameof(RemoveError)}",
        "Could not remove invitation, try again later.");
}