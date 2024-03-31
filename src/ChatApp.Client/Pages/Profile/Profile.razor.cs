using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace ChatApp.Client.Pages.Profile;

[Authorize]
public sealed partial class Profile
{
    [Inject] private IDialogService DialogService { get; set; } = default!;

    private const string EmailConfirmationDialogTitle = "Email confirmation";
    private const string PhoneConfirmationDialogTitle = "Phone confirmation";

    private readonly DialogOptions _dialogOptions = new() { CloseOnEscapeKey = false };

    private IBrowserFile? File { get; set; }

    private void OpenEmailConfirmationDialog()
    {
        DialogService.Show<EmailConfirmationDialog>(EmailConfirmationDialogTitle, _dialogOptions);
    }

    private void OpenPhoneConfirmationDialog()
    {
        DialogService.Show<PhoneConfirmationDialog>(PhoneConfirmationDialogTitle, _dialogOptions);
    }
}