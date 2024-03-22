using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupRequestRepository : IRepository<GroupRequest>
{
    Task<GroupRequest?> GetByIdsAsync(Guid groupId, Guid userId);
}