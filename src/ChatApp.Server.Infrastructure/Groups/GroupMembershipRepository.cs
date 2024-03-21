using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupMembershipRepository(ApplicationDbContext dbContext)
    : Repository<GroupMembership>(dbContext), IGroupMembershipRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<GroupMembership?> GetByIdsAsync(Guid groupId, Guid memberId, bool includeRole = false)
    {
        var query = _dbContext.Set<GroupMembership>()
            .AsQueryable();

        if (includeRole)
            query = query.Include(membership => membership.Role);

        return await query.FirstOrDefaultAsync(membership => membership.GroupId == groupId
                                                             && membership.MemberId == memberId);
    }
}