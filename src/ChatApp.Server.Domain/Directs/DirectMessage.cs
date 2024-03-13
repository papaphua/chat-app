using System.ComponentModel.DataAnnotations;
using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Chats;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Directs;

public sealed class DirectMessage(Guid chatId, Guid senderId)
    : IEntity<Guid>, IMessage<Direct, DirectAttachment, DirectReaction>
{
    public ICollection<DirectDeletion> Deletions { get; set; } = default!;
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ChatId { get; set; } = chatId;

    public Direct Chat { get; set; } = default!;

    public Guid SenderId { get; set; } = senderId;

    public User Sender { get; set; } = default!;

    [MaxLength(4096)]
    public string? Content { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public ICollection<DirectAttachment> Attachments { get; set; } = default!;

    public ICollection<DirectReaction> Reactions { get; set; } = default!;
}