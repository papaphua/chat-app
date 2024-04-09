using System.ComponentModel.DataAnnotations;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups;

public sealed class Group(string name, Guid ownerId)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(32)] public string Name { get; set; } = name;

    [MaxLength(128)] public string? Info { get; set; }

    public Guid OwnerId { get; set; } = ownerId;

    public User Owner { get; set; } = default!;
    
    public ICollection<GroupMembership> Memberships { get; set; } = default!;

    public ICollection<GroupMessage> Messages { get; set; } = default!;

    public ICollection<GroupAvatar> Avatars { get; set; } = default!;

    public ICollection<GroupRole> Roles { get; set; } = default!;

    public ICollection<GroupBan> Bans { get; set; } = default!;

    public bool AllowSendTextMessages { get; set; } = true;

    public bool AllowSendFiles { get; set; } = true;

    public bool AllowReactions { get; set; } = true;

    public bool AllowAddMembers { get; set; } = true;

    public bool IsPublic { get; set; } = true;
}