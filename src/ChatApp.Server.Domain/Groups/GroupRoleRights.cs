using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Groups.Enum;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupRoleRights(Guid roleId, GroupRightType type)
    : IEntity, IRoleRights<GroupRole, GroupRightType>
{
    public Guid RoleId { get; set; } = roleId;

    public GroupRole Role { get; set; } = default!;

    public GroupRightType Type { get; set; } = type;
}