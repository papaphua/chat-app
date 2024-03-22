using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public sealed class GroupErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(Group)}.{nameof(NotFound)}",
        "Group not found.");
    
    public static readonly Error CreateError = Error.Internal(
        $"{nameof(Group)}.{nameof(CreateError)}",
        "Could not create group, try again later.");
    
    public static readonly Error RemoveError = Error.Internal(
        $"{nameof(Group)}.{nameof(RemoveError)}",
        "Could not remove group, try again later.");
    
    public static readonly Error UpdateError = Error.Internal(
        $"{nameof(Group)}.{nameof(UpdateError)}",
        "Could not update group, try again later.");
    
    public static readonly Error PrivateGroupError = Error.Validation(
        $"{nameof(Group)}.{nameof(PrivateGroupError)}",
        "This group is private.");
}