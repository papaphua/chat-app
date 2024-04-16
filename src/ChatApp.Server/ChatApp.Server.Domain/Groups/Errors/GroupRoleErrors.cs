using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupRoleErrors
{
    public static readonly Error NotAllowed = Error.NotFound(
        $"{nameof(GroupRole)}.{nameof(NotAllowed)}",
        "You are not allowed to do this action.");
    
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(GroupRole)}.{nameof(NotFound)}",
        "Role not found.");
    
    public static readonly Error CreateError = Error.Internal(
        $"{nameof(GroupRole)}.{nameof(CreateError)}",
        "Could not create group role, try again later.");
    
    public static readonly Error UpdateError = Error.Internal(
        $"{nameof(GroupRole)}.{nameof(UpdateError)}",
        "Could not update group role, try again later.");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(GroupRole)}.{nameof(RemoveError)}",
        "Could not remove group role, try again later.");
}