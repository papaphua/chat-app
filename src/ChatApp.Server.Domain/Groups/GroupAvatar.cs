using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Chats;
using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupAvatar(Guid chatId, Guid resourceId)
    : IEntity, IChatAvatar<Group>
{
    public Guid ChatId { get; set; } = chatId;

    public Group Chat { get; set; } = default!;

    public Guid ResourceId { get; set; } = resourceId;

    public Resource Resource { get; set; } = default!;
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}