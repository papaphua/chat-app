using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace ChatApp.Client.Pages;

[Authorize]
public sealed partial class Profile
{
    private IBrowserFile? File { get; set; }
}