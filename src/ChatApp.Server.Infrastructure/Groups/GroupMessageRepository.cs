using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupMessageRepository(ApplicationDbContext dbContext)
    : Repository<GroupMessage>(dbContext), IGroupMessageRepository;