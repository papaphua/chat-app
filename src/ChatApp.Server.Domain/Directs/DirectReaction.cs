using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Chats;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Directs;

public sealed class DirectReaction(Guid messageId, Guid userId, ReactionType type)
    : IEntity<Guid>, IReaction<DirectMessage>
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid MessageId { get; set; } = messageId;

    public DirectMessage Message { get; set; } = default!;

    public Guid UserId { get; set; } = userId;

    public User User { get; set; } = default!;

    public ReactionType Type { get; set; } = type;
}