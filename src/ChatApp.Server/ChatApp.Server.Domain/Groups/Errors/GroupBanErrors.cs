using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupBanErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(GroupBan)}.{nameof(NotFound)}",
        "User is not banned.");
    
    public static readonly Error Banned = Error.Validation(
        $"{nameof(GroupBan)}.{nameof(Banned)}",
        "You are banned from this group.");
    
    public static readonly Error CreateError = Error.Internal(
        $"{nameof(GroupBan)}.{nameof(CreateError)}",
        "Could not ban member, try again later.");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(GroupBan)}.{nameof(RemoveError)}",
        "Could not unban member, try again later.");
}