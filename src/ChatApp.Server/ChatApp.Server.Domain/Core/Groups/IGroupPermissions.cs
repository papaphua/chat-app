namespace ChatApp.Server.Domain.Core.Groups;

public interface IGroupPermissions
{
    bool AllowSendTextMessages { get; set; }

    bool AllowSendFiles { get; set; }

    bool AllowReactions { get; set; }

    bool AllowAddMembers { get; set; }
}