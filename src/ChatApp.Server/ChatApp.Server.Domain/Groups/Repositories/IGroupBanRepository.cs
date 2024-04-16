using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupBanRepository : IRepository<GroupBan>
{
    Task<GroupBan?> GetByGroupIdAndMemberIdAsync(Guid groupId, Guid memberId);
}