using AutoMapper;
using ChatApp.Server.Application.Contacts.Dtos;
using ChatApp.Server.Domain.Contacts;

namespace ChatApp.Server.Application.Contacts.Maps;

public sealed class ContactNameDtoMap : Profile
{
    public ContactNameDtoMap()
    {
        CreateMap<ContactNameDto, Contact>()
            .ForMember(dest => dest.LastName, opt =>
                opt.MapFrom(src => string.IsNullOrWhiteSpace(src.LastName)
                    ? null
                    : src.LastName));

        CreateMap<Contact, ContactNameDto>();
    }
}