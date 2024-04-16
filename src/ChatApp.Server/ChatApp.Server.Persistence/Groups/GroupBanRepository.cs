using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
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
}