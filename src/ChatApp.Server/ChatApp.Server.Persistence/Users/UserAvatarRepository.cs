﻿using ChatApp.Server.Domain.Users;
using ChatApp.Server.Domain.Users.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Users;

public sealed class UserAvatarRepository(ApplicationDbContext dbContext)
    : Repository<UserAvatar>(dbContext), IUserAvatarRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<UserAvatar?> GetByIdsAsync(Guid userId, Guid resourceId, bool includeResource = false)
    {
        var query = _dbContext.Set<UserAvatar>()
            .AsQueryable();

        if (includeResource)
            query = query.Include(avatar => avatar.Resource);

        return await query
            .OrderByDescending(avatar => avatar.Timestamp)
            .FirstOrDefaultAsync(avatar => avatar.UserId == userId && avatar.ResourceId == resourceId);
    }

    public async Task<UserAvatar?> GetLatestByUserIdAsync(Guid userId)
    {
        return await _dbContext.Set<UserAvatar>()
            .OrderByDescending(avatar => avatar.Timestamp)
            .FirstOrDefaultAsync(user => user.UserId == userId);
    }
}