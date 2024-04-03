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
        var response = await ContactService.GetAllContacts(_parameters);
        ContactList = response.Items;
        PagedData = response.PagedData;
    }

    private string? GetImageValue(Guid contactId)
    {
        Avatars.TryGetValue(contactId, out var value);
        return value;
    }
}