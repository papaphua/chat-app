using ChatApp.Server.Domain.Resources;
using ChatApp.Server.Domain.Resources.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Resources;

public sealed class ResourceRepository(ApplicationDbContext dbContext)
    : Repository<Resource>(dbContext), IResourceRepository;