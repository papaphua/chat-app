using AutoMapper;
using ChatApp.Server.Application.Contacts.Dtos;
using ChatApp.Server.Domain.Contacts;

namespace ChatApp.Server.Application.Contacts.Maps;

public sealed class ContactDtoMap : Profile
{
    public ContactDtoMap()
    {
        CreateMap<Contact, ContactDto>();
    }
}