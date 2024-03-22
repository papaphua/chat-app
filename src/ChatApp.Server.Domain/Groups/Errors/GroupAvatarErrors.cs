using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupAvatarErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(GroupAvatar)}.{nameof(NotFound)}",
        "Group avatar not found.");

    public static readonly Error Invalid = Error.Validation(
        $"{nameof(GroupAvatar)}.{nameof(Invalid)}",
        "Invalid avatar file type.");

    public static readonly Error AddError = Error.Internal(
        $"{nameof(GroupAvatar)}.{nameof(AddError)}",
        "Could not add group avatar, try again later.");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(GroupAvatar)}.{nameof(RemoveError)}",
        "Could not remove group avatar, try again later.");
}