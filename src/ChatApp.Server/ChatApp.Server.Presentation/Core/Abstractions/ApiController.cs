using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Presentation.Core.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected Guid UserId
    {
        get
        {
            var stringId = HttpContext.User.FindFirstValue("sub");
            return Guid.TryParse(stringId, out var guid)
                ? guid
                : Guid.Empty;
        }
    }
}