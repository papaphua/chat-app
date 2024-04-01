using ChatApp.Client.Dtos;
using ChatApp.Client.Services.ProfileService;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ChatApp.Client.Pages.Profiles;

public sealed partial class Profile
{
    private MudTextField<string> pwField1;
    [Inject] private IProfileService ProfileService { get; set; } = default!;

    private ProfileNameDto Information { get; } = new();
    private UserNameDto UserNameInput { get; } = new();
    private EmailDto EmailInput { get; } = new();
    private PhoneNumberDto PhoneNumberInput { get; } = new();
    private NewPasswordDto PasswordInput { get; } = new();

    protected override async Task OnInitializedAsync()
    {
        var profile = await ProfileService.GetProfileAsync();
        Information.FirstName = profile.FirstName;
        Information.LastName = profile.LastName;
        Information.Bio = profile.Bio;
        UserNameInput.UserName = profile.UserName;
        EmailInput.Email = profile.Email;
        PhoneNumberInput.PhoneNumber = profile.PhoneNumber;
    }

    private string? PasswordMatch(string arg)
    {
        return pwField1.Value != arg ? "Passwords don't match" : null;
    }
}