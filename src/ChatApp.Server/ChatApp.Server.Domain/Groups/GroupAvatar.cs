using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupAvatar(Guid groupId, Guid resourceId)
{
    public Guid GroupId { get; set; } = groupId;

    public Group Group { get; set; } = default!;

    public Guid ResourceId { get; set; } = resourceId;

    public Resource Resource { get; set; } = default!;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}