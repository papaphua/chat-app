using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Groups;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Domain.Users;

public sealed class User : IdentityUser<Guid>, IEntity<Guid>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Bio { get; set; }

    public ICollection<UserAvatar> Avatars { get; set; } = default!;

    public ICollection<Contact> Contacts { get; set; } = default!;

    public ICollection<DirectMembership> DirectMemberships { get; set; } = default!;

    public ICollection<GroupMembership> GroupMemberships { get; set; } = default!;
}