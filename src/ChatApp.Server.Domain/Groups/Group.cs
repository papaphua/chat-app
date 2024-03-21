using System.ComponentModel.DataAnnotations;
using ChatApp.Server.Domain.Core.Groups;

namespace ChatApp.Server.Domain.Groups;

public sealed class Group : IGroupPermissions, IGroupPrivacy
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(32)] public string Name { get; set; } = default!;

    [MaxLength(128)] public string? Info { get; set; }

    public ICollection<GroupMembership> Memberships { get; set; } = default!;

    public ICollection<GroupMessage> Messages { get; set; } = default!;

    public ICollection<GroupAvatar> Avatars { get; set; } = default!;

    public ICollection<GroupRole> Roles { get; set; } = default!;

    public bool AllowSendTextMessages { get; set; } = true;

    public bool AllowSendFiles { get; set; } = true;

    public bool AllowReactions { get; set; } = true;

    public bool AllowAddUsers { get; set; } = true;

    public bool IsPublic { get; set; } = true;

    public bool IsHidden { get; set; } = false;
}