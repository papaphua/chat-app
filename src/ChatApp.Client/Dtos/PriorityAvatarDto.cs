using ChatApp.Client.Enums;

namespace ChatApp.Client.Dtos;

public sealed class PriorityAvatarDto
{
    public Guid ResourceId { get; set; }

    public DateTime? Timestamp { get; set; }

    public AvatarPriority Priority { get; set; }
}