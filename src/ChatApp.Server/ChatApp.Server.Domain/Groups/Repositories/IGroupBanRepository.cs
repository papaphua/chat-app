using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Shared;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupBanRepository : IRepository<GroupBan>
{
    Task<GroupBan?> GetByGroupIdAndMemberIdAsync(Guid groupId, Guid memberId);

    Task<PagedList<User>> GetByGroupIdAsync(Guid groupId, MemberParameters parameters);
}