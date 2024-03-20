using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Directs.Errors;

public static class DirectMessageErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(DirectMessage)}.{nameof(NotFound)}",
        "Message not found");
    
    public static readonly Error AddError = Error.Internal(
        $"{nameof(DirectMessage)}.{nameof(AddError)}",
        "Could not add message, try again later");
    
    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(DirectMessage)}.{nameof(RemoveError)}",
        "Could not remove message, try again later");
}