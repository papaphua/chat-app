using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupAttachment(Guid messageId, Guid resourceId)
{
    public Guid MessageId { get; set; } = messageId;

    public GroupMessage Message { get; set; } = default!;

    public Guid ResourceId { get; set; } = resourceId;

    public Resource Resource { get; set; } = default!;
}