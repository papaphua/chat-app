using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupMembershipErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(GroupMembership)}.{nameof(NotFound)}",
        "Group member not found.");
    
    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(GroupMembership)}.{nameof(RemoveError)}",
        "Could not remove member, try again later.");
    
    public static readonly Error AlreadyExist = Error.Conflict(
        $"{nameof(GroupMembership)}.{nameof(AlreadyExist)}",
        "You are already a member.");
    
    public static readonly Error CreateError = Error.Internal(
        $"{nameof(GroupMembership)}.{nameof(CreateError)}",
        "Could not join, try again later.");
}