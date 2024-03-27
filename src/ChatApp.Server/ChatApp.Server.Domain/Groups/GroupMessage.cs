using System.ComponentModel.DataAnnotations;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupMessage(Guid groupId, Guid senderId)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid GroupId { get; set; } = groupId;

    public Group Group { get; set; } = default!;

    public Guid SenderId { get; set; } = senderId;

    public User Sender { get; set; } = default!;

    [MaxLength(4096)] public string? Content { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public ICollection<GroupAttachment> Attachments { get; set; } = default!;

    public ICollection<GroupReaction> Reactions { get; set; } = default!;

    public ICollection<GroupDeletion> Deletions { get; set; } = default!;
}