using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupBanRepository(ApplicationDbContext dbContext)
    : Repository<GroupBan>(dbContext), IGroupBanRepository;