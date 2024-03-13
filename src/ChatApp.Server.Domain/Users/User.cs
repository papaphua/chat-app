using ChatApp.Server.Domain.Core.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Domain.Users;

public sealed class User : IdentityUser<Guid>, IEntity<Guid>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Bio { get; set; }
}