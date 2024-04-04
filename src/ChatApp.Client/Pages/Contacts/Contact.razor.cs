using ChatApp.Client.Dtos;
using ChatApp.Client.Services.ContactService;
using ChatApp.Client.Services.ResourceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ChatApp.Client.Pages.Contacts;

[Authorize]
public sealed partial class Contact
{
    private ContactDto ContactDto { get; set; } = new();
    private ContactNameDto Input { get; set; } = new();
    
    private string? Image { get; set; }
    private IBrowserFile? File { get; set; }
    
    [Inject] private IContactService ContactService { get; set; } = default!;
    [Inject] private IResourceService ResourceService { get; set; } = default!;
    [Parameter] public Guid ContactId { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        ContactDto = await ContactService.GetContactAsync(ContactId);

        if (ContactDto.Avatar is not null)
            Image = (await ResourceService.GetResourceAsync(ContactDto.Avatar.ResourceId)).Bytes;
        
        Input.FirstName = ContactDto.FirstName!;
        Input.LastName = ContactDto.LastName;
    }
    
    private void SetFile(InputFileChangeEventArgs e)
    {
        File = e.File;
    }
}