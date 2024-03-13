using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupRole(Guid chatId, string name)
    : IEntity<Guid>, IRole<Group, GroupRoleRights>
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ChatId { get; set; } = chatId;

    public Group Chat { get; set; } = default!;

    public string Name { get; set; } = name;

    public ICollection<GroupRoleRights> Rights { get; set; } = default!;
}