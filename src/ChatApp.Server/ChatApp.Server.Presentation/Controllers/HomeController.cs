using ChatApp.Server.Application.Home;
using ChatApp.Server.Presentation.Core.Abstractions;
using ChatApp.Server.Presentation.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Presentation.Controllers;

[Route("api/home")]
public sealed class HomeController(IHomeService homeService)
    : ApiController
{
    [HttpGet]
    public async Task<IResult> GetChatPreviews()
    {
        var result = await homeService.GetChatPreviewsAsync(UserId);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpGet("search/{value}")]
    public async Task<IResult> SearchChatsByName(string value)
    {
        var result = await homeService.SearchChatsByNameAsync(UserId, value);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
}