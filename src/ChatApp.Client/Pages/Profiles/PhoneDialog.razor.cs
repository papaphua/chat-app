using ChatApp.Client.Dtos;
using ChatApp.Client.Services.ProfileService;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ChatApp.Client.Pages.Profiles;

public sealed partial class PhoneDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;

    [Inject] private IProfileService ProfileService { get; set; } = default!;
    
    private PhoneNumberChangeDto Input { get; set; } = new();

    [Parameter] public string PhoneNumber { get; set; } = default!;

    private async Task Submit()
    {
        Input.PhoneNumber = PhoneNumber;
        await ProfileService.ChangePhoneAsync(Input);
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() => MudDialog.Cancel();
}