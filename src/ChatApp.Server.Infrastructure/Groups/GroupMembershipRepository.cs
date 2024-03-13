using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupMembershipRepository(ApplicationDbContext dbContext)
    : Repository<GroupMembership>(dbContext), IGroupMembershipRepository
{
}