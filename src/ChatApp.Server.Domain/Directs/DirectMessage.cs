using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Chats;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Directs;

public sealed class DirectMessage(Guid chatId, Guid senderId, string? content = default)
    : IEntity<Guid>, IMessage<Direct, DirectAttachment, DirectReaction>
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ChatId { get; set; } = chatId;

    public Direct Chat { get; set; } = default!;

    public Guid SenderId { get; set; } = senderId;

    public User Sender { get; set; } = default!;

    public string? Content { get; set; } = content;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public ICollection<DirectAttachment> Attachments { get; set; } = default!;

    public ICollection<DirectReaction> Reactions { get; set; } = default!;

    public ICollection<DirectDeletion> Deletions { get; set; } = default!;
}