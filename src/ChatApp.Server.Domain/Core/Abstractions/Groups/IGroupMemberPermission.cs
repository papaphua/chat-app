namespace ChatApp.Server.Domain.Core.Abstractions.Groups;

public interface IGroupMemberPermission
{
    bool AllowSendTextMessages { get; set; }

    bool AllowSendFiles { get; set; }

    bool AllowAddUsers { get; set; }

    bool AllowChangeGroupInfo { get; set; }
}