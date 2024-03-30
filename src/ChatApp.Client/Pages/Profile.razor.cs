using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace ChatApp.Client.Pages;

[Authorize]
public sealed partial class Profile
{
    [Inject] private IDialogService DialogService { get; set; } = default!;

    private IBrowserFile? File { get; set; }
}