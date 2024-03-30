using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupMembershipRepository(ApplicationDbContext dbContext)
    : Repository<GroupMembership>(dbContext), IGroupMembershipRepository;