using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupMessage
    : IEntity<Guid>, IMessage<Group, GroupAttachment, GroupReaction>
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ChatId { get; set; }

    public Group Chat { get; set; }

    public Guid SenderId { get; set; }

    public User Sender { get; set; }

    public string? Content { get; set; }

    public DateTime Timestamp { get; set; }

    public ICollection<GroupAttachment> Attachments { get; set; }

    public ICollection<GroupReaction> Reactions { get; set; }
}