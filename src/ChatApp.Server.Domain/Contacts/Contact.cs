using System.ComponentModel.DataAnnotations;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Contacts;

public sealed class Contact(Guid ownerId, Guid partnerId)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OwnerId { get; set; } = ownerId;

    public User Owner { get; set; } = default!;

    public Guid PartnerId { get; set; } = partnerId;

    public User Partner { get; set; } = default!;

    [MaxLength(64)] public string FirstName { get; set; } = default!;

    [MaxLength(64)] public string? LastName { get; set; }

    public Guid? AvatarId { get; set; }

    public ContactAvatar? Avatar { get; set; }
}