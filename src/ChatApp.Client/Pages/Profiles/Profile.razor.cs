using ChatApp.Client.Dtos;
using ChatApp.Client.Services.ProfileService;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ChatApp.Client.Pages.Profiles;

public sealed partial class Profile
{
    private MudTextField<string> pwField1;
    [Inject] private IProfileService ProfileService { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;
    private readonly DialogOptions _dialogOptions = new() { CloseOnEscapeKey = false };

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

    private async Task SendEmailConfirmationAsync()
    {
        var result = await ProfileService.SendChangeEmailTokenAsync(EmailInput);
        if (result)
        {
            OpenEmailDialog();
        }
    }
    
    private async Task SendPhoneConfirmationAsync()
    {
        var result = await ProfileService.SendChangePhoneTokenAsync(PhoneNumberInput);
        if (result)
        {
            OpenPhoneDialog();
        }
    }

    private void OpenEmailDialog()
    {
        DialogService.Show<EmailDialog>("Email confirmation", _dialogOptions);
    }

    private void OpenPhoneDialog()
    {
        DialogService.Show<PhoneDialog>("Phone confirmation", _dialogOptions);
    }
}