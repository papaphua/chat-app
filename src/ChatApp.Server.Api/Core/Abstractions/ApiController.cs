using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Api.Core.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase
{
    public Guid UserId =>
        // var stringId = HttpContext.User.FindFirstValue("sub");
        // return Guid.TryParse(stringId, out var guid)
        //     ? guid
        //     : Guid.Empty;
        Guid.Parse("4CD62940-6744-4BAD-83BB-B2B1830A2BB7");
}