using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupRepository(ApplicationDbContext dbContext)
    : Repository<Group>(dbContext), IGroupRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Group?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<Group>()
            .FirstOrDefaultAsync(group => group.Id == id);
    }
}