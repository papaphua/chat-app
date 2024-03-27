using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupMembership(Guid groupId, Guid memberId)
{
    public Guid GroupId { get; set; } = groupId;

    public Group Group { get; set; } = default!;

    public Guid MemberId { get; set; } = memberId;

    public User Member { get; set; } = default!;

    public Guid? RoleId { get; set; }

    public GroupRole? Role { get; set; }
    
    public DateTime? RoleTimestamp { get; set; }
}