using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupReactionRepository(ApplicationDbContext dbContext)
    : Repository<GroupReaction>(dbContext), IGroupReactionRepository;