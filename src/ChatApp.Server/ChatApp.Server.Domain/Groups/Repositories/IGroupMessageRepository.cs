using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Shared;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupMessageRepository : IRepository<GroupMessage>
{
    Task<PagedList<GroupMessage>> GetPagedByGroupIdAndUserIdAsync(Guid groupId, Guid userId,
        MessageParameters parameters);

    Task<GroupMessage?> GetByIdAsync(Guid id, bool includeDeletions = false);
}