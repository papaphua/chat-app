﻿using ChatApp.Server.Application.Contacts;
using ChatApp.Server.Application.Contacts.Dtos;
using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Presentation.Core.Abstractions;
using ChatApp.Server.Presentation.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChatApp.Server.Presentation.Controllers;

[Route("api/contact")]
public sealed class ContactController(
    IContactService contactService)
    : ApiController
{
    [HttpGet]
    public async Task<IResult> GetAllContacts([FromQuery] ContactParameters parameters)
    {
        var result = await contactService.GetAllContactsAsync(UserId, parameters);

        if (!result.IsSuccess)
            return result.ToProblemDetails();

        Response.Headers["X-PagedData"] = JsonConvert.SerializeObject(result.Value!.PagedData);

        return Results.Ok(result.Value);
    }

    [HttpGet("{contactId:guid}")]
    public async Task<IResult> GetContact(Guid contactId)
    {
        var result = await contactService.GetContactAsync(UserId, contactId);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpPost("{partnerId:guid}")]
    public async Task<IResult> AddContact(Guid partnerId, ContactNameDto dto)
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
    public async Task<IResult> UpdateName(Guid contactId, ContactNameDto dto)
    {
        var result = await contactService.UpdateNameAsync(UserId, contactId, dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPut("{contactId:guid}/avatar")]
    public async Task<IResult> AddAvatar(Guid contactId, [FromForm(Name = "files")] IFormFile file)
    {
        var result = await contactService.AddAvatarAsync(UserId, contactId, file);

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