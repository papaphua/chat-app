using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Domain.Core.Abstractions.Chats;

public interface IChatAvatar<TChat>
    where TChat : IEntity
{
    Guid ChatId { get; set; }

    TChat Chat { get; set; }

    Guid ResourceId { get; set; }

    Resource Resource { get; set; }
    
    DateTime Timestamp { get; set; }
}