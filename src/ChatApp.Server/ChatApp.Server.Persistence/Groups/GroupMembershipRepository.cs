using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Domain.Shared;
using ChatApp.Server.Domain.Users;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupMembershipRepository(ApplicationDbContext dbContext)
    : Repository<GroupMembership>(dbContext), IGroupMembershipRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<PagedList<User>> GetPagedByGroupIdAsync(Guid groupId,
        MemberParameters parameters)
    {
        return await _dbContext.Set<GroupMembership>()
            .Where(membership => membership.GroupId == groupId)
            .Select(membership => membership.Member)
            .ToPagedListAsync(parameters);
    }

    public async Task<GroupMembership?> GetByGroupIdAndMemberIdAsync(Guid groupId, Guid memberId,
        bool includeGroup = false, bool includeGroupWithAvatars = false, bool includeRole = false)
    {
        var query = _dbContext.Set<GroupMembership>()
            .AsQueryable();

        if (includeGroup)
            query = query.Include(membership => membership.Group);

        if (includeGroupWithAvatars)
            query = query.Include(membership => membership.Group)
                .ThenInclude(group => group.Avatars);

        if (includeRole)
            query = query.Include(membership => membership.Role);

        return await query.FirstOrDefaultAsync(membership => membership.GroupId == groupId
                                                             && membership.MemberId == memberId);
    }
}