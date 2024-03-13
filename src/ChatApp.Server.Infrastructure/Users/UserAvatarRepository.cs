using ChatApp.Server.Domain.Users;
using ChatApp.Server.Domain.Users.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Users;

public sealed class UserAvatarRepository(ApplicationDbContext dbContext)
    : Repository<UserAvatar>(dbContext), IUserAvatarRepository
{
}