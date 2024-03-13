using ChatApp.Server.Domain.Users;
using ChatApp.Server.Domain.Users.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Infrastructure.Users;

public sealed class UserAvatarRepository(ApplicationDbContext dbContext)
    : Repository<UserAvatar>(dbContext), IUserAvatarRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<UserAvatar?> GetByIdsAsync(Guid userId, Guid resourceId)
    {
        return await _dbContext.Set<UserAvatar>()
            .Include(avatar => avatar.Resource)
            .FirstOrDefaultAsync(avatar => avatar.UserId == userId && avatar.ResourceId == resourceId);
    }
}