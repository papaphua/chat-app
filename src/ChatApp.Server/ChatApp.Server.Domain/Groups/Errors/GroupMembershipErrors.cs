using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupMembershipErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(GroupMembership)}.{nameof(NotFound)}",
        "You are not a member of this group.");
    
    public static readonly Error RemoveError = Error.NotFound(
        $"{nameof(GroupMembership)}.{nameof(RemoveError)}",
        "Could not remove member, try again later.");
}