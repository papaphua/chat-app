using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupBanErrors
{
    public static readonly Error Banned = Error.Validation(
        $"{nameof(Group)}.{nameof(Banned)}",
        "You are banned from this group.");
}