using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupRepository : IRepository<Group>
{
    Task<Group?> GetByIdAsync(Guid id);
}