using ChatApp.Client.Dtos;

namespace ChatApp.Client.Services.ProfileService;

public interface IProfileService
{
    Task<ProfileDto> GetProfileAsync();

    Task UpdateNameAsync(ProfileNameDto dto);

    Task UpdateUserName(UserNameDto dto);
    
    
}