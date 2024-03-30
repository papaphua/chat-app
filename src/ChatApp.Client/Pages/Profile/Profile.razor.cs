using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace ChatApp.Client.Pages.Profile;

[Authorize]
public sealed partial class Profile
{
    [Inject] private IDialogService DialogService { get; set; } = default!;

    private IBrowserFile? File { get; set; }

    private DialogOptions _dialogOptions = new() { CloseOnEscapeKey = false };

    private void OpenEmailConfirmation()
    {
        DialogService.Show<EmailConfirmation>("Email confirmation", _dialogOptions);
    }

    private void OpenPhoneConfirmation()
    {
        DialogService.Show<PhoneConfirmation>("Phone confirmation", _dialogOptions);
    }
}