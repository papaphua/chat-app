using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupRoleRepository : IRepository<GroupRole>
{
    Task<GroupRole?> GetByIdAsync(Guid id);
}