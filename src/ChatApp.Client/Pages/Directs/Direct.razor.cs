using ChatApp.Client.Dtos;
using ChatApp.Client.Services.DirectService;
using ChatApp.Client.Services.ResourceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ChatApp.Client.Pages.Directs;

[Authorize]
public sealed partial class Direct
{
    [Parameter] public Guid DirectId { get; set; }
    [Inject] private IDirectService DirectService { get; set; } = default!;
    [Inject] private IResourceService ResourceService { get; set; } = default!;
    private DirectDto DirectDto { get; set; } = new();
    private string? Image { get; set; }
    private string? Input { get; set; }
    private List<IBrowserFile> Files { get; set; }

    protected override async Task OnInitializedAsync()
    {
        DirectDto = await DirectService.GetDirectAsync(DirectId);
        if (DirectDto.Avatars.Count > 0)
        {
            Image = (await ResourceService.GetResourceAsync(DirectDto.Avatars[0].ResourceId)).Bytes;
        }
    }

    private void SetFiles(InputFileChangeEventArgs e)
    {
        Files = e.GetMultipleFiles().ToList();
    }
}