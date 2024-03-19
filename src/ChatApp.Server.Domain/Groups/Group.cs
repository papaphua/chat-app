namespace ChatApp.Server.Domain.Groups;

public sealed class Group
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public bool AllowSendTextMessages { get; set; } = true;

    public bool AllowSendFiles { get; set; } = true;

    public bool AllowAddUsers { get; set; } = true;

    public bool AllowChangeGroupInfo { get; set; } = true;

    public ICollection<GroupMembership> Memberships { get; set; } = default!;

    public ICollection<GroupMessage> Messages { get; set; } = default!;

    public ICollection<GroupAvatar> Avatars { get; set; } = default!;

    public ICollection<GroupRole> Roles { get; set; } = default!;
}