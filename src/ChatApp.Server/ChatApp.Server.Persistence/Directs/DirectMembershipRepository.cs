using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;

namespace ChatApp.Server.Persistence.Directs;

public sealed class DirectMembershipRepository(ApplicationDbContext dbContext)
    : Repository<DirectMembership>(dbContext), IDirectMembershipRepository;