using ChatApp.Server.Application.Resources;
using ChatApp.Server.Presentation.Core.Abstractions;
using ChatApp.Server.Presentation.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Presentation.Controllers;

[Route("apr/resources")]
public sealed class ResourceController(IResourceService resourceService)
    : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<IResult> GetResource(Guid id)
    {
        var result = await resourceService.GetResourceAsync(id);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
}