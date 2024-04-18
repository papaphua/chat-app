using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Shared;

namespace ChatApp.Server.Domain.Directs.Repositories;

public interface IDirectMessageRepository : IRepository<DirectMessage>
{
    Task<DirectMessage?> GetLastMessageAsync(Guid directId, Guid userId, bool includeAttachments = false);
    
    Task<PagedList<DirectMessage>> GetPagedByDirectIdAndUserIdAsync(Guid directId, Guid userId, MessageParameters parameters);
    
    Task<DirectMessage?> GetByIdAsync(Guid id, bool includeDeletions = false);
}