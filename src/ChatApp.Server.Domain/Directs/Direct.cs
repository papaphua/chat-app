using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Chats;

namespace ChatApp.Server.Domain.Directs;

public sealed class Direct
    : IEntity<Guid>, IChat<DirectMembership, DirectMessage>
{
    public ICollection<DirectMembership> Memberships { get; set; } = default!;

    public ICollection<DirectMessage> Messages { get; set; } = default!;
    public Guid Id { get; set; } = Guid.NewGuid();
}