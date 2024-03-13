using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Domain.Core.Abstractions;

public interface IChatAvatar<TChat>
    where TChat : IEntity
{
    Guid ChatId { get; set; }

    TChat Chat { get; set; }

    Guid ResourceId { get; set; }

    Resource Resource { get; set; }
}