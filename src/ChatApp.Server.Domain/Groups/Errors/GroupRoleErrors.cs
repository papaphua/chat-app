using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupRoleErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(GroupRole)}.{nameof(NotFound)}",
        "Role not found.");
    
    public static readonly Error NotEnoughRights = Error.Validation(
        $"{nameof(GroupRole)}.{nameof(NotEnoughRights)}",
        "Not enough rights to do this.");
}