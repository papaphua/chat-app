using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Infrastructure.Directs;

public sealed class DirectRepository(ApplicationDbContext dbContext)
    : Repository<Direct>(dbContext), IDirectRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Direct?> GetByIdAsync(Guid id, bool includeMemberships = false)
    {
        var query = _dbContext.Set<Direct>()
            .AsQueryable();

        if (includeMemberships)
            query = query.Include(direct => direct.Memberships)
                .ThenInclude(membership => membership.Member);

        return await query.FirstOrDefaultAsync(direct => direct.Id == id);
    }
}