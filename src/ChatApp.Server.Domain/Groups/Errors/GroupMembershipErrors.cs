using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupMembershipErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(GroupMembership)}.{nameof(NotFound)}",
        "You are not a member.");
    
    public static readonly Error AlreadyMember = Error.Conflict(
        $"{nameof(GroupMembership)}.{nameof(AlreadyMember)}",
        "You are already member.");
    
    public static readonly Error JoinError = Error.Internal(
        $"{nameof(GroupMembership)}.{nameof(JoinError)}",
        "Could not join group, try again later.");
}