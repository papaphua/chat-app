using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupDeletion(Guid messageId, Guid userId)
{
    public Guid MessageId { get; set; } = messageId;

    public GroupMessage Message { get; set; } = default!;

    public Guid UserId { get; set; } = userId;

    public User User { get; set; } = default!;
}