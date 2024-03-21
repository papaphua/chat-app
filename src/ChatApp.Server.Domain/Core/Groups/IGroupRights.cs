namespace ChatApp.Server.Domain.Core.Groups;

public interface IGroupRights
{
    bool AllowChangeGroupInfo { get; set; }
    
    bool AllowDeleteMessage { get; set; }
    
    bool AllowBanMembers { get; set; }
    
    bool AllowInviteUsersViaLink { get; set; }
    
    bool AllowManagePermissions { get; set; }
    
    bool AllowManagePrivacy { get; set; }
    
    bool AllowManageRoles { get; set; }
}