using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Chats;
using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupAttachment(Guid messageId, Guid resourceId)
    : IEntity, IAttachment<GroupMessage>
{
    public Guid MessageId { get; set; } = messageId;

    public GroupMessage Message { get; set; } = default!;

    public Guid ResourceId { get; set; } = resourceId;

    public Resource Resource { get; set; } = default!;
}