using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupBanErrors
{
    public static readonly Error Banned = Error.Validation(
        $"{nameof(GroupBan)}.{nameof(Banned)}",
        "User is banned from this group.");
}