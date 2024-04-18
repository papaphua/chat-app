using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Directs;

public sealed class DirectRepository(ApplicationDbContext dbContext)
    : Repository<Direct>(dbContext), IDirectRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<Direct>> GetAllByUserIdAsync(Guid userId, bool includeMembers = false,
        bool includeMemberAvatars = false)
    {
        var query = _dbContext.Set<Direct>()
            .AsQueryable();

        if (includeMembers)
            query = query.Include(direct => direct.Memberships)
                .ThenInclude(membership => membership.Member);

        if (includeMemberAvatars)
            query = query.Include(direct => direct.Memberships)
                .ThenInclude(membership =>
                    membership.Member.Avatars.OrderByDescending(avatar => avatar.Timestamp));

        return await query.Where(direct =>
                direct.Memberships.Any(membership => membership.MemberId == userId && !membership.IsChatSelfDeleted))
            .ToListAsync();
    }

    public async Task<Direct?> GetByIdAsync(Guid id, bool includeMembers = false, bool includeMemberAvatars = false)
    {
        var query = _dbContext.Set<Direct>()
            .AsQueryable();

        if (includeMembers)
            query = query.Include(direct => direct.Memberships)
                .ThenInclude(membership => membership.Member);

        if (includeMemberAvatars)
            query = query.Include(direct => direct.Memberships)
                .ThenInclude(membership =>
                    membership.Member.Avatars.OrderByDescending(avatar => avatar.Timestamp));

        return await query.FirstOrDefaultAsync(direct => direct.Id == id);
    }

    public async Task<Direct?> GetByMemberIds(Guid memberId1, Guid memberId2, bool includeMembers = false)
    {
        var query = _dbContext.Set<Direct>()
            .AsQueryable();

        if (includeMembers)
            query = query.Include(direct => direct.Memberships)
                .ThenInclude(membership => membership.Member);

        return await query.FirstOrDefaultAsync(direct =>
            direct.Memberships.Any(membership => membership.MemberId == memberId1)
            && direct.Memberships.Any(membership => membership.MemberId == memberId2));
    }
}