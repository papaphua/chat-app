using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupRepository(ApplicationDbContext dbContext)
    : Repository<Group>(dbContext), IGroupRepository
{
}