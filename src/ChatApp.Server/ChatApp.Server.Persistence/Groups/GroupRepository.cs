using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupRepository(ApplicationDbContext dbContext)
    : Repository<Group>(dbContext), IGroupRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<Group>> GetByNameAsync(Guid userId, string search, bool includeAvatars = false)
    {
        var query = _dbContext.Set<Group>()
            .AsQueryable();

        if (includeAvatars)
            query = query.Include(group => group.Avatars
                .OrderByDescending(avatar => avatar.Timestamp));

        return await query.Where(group => EF.Functions.Like(group.Name, $"%{search}%")
                                          && group.Memberships.All(membership => membership.MemberId != userId)
                                          && group.IsPublic)
            .ToListAsync();
    }

    public async Task<List<Group>> GetAllByUserIdAsync(Guid userId, bool includeAvatars = false)
    {
        var query = _dbContext.Set<Group>()
            .AsQueryable();

        if (includeAvatars)
            query = query.Include(group => group.Avatars);

        return await query.Where(group => group.Memberships.Any(membership => membership.MemberId == userId))
            .ToListAsync();
    }

    public async Task<Group?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<Group>()
            .FirstOrDefaultAsync(group => group.Id == id);
    }
}