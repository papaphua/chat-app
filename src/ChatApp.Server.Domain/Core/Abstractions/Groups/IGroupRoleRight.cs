namespace ChatApp.Server.Domain.Core.Abstractions.Groups;

public interface IGroupRoleRight
{
    bool AllowChangeGroupInfo { get; set; }
    
    bool AllowDeleteMessages { get; set; }
    
    bool AllowBanUsers { get; set; }
    
    bool AllowInviteUsersViaLink { get; set; }
    
    bool AllowAddNewAdmins { get; set; }
}