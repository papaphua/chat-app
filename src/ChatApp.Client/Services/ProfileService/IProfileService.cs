using ChatApp.Client.Dtos;
using Microsoft.AspNetCore.Components.Forms;

namespace ChatApp.Client.Services.ProfileService;

public interface IProfileService
{
    Task<ProfileDto> GetProfileAsync();

    Task UpdateNameAsync(ProfileNameDto dto);

    Task UpdateUserNameAsync(UserNameDto dto);

    Task AddAvatarAsync(IBrowserFile file);

    Task RemoveAvatarAsync(Guid resourceId);

    Task<bool> SendChangeEmailTokenAsync(EmailDto dto);

    Task ChangeEmailAsync(EmailChangeDto dto);

    Task<bool> SendChangePhoneTokenAsync(PhoneNumberDto dto);

    Task ChangePhoneAsync(PhoneNumberChangeDto dto);

    Task ChangePasswordAsync(NewPasswordDto dto);
}