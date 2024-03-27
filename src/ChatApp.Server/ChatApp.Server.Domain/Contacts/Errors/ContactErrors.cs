using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Contacts.Errors;

public static class ContactErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(Contact)}.{nameof(NotFound)}",
        "Contact not found.");

    public static readonly Error AlreadyExist = Error.Conflict(
        $"{nameof(Contact)}.{nameof(AlreadyExist)}",
        "Contact already exist.");

    public static readonly Error CreateError = Error.Internal(
        $"{nameof(Contact)}.{nameof(CreateError)}",
        "Could not create contact, try again later.");

    public static readonly Error UpdateError = Error.Internal(
        $"{nameof(Contact)}.{nameof(UpdateError)}",
        "Could not update contact, try again later.");

    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(Contact)}.{nameof(RemoveError)}",
        "Could not remove contact, try again later.");
}