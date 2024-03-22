using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupRequestRepository(ApplicationDbContext dbContext)
    : Repository<GroupRequest>(dbContext), IGroupRequestRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<GroupRequest?> GetByIdsAsync(Guid groupId, Guid userId)
    {
        return await _dbContext.Set<GroupRequest>()
            .FirstOrDefaultAsync(request => request.GroupId == groupId
                                            && request.UserId == userId);
    }
}