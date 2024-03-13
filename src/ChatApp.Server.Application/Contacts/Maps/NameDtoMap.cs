using AutoMapper;
using ChatApp.Server.Application.Contacts.Dtos;
using ChatApp.Server.Domain.Contacts;

namespace ChatApp.Server.Application.Contacts.Maps;

public sealed class NameDtoMap : Profile
{
    public NameDtoMap()
    {
        CreateMap<NameDto, Contact>();
    }
}