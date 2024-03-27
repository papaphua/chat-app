namespace ChatApp.Server.Domain.Core.Abstractions.Errors;

public enum ErrorType
{
    _ = 0,
    Validation = 400,
    NotFound = 404,
    Conflict = 409,
    Internal = 500
}