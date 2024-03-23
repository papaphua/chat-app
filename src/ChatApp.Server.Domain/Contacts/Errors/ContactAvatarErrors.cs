using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Contacts.Errors;

public static class ContactAvatarErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(ContactAvatar)}.{nameof(NotFound)}",
        "Contact avatar not found.");

    public static readonly Error Invalid = Error.Validation(
        $"{nameof(ContactAvatar)}.{nameof(Invalid)}",
        "Invalid avatar file type.");

    public static readonly Error SetError = Error.NotFound(
        $"{nameof(ContactAvatar)}.{nameof(SetError)}",
        "Could not set avatar, try again later.");

    public static readonly Error RemoveError = Error.NotFound(
        $"{nameof(ContactAvatar)}.{nameof(RemoveError)}",
        "Could not remove avatar, try again later.");
}