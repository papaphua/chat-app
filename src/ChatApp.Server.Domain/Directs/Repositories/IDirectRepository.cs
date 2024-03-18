using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Directs.Repositories;

public interface IDirectRepository : IRepository<Direct>
{
    Task<Direct?> GetByIdAsync(Guid id, bool includeMemberships = false);
}