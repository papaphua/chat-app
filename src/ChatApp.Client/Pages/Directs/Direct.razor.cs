using ChatApp.Client.Core.Paging;
using ChatApp.Client.Dtos;
using ChatApp.Client.Services;
using ChatApp.Client.Services.DirectService;
using ChatApp.Client.Services.ResourceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ChatApp.Client.Pages.Directs;

[Authorize]
public sealed partial class Direct
{
    private readonly MessageParameters _parameters = new();
    [Parameter] public Guid DirectId { get; set; }
    [Inject] private IDirectService DirectService { get; set; } = default!;
    [Inject] private IResourceService ResourceService { get; set; } = default!;
    private DirectDto DirectDto { get; set; } = new();
    private string? Image { get; set; }
    private string? Input { get; set; }
    private List<IBrowserFile> Files { get; set; }
    private List<MessageDto> Messages { get; set; } = [];
    private PagedData PagedData { get; set; } = new();
    private Dictionary<Guid, string> Resources { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        DirectDto = await DirectService.GetDirectAsync(DirectId);
        if (DirectDto.Avatars.Count > 0)
        {
            Image = (await ResourceService.GetResourceAsync(DirectDto.Avatars[0].ResourceId)).Bytes;
        }

        await SetPage(1);
    }

    private void SetFiles(InputFileChangeEventArgs e)
    {
        Files = e.GetMultipleFiles().ToList();
    }

    private string? GetImageValue(Guid resourceId)
    {
        Resources.TryGetValue(resourceId, out var value);
        return value;
    }
    
    private async Task LoadAttachments()
    {
        var ids = Messages.SelectMany(m => m.Attachments)
            .Select(a => a.ResourceId);
        foreach (var id in ids)
        {
            if (Resources.TryGetValue(id, out _)) continue;
                
            var resource = await ResourceService.GetResourceAsync(id);
            Resources.Add(id, resource.Bytes);
        }
    }

    private async Task SetPage(int newPage)
    {
        _parameters.CurrentPage = newPage;
        var response = await DirectService.GetAllMessagesAsync(DirectId, _parameters);
        Messages = response.Items;
        PagedData = response.PagedData;
        await LoadAttachments();
    }
}