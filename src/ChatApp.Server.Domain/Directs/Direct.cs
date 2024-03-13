using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Directs;

public sealed class Direct
    : IEntity<Guid>, IChat<DirectMembership, DirectMessage>
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public ICollection<DirectMembership> Memberships { get; set; } = default!;

    public ICollection<DirectMessage> Messages { get; set; } = default!;
}