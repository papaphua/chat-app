using ChatApp.Server.Api.Core.Abstractions;
using ChatApp.Server.Api.Core.Extensions;
using ChatApp.Server.Application.Contacts;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Api.Controllers;

[Route("api/contact")]
public sealed class ContactController(
    IContactService contactService)
    : ApiController
{
    [HttpGet("{contactId:guid}")]
    public async Task<IResult> GetContact(Guid contactId)
    {
        var result = await contactService.GetContactAsync(UserId, contactId);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
}