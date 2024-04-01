using ChatApp.Client.Dtos;
using ChatApp.Client.Services.ProfileService;
using Microsoft.AspNetCore.Components;

namespace ChatApp.Client.Pages.Profiles;

public sealed partial class Profile
{
    [Inject] private IProfileService ProfileService { get; set; } = default!;

    private ProfileNameDto Information { get; set; } = new();
    private UserNameDto UserNameInput { get; set; } = new(); 
    
    protected override async Task OnInitializedAsync()
    {
        var profile = await ProfileService.GetProfileAsync();
        Information.FirstName = profile.FirstName;
        Information.LastName = profile.LastName;
        Information.Bio = profile.Bio;
        UserNameInput.UserName = profile.UserName;
    }
}