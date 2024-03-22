using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupAvatarRepository(ApplicationDbContext dbContext)
    : Repository<GroupAvatar>(dbContext), IGroupAvatarRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<GroupAvatar?> GetByIdsAsync(Guid groupId, Guid resourceId, bool includeResource = false)
    {
        var query = _dbContext.Set<GroupAvatar>()
            .AsQueryable();

        if (includeResource)
            query = query.Include(avatar => avatar.Resource);

        return await query.FirstOrDefaultAsync(avatar => avatar.GroupId == groupId
                                                         && avatar.ResourceId == resourceId);
    }
}