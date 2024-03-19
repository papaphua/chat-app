using System.ComponentModel.DataAnnotations;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupRole(Guid groupId)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(32)] public string Name { get; set; } = default!;

    public bool AllowChangeGroupInfo { get; set; } = true;

    public bool AllowDeleteMessages { get; set; } = true;

    public bool AllowBanUsers { get; set; } = true;

    public bool AllowInviteUsersViaLink { get; set; } = true;

    public bool AllowAddNewAdmins { get; set; }

    public Guid GroupId { get; set; } = groupId;

    public Group Group { get; set; } = default!;
}