using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupBanRepository(ApplicationDbContext dbContext)
    : Repository<GroupBan>(dbContext), IGroupBanRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<GroupBan?> GetByIdsAsync(Guid groupId, Guid userId)
    {
        return await _dbContext.Set<GroupBan>()
            .FirstOrDefaultAsync(ban => ban.GroupId == groupId
                                        && ban.UserId == userId);
    }
}