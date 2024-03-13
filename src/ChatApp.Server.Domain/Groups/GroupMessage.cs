using System.ComponentModel.DataAnnotations;
using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Chats;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupMessage(Guid chatId, Guid senderId)
    : IEntity<Guid>, IMessage<Group, GroupAttachment, GroupReaction>
{
    public ICollection<GroupDeletion> Deletions { get; set; } = default!;
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ChatId { get; set; } = chatId;

    public Group Chat { get; set; } = default!;

    public Guid SenderId { get; set; } = senderId;

    public User Sender { get; set; } = default!;

    [MaxLength(4096)]
    public string? Content { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public ICollection<GroupAttachment> Attachments { get; set; } = default!;

    public ICollection<GroupReaction> Reactions { get; set; } = default!;
}