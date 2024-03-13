using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Directs;

public sealed class DirectReactionRepository(ApplicationDbContext dbContext)
    : Repository<DirectReaction>(dbContext), IDirectReactionRepository
{
}