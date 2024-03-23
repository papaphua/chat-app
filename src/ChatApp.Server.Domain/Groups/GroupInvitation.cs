using System.ComponentModel.DataAnnotations;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupInvitation(Guid groupId, Guid creatorId)
{
    public Guid GroupId { get; set; } = groupId;

    public Group Group { get; set; } = default!;

    public Guid CreatorId { get; set; } = creatorId;

    public User Creator { get; set; } = default!;
    
    [MaxLength(128)]
    public string Link { get; set; } = default!;
    
    public DateTime Timestamp { get; set; } = DateTime.Now;
}