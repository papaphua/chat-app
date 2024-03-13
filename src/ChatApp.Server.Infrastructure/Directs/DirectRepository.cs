using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Directs;

public sealed class DirectRepository(ApplicationDbContext dbContext)
    : Repository<Direct>(dbContext), IDirectRepository;