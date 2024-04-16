using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupMessageErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(GroupMessage)}.{nameof(NotFound)}",
        "Group message not found");

    public static readonly Error CreateError = Error.Internal(
        $"{nameof(GroupMessage)}.{nameof(CreateError)}",
        "Could not create group message, try again later.");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(GroupMessage)}.{nameof(RemoveError)}",
        "Could not remove group message, try again later.");
}