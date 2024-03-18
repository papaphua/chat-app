using ChatApp.Server.Application.Directs.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;

namespace ChatApp.Server.Application.Directs;

public interface IDirectService
{
    Task<Result<DirectDto>> GetDirectAsync(Guid userId, Guid directId);

    Task<Result<Guid>> CreateDirectAsync(Guid userId, Guid partnerId);

    Task<Result> RemoveDirectAsync(Guid userId, Guid directId);

    Task<Result> RemoveDirectForSelfAsync(Guid userId, Guid directId);
}