using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Groups.Errors;

public static class GroupRequestErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(GroupRequest)}.{nameof(NotFound)}",
        "Request not found.");

    public static readonly Error AddError = Error.Internal(
        $"{nameof(GroupRequest)}.{nameof(AddError)}",
        "Could not add request, try again later.");
    
    public static readonly Error ApproveError = Error.Internal(
        $"{nameof(GroupRequest)}.{nameof(ApproveError)}",
        "Could not approve request, try again later.");

    public static readonly Error AlreadyExist = Error.Conflict(
        $"{nameof(GroupRequest)}.{nameof(AlreadyExist)}",
        "You already requested to join this group.");
}