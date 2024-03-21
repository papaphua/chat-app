using ChatApp.Server.Domain.Core.Groups;

namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class GroupRoleDto : IGroupRights
{
    public Guid Id { get; set; }
    
    public bool AllowChangeGroupInfo { get; set; }
    
    public bool AllowDeleteMessage { get; set; }
    
    public bool AllowBanMembers { get; set; }
    
    public bool AllowInviteUsersViaLink { get; set; }
    
    public bool AllowManagePermissions { get; set; }
    
    public bool AllowManagePrivacy { get; set; }
    
    public bool AllowManageRoles { get; set; }
}