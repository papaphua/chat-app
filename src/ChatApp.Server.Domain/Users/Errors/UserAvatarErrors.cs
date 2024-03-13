using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Users.Errors;

public static class UserAvatarErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(UserAvatar)}.{nameof(NotFound)}",
        "User avatar not found.");
    
    public static readonly Error Invalid = Error.Validation(
        $"{nameof(UserAvatar)}.{nameof(Invalid)}",
        "Invalid avatar file type.");
    
    public static readonly Error AddError = Error.Internal(
        $"{nameof(UserAvatar)}.{nameof(AddError)}",
        "Could not add user avatar, try again later.");
    
    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(UserAvatar)}.{nameof(RemoveError)}",
        "Could not remove user avatar, try again later.");
}