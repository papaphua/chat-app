using ChatApp.Server.Application.Core;

namespace ChatApp.Server.Application.Shared.Dtos;

public sealed class PriorityAvatarDto
{
    public Guid ResourceId { get; set; }

    public DateTime? Timestamp { get; set; }

    public AvatarPriority Priority { get; set; }
}