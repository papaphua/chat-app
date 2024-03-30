using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Directs;

public sealed class DirectAttachmentRepository(ApplicationDbContext dbContext)
    : Repository<DirectAttachment>(dbContext), IDirectAttachmentRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<DirectAttachment>> GetByMessageIdAsync(Guid messageId, bool includeResource = false)
    {
        var query = _dbContext.Set<DirectAttachment>()
            .AsQueryable();

        if (includeResource)
            query = query.Include(attachment => attachment.Resource);

        return await query.Where(attachment => attachment.MessageId == messageId)
            .ToListAsync();
    }
}