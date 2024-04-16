using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Domain.Shared;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupRoleRepository(ApplicationDbContext dbContext)
    : Repository<GroupRole>(dbContext), IGroupRoleRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<GroupRole?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<GroupRole>()
            .FirstOrDefaultAsync(role => role.Id == id);
    }

    public async Task<PagedList<GroupRole>> GetByGroupIdAsync(Guid groupId, RoleParameters parameters)
    {
        return await _dbContext.Set<GroupRole>()
            .Where(role => role.GroupId == groupId)
            .ToPagedListAsync(parameters);
    }
}