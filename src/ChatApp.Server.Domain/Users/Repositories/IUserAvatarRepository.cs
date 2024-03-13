using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Users.Repositories;

public interface IUserAvatarRepository : IRepository<UserAvatar>
{
    Task<UserAvatar?> GetByIdsAsync(Guid userId, Guid resourceId);
}