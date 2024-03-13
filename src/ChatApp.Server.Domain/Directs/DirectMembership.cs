using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Directs;

public sealed class DirectMembership(Guid chatId, Guid memberId)
    : IEntity, IMembership<Direct>
{
    public Guid ChatId { get; set; } = chatId;

    public Direct Chat { get; set; } = default!;

    public Guid MemberId { get; set; } = memberId;

    public User Member { get; set; } = default!;

    public bool IsChatSelfDeleted { get; set; }
    
    public DateTime? ChatSelfDeletedTimestamp { get; set; }
}