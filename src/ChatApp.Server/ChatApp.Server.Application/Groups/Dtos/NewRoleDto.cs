using ChatApp.Server.Domain.Core;

namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class NewRoleDto : IRoleRights
{
    public string Name { get; set; }
    
    public bool AllowChangeGroupInfo { get; set; }
    
    public bool AllowDeleteMessage { get; set; }
    
    public bool AllowInviteUsersViaLink { get; set; }
    
    public bool AllowBanMembers { get; set; }
}