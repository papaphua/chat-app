using ChatApp.Client.Dtos;
using ChatApp.Client.Services.ProfileService;
using ChatApp.Client.Services.ResourceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace ChatApp.Client.Pages.Profiles;

[Authorize]
public sealed partial class Profile
{
    private readonly DialogOptions _dialogOptions = new() { CloseOnEscapeKey = false };
    private MudTextField<string> pwField1;
    [Inject] private IProfileService ProfileService { get; set; } = default!;
    [Inject] private IResourceService ResourceService { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;

    private ProfileNameDto Information { get; } = new();
    private UserNameDto UserNameInput { get; } = new();
    private EmailDto EmailInput { get; } = new();
    private PhoneNumberDto PhoneNumberInput { get; } = new();
    private NewPasswordDto PasswordInput { get; } = new();
    private IBrowserFile? File { get; set; }

    private Dictionary<int, string> AvatarImages { get; } = [];
    private List<AvatarDto> AvatarDtos { get; } = [];
    private int SelectedAvatar { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var profile = await ProfileService.GetProfileAsync();
        Information.FirstName = profile.FirstName;
        Information.LastName = profile.LastName;
        Information.Bio = profile.Bio;
        UserNameInput.UserName = profile.UserName;
        EmailInput.Email = profile.Email;
        PhoneNumberInput.PhoneNumber = profile.PhoneNumber;
        if (profile.Avatars.Count != 0)
        {
            AvatarDtos.AddRange(profile.Avatars);
            await DownloadAvatarAsync(0);
        }
    }

    private string? PasswordMatch(string arg)
    {
        return pwField1.Value != arg ? "Passwords don't match" : null;
    }

    private async Task SendEmailConfirmationAsync()
    {
        var result = await ProfileService.SendChangeEmailTokenAsync(EmailInput);
        if (result) OpenEmailDialog();
    }

    private async Task SendPhoneConfirmationAsync()
    {
        var result = await ProfileService.SendChangePhoneTokenAsync(PhoneNumberInput);
        if (result) OpenPhoneDialog();
    }

    private void OpenEmailDialog()
    {
        var parameters = new DialogParameters { { "Email", EmailInput.Email } };

        DialogService.Show<EmailDialog>("Email confirmation", parameters, _dialogOptions);
    }

    private void OpenPhoneDialog()
    {
        var parameters = new DialogParameters { { "PhoneNumber", PhoneNumberInput.PhoneNumber } };

        DialogService.Show<PhoneDialog>("Phone confirmation", parameters, _dialogOptions);
    }

    private void SetFile(InputFileChangeEventArgs e)
    {
        File = e.File;
    }

    private async Task DownloadAvatarAsync(int newIndex)
    {
        SelectedAvatar = newIndex;
        var id = AvatarDtos[SelectedAvatar].ResourceId;
        if (!AvatarImages.ContainsKey(SelectedAvatar))
        {
            var resource = await ResourceService.GetResourceAsync(id);
            AvatarImages.Add(SelectedAvatar, resource.Bytes);
            StateHasChanged();
        }
    }

    private string? GetImageValue()
    {
        AvatarImages.TryGetValue(SelectedAvatar, out var value);
        return value;
    }
}