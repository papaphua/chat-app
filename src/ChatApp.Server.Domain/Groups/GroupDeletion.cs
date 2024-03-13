using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupDeletion(Guid messageId, Guid userId)
    : IEntity, IDeletion<GroupMessage>
{
    public Guid MessageId { get; set; } = messageId;

    public GroupMessage Message { get; set; } = default!;

    public Guid UserId { get; set; } = userId;

    public User User { get; set; } = default!;
}