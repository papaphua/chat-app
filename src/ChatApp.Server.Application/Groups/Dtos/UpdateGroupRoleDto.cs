using ChatApp.Server.Domain.Core.Groups;

namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class UpdateGroupRoleDto : IGroupRights
{
    public bool AllowChangeGroupInfo { get; set; }
    
    public bool AllowDeleteMessage { get; set; }
    
    public bool AllowApproveJoinRequests { get; set; }

    public bool AllowBanMembers { get; set; }
    
    public bool AllowInviteUsersViaLink { get; set; }
    
    public bool AllowManageSecurity { get; set; }
}