namespace ChatApp.Server.Domain.Core.Groups;

public interface IGroupRights
{
    bool AllowChangeGroupName { get; set; }

    bool AllowDeleteMessage { get; set; }

    bool AllowApproveRequests { get; set; }

    bool AllowInviteUsersViaLink { get; set; }

    bool AllowBanMembers { get; set; }

    bool AllowManagePermissions { get; set; }

    bool AllowManagePrivacy { get; set; }

    // if AllowManageRoles = true, all other = true
    bool AllowManageRoles { get; set; }
}