using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Contacts.Errors;

public static class ContactAvatarErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(ContactAvatar)}.{nameof(NotFound)}",
        "Contact avatar not found.");

    public static readonly Error Invalid = Error.Validation(
        $"{nameof(ContactAvatar)}.{nameof(Invalid)}",
        "Invalid contact avatar file type.");

    public static readonly Error CreateError = Error.Internal(
        $"{nameof(ContactAvatar)}.{nameof(CreateError)}",
        "Could not create contact avatar, try again later.");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(ContactAvatar)}.{nameof(RemoveError)}",
        "Could not remove contact avatar, try again later.");
}