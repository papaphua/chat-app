using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups;

public sealed class Group
    : IEntity<Guid>, IAdministratedChat<GroupMembership, GroupMessage, GroupRole, GroupAvatar>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public ICollection<GroupMembership> Memberships { get; set; } = default!;

    public ICollection<GroupMessage> Messages { get; set; } = default!;

    public ICollection<GroupAvatar> Avatars { get; set; } = default!;
    
    public ICollection<GroupRole> Roles { get; set; } = default!;
}