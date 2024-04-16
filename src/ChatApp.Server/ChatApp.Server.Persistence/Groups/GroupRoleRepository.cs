using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Domain.Shared;
using ChatApp.Server.Persistence.Core.Abstractions;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupRoleRepository(ApplicationDbContext dbContext)
    : Repository<GroupRole>(dbContext), IGroupRoleRepository
{
    public async Task<PagedList<GroupRole>> GetByGroupId(Guid groupId, RoleParameters parameters)
    {
        return await dbContext.Set<GroupRole>()
            .Where(role => role.GroupId == groupId)
            .ToPagedListAsync(parameters);
    }
}