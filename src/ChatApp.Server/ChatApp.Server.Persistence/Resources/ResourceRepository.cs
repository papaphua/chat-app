using ChatApp.Server.Domain.Resources;
using ChatApp.Server.Domain.Resources.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;

namespace ChatApp.Server.Persistence.Resources;

public sealed class ResourceRepository(ApplicationDbContext dbContext)
    : Repository<Resource>(dbContext), IResourceRepository;