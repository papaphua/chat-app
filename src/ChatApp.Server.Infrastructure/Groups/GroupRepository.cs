using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupRepository(ApplicationDbContext dbContext)
    : Repository<Group>(dbContext), IGroupRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Group?> GetByIdAsync(Guid id, bool includeAvatars = false, bool includeAvatarResource = false)
    {
        var query = _dbContext.Set<Group>()
            .AsQueryable();

        if (includeAvatars)
            query = query.Include(group => group.Avatars);
        
        if (includeAvatarResource)
            query = query.Include(group => group.Avatars)
                .ThenInclude(avatar => avatar.Resource);
        
        return await query.FirstOrDefaultAsync(group => group.Id == id);
    }
}