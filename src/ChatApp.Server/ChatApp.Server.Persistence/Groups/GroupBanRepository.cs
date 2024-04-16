using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Domain.Shared;
using ChatApp.Server.Domain.Users;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupBanRepository(ApplicationDbContext dbContext)
    : Repository<GroupBan>(dbContext), IGroupBanRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<GroupBan?> GetByGroupIdAndMemberIdAsync(Guid groupId, Guid memberId)
    {
        return await _dbContext.Set<GroupBan>()
            .FirstOrDefaultAsync(ban => ban.GroupId == groupId
                                        && ban.UserId == memberId);
    }

    public async Task<PagedList<User>> GetByGroupIdAsync(Guid groupId, MemberParameters parameters)
    {
        return await _dbContext.Set<GroupBan>()
            .Include(ban => ban.User)
            .Where(ban => ban.GroupId == groupId)
            .Select(ban => ban.User)
            .ToPagedListAsync(parameters);
    }
}