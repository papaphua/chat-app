using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Domain.Users;

public sealed class User : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Bio { get; set; }
}