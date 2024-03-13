using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Chats;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupMessage(Guid chatId, Guid senderId, string? content = default)
    : IEntity<Guid>, IMessage<Group, GroupAttachment, GroupReaction>
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ChatId { get; set; } = chatId;

    public Group Chat { get; set; } = default!;

    public Guid SenderId { get; set; } = senderId;

    public User Sender { get; set; } = default!;

    public string? Content { get; set; } = content;

    public DateTime Timestamp { get; set; }  = DateTime.UtcNow;

    public ICollection<GroupAttachment> Attachments { get; set; } = default!;

    public ICollection<GroupReaction> Reactions { get; set; } = default!;
    
    public ICollection<GroupDeletion> Deletions { get; set; } = default!;
}