using System.ComponentModel.DataAnnotations;
using ChatApp.Server.Domain.Core.Groups;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupRole(Guid groupId, string name) : IGroupRights
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(32)] public string Name { get; set; } = name;

    public Guid GroupId { get; set; } = groupId;

    public Group Group { get; set; } = default!;

    public bool AllowChangeGroupInfo { get; set; } = false;
    
    public bool AllowDeleteMessage { get; set; } = false;
    
    public bool AllowBanMembers { get; set; } = false;

    public bool AllowInviteUsersViaLink { get; set; } = false;

    public bool AllowManagePermissions { get; set; } = false;
    
    public bool AllowManagePrivacy { get; set; } = false;
    
    public bool AllowManageRoles { get; set; } = false;
}