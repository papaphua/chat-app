using ChatApp.Server.Api.Core.Abstractions;
using ChatApp.Server.Api.Core.Extensions;
using ChatApp.Server.Application.Contacts;
using ChatApp.Server.Application.Contacts.Dtos;
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

    [HttpPost("{partnerId:guid}")]
    public async Task<IResult> AddContact(Guid partnerId, NameDto dto)
    {
        var result = await contactService.AddContactAsync(UserId, partnerId, dto);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpDelete("{contactId:guid}")]
    public async Task<IResult> RemoveContact(Guid contactId)
    {
        var result = await contactService.RemoveContactAsync(UserId, contactId);
        
        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPut("{contactId:guid}/name")]
    public async Task<IResult> UpdateName(Guid contactId, NameDto dto)
    {
        var result = await contactService.UpdateNameAsync(UserId, contactId, dto);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
    
    [HttpPut("{contactId:guid}/avatar")]
    public async Task<IResult> SetAvatar(Guid contactId, IFormFile file)
    {
        var result = await contactService.SetAvatarAsync(UserId, contactId, file.ToNewResourceDto());
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
    
    [HttpDelete("{contactId:guid}/avatar")]
    public async Task<IResult> RemoveAvatar(Guid contactId)
    {
        var result = await contactService.RemoveAvatarAsync(UserId, contactId);
        
        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
}