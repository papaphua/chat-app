using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupRoleErrors
{
    public static readonly Error NotAllowed = Error.NotFound(
        $"{nameof(GroupRole)}.{nameof(NotAllowed)}",
        "You are not allowed to do this action.");
}