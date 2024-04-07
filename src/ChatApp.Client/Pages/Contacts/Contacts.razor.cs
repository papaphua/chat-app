using ChatApp.Client.Core.Paging;
using ChatApp.Client.Dtos;
using ChatApp.Client.Services.ContactService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using IResourceService = ChatApp.Client.Services.ResourceService.IResourceService;

namespace ChatApp.Client.Pages.Contacts;

[Authorize]
public sealed partial class Contacts
{
    private readonly ContactParameters _parameters = new();
    [Inject] private IContactService ContactService { get; set; } = default!;
    [Inject] private IResourceService ResourceService { get; set; } = default!;
    private List<ContactDto> ContactList { get; set; } = [];
    private PagedData PagedData { get; set; } = new();
    private Dictionary<Guid, string> Avatars { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await SetPage(1);
    }

    private async Task LoadAvatars()
    {
        foreach (var contact in ContactList)
        {
            if (contact.Avatar is null || Avatars.TryGetValue(contact.Id, out _)) continue;

            var resource = await ResourceService.GetResourceAsync(contact.Avatar.ResourceId);
            
            Avatars.Add(contact.Id, resource.Bytes);
        }
    }
    
    private string? GetImageValue(Guid contactId)
    {
        Avatars.TryGetValue(contactId, out var value);
        return value;
    }

    private async Task SetPage(int newPage)
    {
        _parameters.CurrentPage = newPage;
        var response = await ContactService.GetAllContacts(_parameters);
        ContactList = response.Items;
        PagedData = response.PagedData;
        await LoadAvatars();
    }
}