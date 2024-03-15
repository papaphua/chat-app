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
    
    public static readonly Error AddError = Error.NotFound(
        $"{nameof(ContactAvatar)}.{nameof(AddError)}",
        "Could not add avatar, try again later.");

    public static readonly Error UpdateError = Error.NotFound(
        $"{nameof(ContactAvatar)}.{nameof(UpdateError)}",
        "Could not update avatar, try again later.");

    public static readonly Error RemoveError = Error.NotFound(
        $"{nameof(ContactAvatar)}.{nameof(RemoveError)}",
        "Could not remove avatar, try again later.");
}