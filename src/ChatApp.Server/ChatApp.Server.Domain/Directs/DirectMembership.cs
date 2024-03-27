using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Directs;

public sealed class DirectMembership(Guid directId, Guid memberId)
{
    public Guid DirectId { get; set; } = directId;

    public Direct Direct { get; set; } = default!;

    public Guid MemberId { get; set; } = memberId;

    public User Member { get; set; } = default!;

    public bool IsChatSelfDeleted { get; set; }

    public DateTime? ChatSelfDeletedTimestamp { get; set; }
}