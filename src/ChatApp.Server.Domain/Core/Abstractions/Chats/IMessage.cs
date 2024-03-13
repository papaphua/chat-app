using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Core.Abstractions.Chats;

public interface IMessage<TChat, TAttachment, TReaction>
    where TChat : IEntity
    where TAttachment : IEntity
    where TReaction : IEntity
{
    Guid ChatId { get; set; }

    TChat Chat { get; set; }

    Guid SenderId { get; set; }

    User Sender { get; set; }

    string? Content { get; set; }

    DateTime Timestamp { get; set; }

    ICollection<TAttachment> Attachments { get; set; }

    ICollection<TReaction> Reactions { get; set; }
}