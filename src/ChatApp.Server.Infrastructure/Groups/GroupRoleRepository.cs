using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupRoleRepository(ApplicationDbContext dbContext)
    : Repository<GroupRole>(dbContext), IGroupRoleRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<GroupRole?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<GroupRole>()
            .FirstOrDefaultAsync(role => role.Id == id);
    }
}