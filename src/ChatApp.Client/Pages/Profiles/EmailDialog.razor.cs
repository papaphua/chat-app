using ChatApp.Client.Dtos;
using ChatApp.Client.Services.ProfileService;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ChatApp.Client.Pages.Profiles;

public sealed partial class EmailDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
    [Inject] private IProfileService ProfileService { get; set; } = default!;

    private EmailChangeDto Input { get; set; } = new();

    [Parameter] public string Email { get; set; } = default!;

    private async Task Submit()
    {
        Input.Email = Email;
        await ProfileService.ChangeEmailAsync(Input);
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() => MudDialog.Cancel();
}