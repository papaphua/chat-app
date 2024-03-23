using System.ComponentModel.DataAnnotations;
using ChatApp.Server.Domain.Core.Groups;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupRole(Guid groupId, string name) : IGroupRights
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(32)] public string Name { get; set; } = name;

    public Guid GroupId { get; set; } = groupId;

    public Group Group { get; set; } = default!;

    public bool AllowChangeGroupInfo { get; set; }

    public bool AllowDeleteMessage { get; set; }
    
    public bool AllowApproveJoinRequests { get; set; }

    public bool AllowBanMembers { get; set; }

    public bool AllowInviteUsersViaLink { get; set; }
    
    public bool AllowManageSecurity { get; set; }

    public bool IsOwner { get; set; }
}