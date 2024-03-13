using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupAvatarRepository(ApplicationDbContext dbContext)
    : Repository<GroupAvatar>(dbContext), IGroupAvatarRepository
{
}