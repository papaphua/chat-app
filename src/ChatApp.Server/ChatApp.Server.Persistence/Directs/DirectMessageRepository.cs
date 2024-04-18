using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Domain.Shared;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Directs;

public sealed class DirectMessageRepository(ApplicationDbContext dbContext)
    : Repository<DirectMessage>(dbContext), IDirectMessageRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<DirectMessage?> GetLastMessageAsync(Guid directId, Guid userId, bool includeAttachments = false)
    {
        var query = _dbContext.Set<DirectMessage>()
            .AsQueryable();

        if (includeAttachments)
            query = query.Include(message => message.Attachments);

        return await query.Where(message => message.DirectId == directId
                                            && message.Deletions.All(deletion => deletion.UserId != userId))
            .OrderByDescending(message => message.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task<PagedList<DirectMessage>> GetPagedByDirectIdAndUserIdAsync(Guid directId, Guid userId,
        MessageParameters parameters)
    {
        return await _dbContext.Set<DirectMessage>()
            .Include(message => message.Attachments)
            .Include(message => message.Reactions)
            .Where(message => message.DirectId == directId
                              && message.Deletions.All(deletion => deletion.UserId != userId))
            .OrderByDescending(message => message.Timestamp)
            .ToPagedListAsync(parameters);
    }

    public async Task<DirectMessage?> GetByIdAsync(Guid id, bool includeDeletions = false)
    {
        var query = _dbContext.Set<DirectMessage>()
            .AsQueryable();

        if (includeDeletions)
            query = query.Include(message => message.Deletions);

        return await query.FirstOrDefaultAsync(message => message.Id == id);
    }
}