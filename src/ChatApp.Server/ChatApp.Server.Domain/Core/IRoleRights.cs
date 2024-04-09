namespace ChatApp.Server.Domain.Core;

public interface IRoleRights
{
    bool AllowChangeGroupInfo { get; set; }

    bool AllowDeleteMessage { get; set; }

    bool AllowInviteUsersViaLink { get; set; }

    bool AllowBanMembers { get; set; }
}