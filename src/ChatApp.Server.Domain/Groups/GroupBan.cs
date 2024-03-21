using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupBan(Guid groupId, Guid userId)
{
    public Guid GroupId { get; set; } = groupId;

    public Group Group { get; set; } = default!;

    public Guid UserId { get; set; } = userId;

    public User User { get; set; } = default!;
}