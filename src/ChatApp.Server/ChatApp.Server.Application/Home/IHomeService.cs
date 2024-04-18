using ChatApp.Server.Application.Home.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;

namespace ChatApp.Server.Application.Home;

public interface IHomeService
{
    Task<Result<List<ChatPreviewDto>>> GetChatPreviewsAsync(Guid userId);
}