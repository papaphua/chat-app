using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Domain.Users;

public sealed class UserAvatar(Guid userId, Guid resourceId)
{
    public Guid UserId { get; set; } = userId;

    public User User { get; set; } = default!;

    public Guid ResourceId { get; set; } = resourceId;

    public Resource Resource { get; set; } = default!;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}