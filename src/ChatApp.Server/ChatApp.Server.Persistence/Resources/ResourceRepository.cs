using ChatApp.Server.Domain.Resources;
using ChatApp.Server.Domain.Resources.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Resources;

public sealed class ResourceRepository(ApplicationDbContext dbContext)
    : Repository<Resource>(dbContext), IResourceRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Resource?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<Resource>()
            .FirstOrDefaultAsync(resource => resource.Id == id);
    }
}