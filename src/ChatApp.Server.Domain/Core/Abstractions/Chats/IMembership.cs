using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Core.Abstractions.Chats;

public interface IMembership<TChat>
    where TChat : IEntity
{
    Guid ChatId { get; set; }

    TChat Chat { get; set; }

    Guid MemberId { get; set; }

    User Member { get; set; }
}