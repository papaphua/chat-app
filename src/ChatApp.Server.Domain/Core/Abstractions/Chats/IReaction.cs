using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Core.Abstractions.Chats;

public interface IReaction<TMessage>
    where TMessage : IEntity
{
    Guid MessageId { get; set; }

    TMessage Message { get; set; }

    Guid UserId { get; set; }

    User User { get; set; }

    ReactionType Type { get; set; }
}