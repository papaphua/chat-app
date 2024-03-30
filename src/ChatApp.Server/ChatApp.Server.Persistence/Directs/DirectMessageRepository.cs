using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Directs;

public sealed class DirectMessageRepository(ApplicationDbContext dbContext)
    : Repository<DirectMessage>(dbContext), IDirectMessageRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<DirectMessage?> GetByIdAsync(Guid id, bool includeDeletions = false)
    {
        var query = _dbContext.Set<DirectMessage>()
            .AsQueryable();

        if (includeDeletions)
            query = query.Include(message => message.Deletions);
        
        return await query.FirstOrDefaultAsync(message => message.Id == id);
    }
}