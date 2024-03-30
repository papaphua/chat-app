﻿using ChatApp.Server.Domain.Users;
using ChatApp.Server.Domain.Users.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Users;

public sealed class UserRepository(ApplicationDbContext dbContext)
    : Repository<User>(dbContext), IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<User?> GetByIdAsync(Guid id, bool includeAvatars = false)
    {
        var query = _dbContext.Set<User>()
            .AsQueryable();

        if (includeAvatars)
            query = query.Include(user =>
                user.Avatars.OrderByDescending(avatar => avatar.Timestamp));

        return await query.FirstOrDefaultAsync(user => user.Id == id);
    }
}