using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Contacts;

public sealed class Contact(Guid ownerId, Guid partnerId, string? firstName = default, string? lastName = default)
    : IEntity<Guid>
{
    public Guid OwnerId { get; set; } = ownerId;

    public User Owner { get; set; } = default!;

    public Guid PartnerId { get; set; } = partnerId;

    public User Partner { get; set; } = default!;

    public string? FirstName { get; set; } = firstName;

    public string? LastName { get; set; } = lastName;

    public Guid? AvatarId { get; set; }

    public ContactAvatar? Avatar { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}