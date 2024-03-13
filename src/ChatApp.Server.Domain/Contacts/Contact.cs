using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Contacts;

public sealed class Contact(Guid ownerId, Guid userId, string? firstName = default, string? lastName = default)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OwnerId { get; set; } = ownerId;

    public User Owner { get; set; } = default!;

    public Guid UserId { get; set; } = userId;

    public User User { get; set; } = default!;

    public string? FirstName { get; set; } = firstName;

    public string? LastName { get; set; } = lastName;

    public Guid? AvatarId { get; set; }

    public ContactAvatar? Avatar { get; set; }
}