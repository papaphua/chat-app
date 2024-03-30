using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Groups;

public sealed class GroupDeletionRepository(ApplicationDbContext dbContext)
    : Repository<GroupDeletion>(dbContext), IGroupDeletionRepository;