using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Chats;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupMembership(Guid chatId, Guid memberId)
    : IEntity, IMembership<Group>
{
    public Guid ChatId { get; set; } = chatId;

    public Group Chat { get; set; } = default!;

    public Guid MemberId { get; set; } = memberId;

    public User Member { get; set; } = default!;
}