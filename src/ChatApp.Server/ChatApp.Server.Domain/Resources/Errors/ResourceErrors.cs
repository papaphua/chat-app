using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Resources.Errors;

public static class ResourceErrors
{
    public static readonly Error NotFound = Error.NotFound(
        $"{nameof(Resource)}.{nameof(NotFound)}",
        "Resource not found.");
}