using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ChatApp.Client.Pages.Profile;

public sealed partial class PhoneConfirmationDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;

    private void Submit() => MudDialog.Close(DialogResult.Ok(true));
    
    private void Cancel() => MudDialog.Cancel();
}