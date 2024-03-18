﻿using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Infrastructure.Directs;

public sealed class DirectRepository(ApplicationDbContext dbContext)
    : Repository<Direct>(dbContext), IDirectRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Direct?> GetByIdAsync(Guid id, bool includeMemberships = false, bool includeMemberAvatars = false)
    {
        var query = _dbContext.Set<Direct>()
            .AsQueryable();

        if (includeMemberships)
        {
            query = query.Include(direct => direct.Memberships)
                .ThenInclude(membership => membership.Member);

            if (includeMemberAvatars)
                query = query.Include(direct => direct.Memberships)
                    .ThenInclude(membership => membership.Member.Avatars)
                    .ThenInclude(avatar => avatar.Resource);
        }

        return await query.FirstOrDefaultAsync(direct => direct.Id == id);
    }

    public async Task<Direct?> GetByMemberIds(Guid memberId1, Guid memberId2, bool includeMemberships = false)
    {
        var query = _dbContext.Set<Direct>()
            .AsQueryable();

        if (includeMemberships)
            query = query.Include(direct => direct.Memberships)
                .ThenInclude(membership => membership.Member);

        return await query.FirstOrDefaultAsync(direct =>
            direct.Memberships.Any(membership => membership.MemberId == memberId1)
            && direct.Memberships.Any(membership => membership.MemberId == memberId2));
    }
}