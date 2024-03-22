using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupAvatarRepository : IRepository<GroupAvatar>
{
    Task<GroupAvatar?> GetByIdsAsync(Guid groupId, Guid resourceId, bool includeResource = false);
}