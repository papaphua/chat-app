using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Directs;

public sealed class DirectDeletion(Guid messageId, Guid userId)
{
    public Guid MessageId { get; set; } = messageId;

    public DirectMessage Message { get; set; } = default!;

    public Guid UserId { get; set; } = userId;

    public User User { get; set; } = default!;
}