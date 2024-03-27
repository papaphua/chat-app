using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Directs.Repositories;

public interface IDirectMessageRepository : IRepository<DirectMessage>
{
    Task<DirectMessage?> GetByIdAsync(Guid id, bool includeDeletions = false);
}