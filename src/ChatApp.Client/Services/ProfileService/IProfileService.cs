using ChatApp.Client.Dtos;

namespace ChatApp.Client.Services.ProfileService;

public interface IProfileService
{
    Task<ProfileDto> GetProfileAsync();

    Task UpdateNameAsync(ProfileNameDto dto);

    Task UpdateUserName(UserNameDto dto);

    Task SendChangeEmailTokenAsync(EmailDto dto);

    Task ChangeEmailAsync(EmailChangeDto dto);

    Task SendChangePhoneTokenAsync(PhoneNumberDto dto);

    Task ChangePhoneAsync(PhoneNumberChangeDto dto);

    Task ChangePasswordAsync(NewPasswordDto dto);
}