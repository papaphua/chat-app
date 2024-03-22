using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupBanRepository : IRepository<GroupBan>
{
    Task<GroupBan?> GetByIdsAsync(Guid groupId, Guid userId);
}