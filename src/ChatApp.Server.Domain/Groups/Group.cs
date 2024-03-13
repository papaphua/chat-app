using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Chats;
using ChatApp.Server.Domain.Core.Abstractions.Groups;

namespace ChatApp.Server.Domain.Groups;

public sealed class Group
    : IEntity<Guid>, IAdministratedChat<GroupMembership, GroupMessage, GroupRole, GroupAvatar>, IGroupMemberPermission
{
    public ICollection<GroupMembership> Memberships { get; set; } = default!;

    public ICollection<GroupMessage> Messages { get; set; } = default!;

    public ICollection<GroupAvatar> Avatars { get; set; } = default!;

    public ICollection<GroupRole> Roles { get; set; } = default!;
    public Guid Id { get; set; } = Guid.NewGuid();

    public bool AllowSendTextMessages { get; set; } = true;

    public bool AllowSendFiles { get; set; } = true;

    public bool AllowAddUsers { get; set; } = true;

    public bool AllowChangeGroupInfo { get; set; } = true;
}